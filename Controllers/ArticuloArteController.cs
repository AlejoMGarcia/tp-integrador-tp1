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
    public class ArticuloArteController : Controller
    {
        private readonly SubastaDatabaseContext _context;

        public ArticuloArteController(SubastaDatabaseContext context)
        {
            _context = context;
        }

        // GET: ArticuloArte
        public async Task<IActionResult> Index()
        {
            return View(await _context.ArticulosArte.ToListAsync());
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
        public async Task<IActionResult> Create([Bind("Artista,Periodo,TipoArte,Id,Nombre,PrecioInicial,PrecioMinimo,PrecioEnPuja")] ArticuloArte articuloArte)
        {
            if (ModelState.IsValid)
            {
                _context.Add(articuloArte);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
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
        public async Task<IActionResult> Edit(int id, [Bind("Artista,Periodo,TipoArte,Id,Nombre,PrecioInicial,PrecioMinimo,PrecioEnPuja")] ArticuloArte articuloArte)
        {
            if (id != articuloArte.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
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
    }
}
