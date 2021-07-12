using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCBasico.Context;
using MVCBasico.Models;
using MVCBasico.Utils;

namespace MVCBasico.Controllers
{
    public class SubastaController : Controller
    {
        private readonly SubastaDatabaseContext _context;

        public SubastaController(SubastaDatabaseContext context)
        {
            _context = context;
        }

        // GET: Subasta
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.Get<Usuario>("_LoginUser") != default)
            {
                var loginUser = HttpContext.Session.Get<Usuario>("_LoginUser");
                return View(await _context.Subastas
                            .Where(e => e.UsuarioId == loginUser.Id).ToListAsync());
            }
            else
            {
                return BadRequest();
            }
        }

        // GET: Subasta/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subasta = await _context.Subastas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (subasta == null)
            {
                return NotFound();
            }

            return View(subasta);
        }

        // GET: Subasta/Create
        public IActionResult Create()
        {
            Subasta subasta = new Subasta();
            return View(subasta);
        }

        // POST: Subasta/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Codigo,FechaInicio,FechaFinalizacion,Activo")] Subasta subasta)
        {
            if (ModelState.IsValid)
            {
                if (HttpContext.Session.Get<Usuario>("_LoginUser") != default)
                {
                    var loginUser = HttpContext.Session.Get<Usuario>("_LoginUser");

                    subasta.UsuarioId = loginUser.Id;
                    subasta.Activa = true;
                    _context.Add(subasta);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(subasta);
        }

        // GET: Subasta/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subasta = await _context.Subastas.FindAsync(id);
            if (subasta == null)
            {
                return NotFound();
            }
            return View(subasta);
        }

        // POST: Subasta/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Codigo,FechaInicio,FechaFinalizacion,Activo,UsuarioId")] Subasta subasta)
        {
            if (id != subasta.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(subasta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubastaExists(subasta.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(subasta);
        }

        // GET: Subasta/Deactivate/5
        public async Task<IActionResult> Deactivate(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subasta = await _context.Subastas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (subasta == null)
            {
                return NotFound();
            }

            return View(subasta);
        }

        // POST: Subasta/Deactivate/5
        [HttpPost, ActionName("Deactivate")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeactivateConfirmed(int id)
        {
            var subasta = await _context.Subastas.FindAsync(id);
            subasta.Activa = false;
            _context.Update(subasta);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Subasta/Deactivate/5
        public async Task<IActionResult> Activate(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subasta = await _context.Subastas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (subasta == null)
            {
                return NotFound();
            }

            return View(subasta);
        }

        // POST: Subasta/Activate/5
        [HttpPost, ActionName("Activate")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ActivateConfirmed(int id)
        {
            var subasta = await _context.Subastas.FindAsync(id);
            subasta.Activa = true;
            _context.Update(subasta);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SubastaExists(int id)
        {
            return _context.Subastas.Any(e => e.Id == id);
        }


        // GET: Subasta/AddedProduct/5
        public async Task<IActionResult> AddedProduct(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var subasta = await _context.Subastas.FirstOrDefaultAsync(m => m.Id == Id);
            if (subasta == null)
            {
                return NotFound();
            }

            if (HttpContext.Session.Get<Usuario>("_LoginUser") != default)
            {
                var loginUser = HttpContext.Session.Get<Usuario>("_LoginUser");
                List<ProductoSubastaVM> listaProductosSubasta = await BuildListaProductSubastaVM(loginUser.Id, subasta);

                return View(listaProductosSubasta);
            }
            else
            {
                return BadRequest();
            }
        }

        private async Task<List<ProductoSubastaVM>> BuildListaProductSubastaVM(int usuarioId, Subasta subasta)
        {
            List<ProductoSubastaVM> listaProductos = new List<ProductoSubastaVM>();

            var articulosArte = await _context.ArticulosArte
                            .Where(e => e.UsuarioCreadorId == usuarioId
                            && (e.SubastaId == null || e.SubastaId == subasta.Id))
                            .ToListAsync();

            foreach(ArticuloArte arte in articulosArte){
                var arteSubastaVM = new ProductoSubastaVM();
                arteSubastaVM.SubastaId = subasta.Id;
                arteSubastaVM.ArticuloId = arte.Id;
                arteSubastaVM.Nombre = arte.Nombre;
                arteSubastaVM.PrecioInicial = arte.PrecioInicial;
                arteSubastaVM.FechaCreacion = arte.FechaCreacion;
                arteSubastaVM.Included = (arte.SubastaId != null);
                arteSubastaVM.TipoArticulo = TipoArticulo.Arte;
                arteSubastaVM.NombreImagen = arte.NombreImagen;
                listaProductos.Add(arteSubastaVM);
            }

            var articulosMueble = await _context.ArticulosMueble
                        .Where(e => e.UsuarioCreadorId == usuarioId
                        && (e.SubastaId == null || e.SubastaId == subasta.Id))
                        .ToListAsync();

            foreach (ArticuloMueble mueble in articulosMueble)
            {
                var muebleSubastaVM = new ProductoSubastaVM();
                muebleSubastaVM.SubastaId = subasta.Id;
                muebleSubastaVM.ArticuloId = mueble.Id;
                muebleSubastaVM.Nombre = mueble.Nombre;
                muebleSubastaVM.PrecioInicial = mueble.PrecioInicial;
                muebleSubastaVM.FechaCreacion = mueble.FechaCreacion;
                muebleSubastaVM.Included = (mueble.SubastaId != null);
                muebleSubastaVM.TipoArticulo = TipoArticulo.Mueble;
                muebleSubastaVM.NombreImagen = mueble.NombreImagen;

                listaProductos.Add(muebleSubastaVM);
            }

            return listaProductos;
        }

        // GET: Subasta/AddedProductConfirm/5/Arte/5
        [HttpGet, ActionName("AddedProductConfirm")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddedProductConfirm(int? subastaId, string? tipoArticulo, int? articuloId)
        {
            Articulo articulo = null;

            if (TipoArticulo.Mueble.Equals(tipoArticulo)){
                articulo = await _context.ArticulosMueble
                        .Where(e => e.Id == articuloId).SingleOrDefaultAsync();
            }
            else
            {
                articulo = await _context.ArticulosArte
                        .Where(e => e.Id == articuloId).SingleOrDefaultAsync();
            }

            if (articulo != null)
            {
                articulo.SubastaId = subastaId;
                _context.Update(articulo);
                await _context.SaveChangesAsync();
                return RedirectToAction("AddedProduct", subastaId);
            }
            else
            {
                NotFound();
            }

            return BadRequest();
        }

        // GET: Subasta/DeletedProductConfirm/5/Arte/5
        [HttpGet, ActionName("DeletedProductConfirm")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletedProductConfirm(int? subastaId, string? tipoArticulo, int? articuloId)
        {
            Articulo articulo = null;

            if (TipoArticulo.Mueble.Equals(tipoArticulo))
            {
                articulo = await _context.ArticulosMueble
                        .Where(e => e.Id        == articuloId && 
                                    e.SubastaId == subastaId)
                        .SingleOrDefaultAsync();
            }
            else
            {
                articulo = await _context.ArticulosArte
                        .Where(e => e.Id        == articuloId 
                                 && e.SubastaId == subastaId)
                        .SingleOrDefaultAsync();
            }

            if (articulo != null)
            {
                articulo.SubastaId = null;
                _context.Update(articulo);
                await _context.SaveChangesAsync();
                return RedirectToAction("AddedProduct", subastaId);
            }
            else
            {
                NotFound();
            }

            return BadRequest();
        }
    }
}
