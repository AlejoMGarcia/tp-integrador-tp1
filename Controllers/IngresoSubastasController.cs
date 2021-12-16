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
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace MVCBasico.Controllers
{
    public class IngresoSubastasController : Controller
    {
        private readonly SubastaDatabaseContext _context;
        private IHostingEnvironment _environment;

        public IngresoSubastasController(SubastaDatabaseContext context, IHostingEnvironment environment)
        {
            _context = context;
            _environment = environment;
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
                ingresoSubastaVM.SubastaId = subasta.Id;

                var articulosArte = await _context.ArticulosArte
                .Where(a => a.SubastaId == subasta.Id).ToListAsync();
                int cantidadArticulosArte = articulosArte.Count();
                ingresoSubastaVM.articulos.AddRange(articulosArte);

                var articulosMueble = await _context.ArticulosMueble
                .Where(a => a.SubastaId == subasta.Id).ToListAsync();
                int cantidadArticulosMueble = articulosMueble.Count();
                ingresoSubastaVM.articulos.AddRange(articulosMueble);

                ingresoSubastaVM.CantidadProductosArte = cantidadArticulosArte;
                ingresoSubastaVM.CantidadProductosMueble = cantidadArticulosMueble;
                ingresoSubastaVM.CantidadProductos = cantidadArticulosMueble + cantidadArticulosArte;
                
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
            var loginUser = HttpContext.Session.Get<Usuario>("_LoginUser");
            var userDb = _context.Usuarios.Find(loginUser.Id);

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
                arteSubastaVM.PrecioMinimo = arte.PrecioMinimo;
                arteSubastaVM.PrecioEnPuja = arte.PrecioEnPuja;
                arteSubastaVM.Artista = arte.Artista;
                arteSubastaVM.Periodo = arte.Periodo;
                arteSubastaVM.TipoArte = arte.TipoArte;
                arteSubastaVM.Saldo = userDb.Saldo;

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
                muebleSubastaVM.PrecioMinimo = mueble.PrecioMinimo;
                muebleSubastaVM.PrecioEnPuja = mueble.PrecioEnPuja;
                muebleSubastaVM.Peso = mueble.Peso;
                muebleSubastaVM.Alto = mueble.Alto;
                muebleSubastaVM.Ancho = mueble.Ancho;
                muebleSubastaVM.Material = mueble.Material;
                muebleSubastaVM.Fabricante = mueble.Fabricante;
                muebleSubastaVM.Tipo = mueble.Tipo;
                muebleSubastaVM.Saldo = userDb.Saldo;

                listaProductos.Add(muebleSubastaVM);
            }

            return listaProductos;
        }
        public IActionResult GetImage(int id)
        {
            Articulo articulo = _context.ArticulosArte.Find(id);
            if (articulo == null)
            {
                articulo = _context.ArticulosMueble.Find(id);
            }

            if (articulo != null)
            {
                string webRootpath = _environment.WebRootPath;
                string folderPath = "\\images\\";
                string fullPath = webRootpath + folderPath + articulo.NombreImagen;
                if (System.IO.File.Exists(fullPath))
                {
                    FileStream fileOnDisk = new FileStream(fullPath, FileMode.Open);
                    byte[] fileBytes;
                    using (BinaryReader br = new BinaryReader(fileOnDisk))
                    {
                        fileBytes = br.ReadBytes((int)fileOnDisk.Length);
                    }
                    return File(fileBytes, articulo.TipoImagen);
                }
                else
                {
                    if (articulo.ArchivoImagen.Length > 0)
                    {
                        return File(articulo.ArchivoImagen, articulo.TipoImagen);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<ActionResult> PushPriceProductAsync(int subastaId, int articuloId, double precioPuja)
        {
            if (HttpContext.Session.Get<Usuario>("_LoginUser") != default)
            {
                var loginUser = HttpContext.Session.Get<Usuario>("_LoginUser");
                var userDb = _context.Usuarios.Find(loginUser.Id);
                userDb.Saldo = userDb.Saldo - precioPuja;

                var pujaProducto = new PujaProducto();
                pujaProducto.UsuarioIdPuja = loginUser.Id;
                pujaProducto.PrecioPuja = precioPuja;
                pujaProducto.ArticuloId = articuloId;

                Articulo articulo = null;
                articulo = _context.ArticulosArte.Find(articuloId);
                if (articulo == null)
                {
                    articulo = _context.ArticulosMueble.Find(articuloId);
                }

                if (articulo != null)
                {
                    articulo.PrecioEnPuja = precioPuja;
                }

                _context.Update(userDb);
                _context.Update(articulo);
                _context.Add(pujaProducto);

                await _context.SaveChangesAsync();
                return RedirectToAction("GetInto", new { subastaId = subastaId });
            } 

            return BadRequest();
        }
    }
}
