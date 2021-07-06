using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCBasico.Context;
using MVCBasico.Models;

namespace MVCBasico.Controllers
{
    public class ArticuloMuebleController : Controller
    {
        private readonly SubastaDatabaseContext _context;
        private IHostingEnvironment _environment;

        public ArticuloMuebleController(SubastaDatabaseContext context, IHostingEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: ArticuloMueble
        public async Task<IActionResult> Index()
        {
            return View(await _context.ArticulosMueble.ToListAsync());
        }

        // GET: ArticuloMueble/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var articuloMueble = await _context.ArticulosMueble
                .FirstOrDefaultAsync(m => m.Id == id);
            if (articuloMueble == null)
            {
                return NotFound();
            }

            return View(articuloMueble);
        }

        // GET: ArticuloMueble/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ArticuloMueble/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Alto,Ancho,Material,Peso,Tipo,Fabricante,Id,Nombre,PrecioInicial,PrecioMinimo,PrecioEnPuja,ArchivoFoto")] ArticuloMueble articuloMueble)
        {
            if (ModelState.IsValid)
            {
                articuloMueble.FechaCreacion = DateTime.Today;
                if (articuloMueble.ArchivoFoto != null && articuloMueble.ArchivoFoto.Length > 0)
                {
                    articuloMueble.TipoImagen = articuloMueble.ArchivoFoto.ContentType;
                    articuloMueble.NombreImagen = Path.GetFileName(articuloMueble.ArchivoFoto.FileName);
                    using (var memoryStream = new MemoryStream())
                    {
                        articuloMueble.ArchivoFoto.CopyTo(memoryStream);
                        articuloMueble.ArchivoImagen = memoryStream.ToArray();
                    }
                    _context.Add(articuloMueble);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(articuloMueble);
        }

        // GET: ArticuloMueble/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var articuloMueble = await _context.ArticulosMueble.FindAsync(id);
            if (articuloMueble == null)
            {
                return NotFound();
            }
            return View(articuloMueble);
        }

        // POST: ArticuloMueble/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Alto,Ancho,Material,Peso,Tipo,Fabricante,Id,Nombre,UsuarioCreador,FechaCreacion,FechaModificacion,PrecioInicial,PrecioMinimo,PrecioEnPuja,ArchivoFoto")] ArticuloMueble articuloMueble)
        {
            if (id != articuloMueble.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    articuloMueble.FechaModificacion = DateTime.Today;
                    if (articuloMueble.ArchivoFoto != null && articuloMueble.ArchivoFoto.Length > 0)
                    {
                        articuloMueble.TipoImagen = articuloMueble.ArchivoFoto.ContentType;
                        articuloMueble.NombreImagen = Path.GetFileName(articuloMueble.ArchivoFoto.FileName);
                        using (var memoryStream = new MemoryStream())
                        {
                            articuloMueble.ArchivoFoto.CopyTo(memoryStream);
                            articuloMueble.ArchivoImagen = memoryStream.ToArray();
                        }
                    }
                    _context.Update(articuloMueble);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArticuloMuebleExists(articuloMueble.Id))
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
            return View(articuloMueble);
        }

        // GET: ArticuloMueble/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var articuloMueble = await _context.ArticulosMueble
                .FirstOrDefaultAsync(m => m.Id == id);
            if (articuloMueble == null)
            {
                return NotFound();
            }

            return View(articuloMueble);
        }

        // POST: ArticuloMueble/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var articuloMueble = await _context.ArticulosMueble.FindAsync(id);
            _context.ArticulosMueble.Remove(articuloMueble);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArticuloMuebleExists(int id)
        {
            return _context.ArticulosMueble.Any(e => e.Id == id);
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
