using Microsoft.AspNetCore.Mvc;
using MVCBasico.Context;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MVCBasico.Models;
using MVCBasico.Utils;
using System.Threading.Tasks;
using System.Linq;
using System;

namespace MVCBasico.Controllers
{
    public class IngresoSubastasController : Controller
    {
        private readonly SubastaDatabaseContext _context;

        public IngresoSubastasController(SubastaDatabaseContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.Get<Usuario>("_LoginUser") != default)
            {
                var loginUser = HttpContext.Session.Get<Usuario>("_LoginUser");

                List<IngresoSubastaVM> listaIngresoSubasta = await BuildListaIngresoSubastaVM(loginUser.Id);

                return View(listaIngresoSubasta);

            }
            else
            {
                return BadRequest();
            }
        }

        private async Task<List<IngresoSubastaVM>> BuildListaIngresoSubastaVM(int usuarioId)
        {
            List<IngresoSubastaVM> listaIngresoSubasta = new List<IngresoSubastaVM>();

            var subastas = await _context.Subastas
                .Where(s => s.UsuarioId != usuarioId && s.Activa 
                && -1 < DateTime.Compare(DateTime.Now, s.FechaInicio)
                && -1 < DateTime.Compare(s.FechaFinalizacion, DateTime.Now))
                .ToListAsync();
            
            foreach (Subasta subasta in subastas)
            {
                IngresoSubastaVM ingresoSubastaVM = new IngresoSubastaVM();
                ingresoSubastaVM.Codigo = subasta.Codigo;
                ingresoSubastaVM.FechaInicio = subasta.FechaInicio;
                ingresoSubastaVM.FechaFinalizacion = subasta.FechaFinalizacion;
                ingresoSubastaVM.UsuarioId = usuarioId;
                
                /* 
                 cantidadArticulosArte = _context.ArticulosArte
                .Where(a => a.SubastaId == subasta.Id).Count();

                int cantidadArticulosMueble = await _context.ArticulosMueble
                .Where(a => a.SubastaId == subasta.Id).CountAsync();
                
                ingresoSubastaVM.CantidadProductosArte = cantidadArticulosArte;
                ingresoSubastaVM.CantidadProductosMueble = cantidadArticulosMueble;
                ingresoSubastaVM.CantidadProductos = cantidadArticulosMueble + cantidadArticulosArte;
                */
                listaIngresoSubasta.Add(ingresoSubastaVM);
            }

            return listaIngresoSubasta;
        }

        public async Task<IActionResult> GetInto(int? subastaId)
        {
            if (subastaId != null)
            {
                List<ProductoSubastaVM> listaProductosSubasta = await BuildListaProductSubastaVM(subastaId.Value);

                return View(listaProductosSubasta);

            }
            else
            {
                return BadRequest();
            }
        }

        private async Task<List<ProductoSubastaVM>> BuildListaProductSubastaVM(int subastaId)
        {
            List<ProductoSubastaVM> listaProductos = new List<ProductoSubastaVM>();

            var articulosArte = await _context.ArticulosArte
                            .Where(e => e.SubastaId == subastaId)
                            .ToListAsync();

            foreach (ArticuloArte arte in articulosArte)
            {
                var arteSubastaVM = new ProductoSubastaVM();
                arteSubastaVM.SubastaId = subastaId;
                arteSubastaVM.ArticuloId = arte.Id;
                arteSubastaVM.Nombre = arte.Nombre;
                arteSubastaVM.PrecioInicial = arte.PrecioInicial;
                arteSubastaVM.TipoArticulo = TipoArticulo.Arte;
                arteSubastaVM.NombreImagen = arte.NombreImagen;
                arteSubastaVM.PrecioInicial = arte.PrecioInicial;
                arteSubastaVM.PrecioEnPuja = arte.PrecioEnPuja;
                listaProductos.Add(arteSubastaVM);
            }

            var articulosMueble = await _context.ArticulosMueble
                            .Where(e => e.SubastaId == subastaId)
                            .ToListAsync();

            foreach (ArticuloMueble mueble in articulosMueble)
            {
                var muebleSubastaVM = new ProductoSubastaVM();
                muebleSubastaVM.SubastaId = subastaId;
                muebleSubastaVM.ArticuloId = mueble.Id;
                muebleSubastaVM.Nombre = mueble.Nombre;
                muebleSubastaVM.PrecioInicial = mueble.PrecioInicial;
                muebleSubastaVM.FechaCreacion = mueble.FechaCreacion;
                muebleSubastaVM.TipoArticulo = TipoArticulo.Mueble;
                muebleSubastaVM.NombreImagen = mueble.NombreImagen;
                muebleSubastaVM.PrecioInicial = mueble.PrecioInicial;
                muebleSubastaVM.PrecioEnPuja = mueble.PrecioEnPuja;

                listaProductos.Add(muebleSubastaVM);
            }

            return listaProductos;
        }
    }
}
