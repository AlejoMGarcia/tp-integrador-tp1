using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCBasico.Context;
using MVCBasico.Models;

namespace MVCBasico.Controllers
{
    public class ArticuloMuebleController : Controller
    {
        private readonly SubastaDatabaseContext _context;

        public ArticuloMuebleController(SubastaDatabaseContext context)
        {
            _context = context;
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
        public async Task<IActionResult> Create([Bind("Alto,Ancho,Material,Peso,Tipo,Fabricante,Id,Nombre,PrecioInicial,PrecioMinimo,PrecioEnPuja")] ArticuloMueble articuloMueble)
        {
            if (ModelState.IsValid)
            {
                _context.Add(articuloMueble);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
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
        public async Task<IActionResult> Edit(int id, [Bind("Alto,Ancho,Material,Peso,Tipo,Fabricante,Id,Nombre,PrecioInicial,PrecioMinimo,PrecioEnPuja")] ArticuloMueble articuloMueble)
        {
            if (id != articuloMueble.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
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
    }
}
