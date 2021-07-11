using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCBasico.Context;
using MVCBasico.Models;
using MVCBasico.Utils;

namespace MVCBasico.Controllers
{
    public class ArticuloArteController : Controller
    {
        private readonly SubastaDatabaseContext _context;
        private IHostingEnvironment _environment;
        private byte[] usuario;

        public ArticuloArteController(SubastaDatabaseContext context, IHostingEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: ArticuloArte
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.Get<Usuario>("_LoginUser") != default)
            {
                var loginUser = HttpContext.Session.Get<Usuario>("_LoginUser");
                return View(await _context.ArticulosArte
                            .Where(e => e.UsuarioCreadorId == loginUser.Id).ToListAsync());
            }
            else
            {
                return BadRequest();
            }
        }

        // GET: ArticuloArte/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var articuloArte = await _context.ArticulosArte
                .FirstOrDefaultAsync(m => m.Id == id);
            if (articuloArte == null)
            {
                return NotFound();
            }

            return View(articuloArte);
        }

        // GET: ArticuloArte/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ArticuloArte/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Artista,Periodo,TipoArte,Id,Nombre,PrecioInicial,PrecioMinimo,PrecioEnPuja,ArchivoFoto")] ArticuloArte articuloArte)
        {
            if (ModelState.IsValid)
            {
                if (HttpContext.Session.Get<Usuario>("_LoginUser") != default)
                {
                    var loginUser = HttpContext.Session.Get<Usuario>("_LoginUser");
                
                    articuloArte.UsuarioCreadorId = loginUser.Id;
                    articuloArte.FechaCreacion = DateTime.Today;
                    if (articuloArte.ArchivoFoto != null && articuloArte.ArchivoFoto.Length > 0)
                    {
                        articuloArte.TipoImagen = articuloArte.ArchivoFoto.ContentType;
                        articuloArte.NombreImagen = Path.GetFileName(articuloArte.ArchivoFoto.FileName);
                        using (var memoryStream = new MemoryStream())
                        {
                            articuloArte.ArchivoFoto.CopyTo(memoryStream);
                            articuloArte.ArchivoImagen = memoryStream.ToArray();
                        }
                        _context.Add(articuloArte);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            return View(articuloArte);
        }

        // GET: ArticuloArte/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var articuloArte = await _context.ArticulosArte.FindAsync(id);
            if (articuloArte == null)
            {
                return NotFound();
            }
            return View(articuloArte);
        }

        // POST: ArticuloArte/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Artista,Periodo,TipoArte,Id,Nombre,UsuarioCreadorId,FechaCreacion,FechaModificacion,PrecioInicial,PrecioMinimo,PrecioEnPuja,ArchivoFoto")] ArticuloArte articuloArte)
        {
            if (id != articuloArte.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    articuloArte.FechaModificacion = DateTime.Today;
                    if (articuloArte.ArchivoFoto != null && articuloArte.ArchivoFoto.Length > 0)
                    {
                        articuloArte.TipoImagen = articuloArte.ArchivoFoto.ContentType;
                        articuloArte.NombreImagen = Path.GetFileName(articuloArte.ArchivoFoto.FileName);
                        using (var memoryStream = new MemoryStream())
                        {
                            articuloArte.ArchivoFoto.CopyTo(memoryStream);
                            articuloArte.ArchivoImagen = memoryStream.ToArray();
                        }
                    }
                    _context.Update(articuloArte);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArticuloArteExists(articuloArte.Id))
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
            return View(articuloArte);
        }

        // GET: ArticuloArte/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var articuloArte = await _context.ArticulosArte
                .FirstOrDefaultAsync(m => m.Id == id);
            if (articuloArte == null)
            {
                return NotFound();
            }

            return View(articuloArte);
        }

        // POST: ArticuloArte/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var articuloArte = await _context.ArticulosArte.FindAsync(id);
            _context.ArticulosArte.Remove(articuloArte);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArticuloArteExists(int id)
        {
            return _context.ArticulosArte.Any(e => e.Id == id);
        }

        public IActionResult GetImage(int id)
        {
            var articulo = _context.ArticulosArte.Find(id);
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
    }
}
