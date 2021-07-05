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
            return View(await _context.Subastas.ToListAsync());
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
                subasta.Activa = true;
                _context.Add(subasta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Codigo,FechaInicio,FechaFinalizacion,Activo")] Subasta subasta)
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
    }
}
