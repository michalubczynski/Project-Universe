using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Models;
using Universe.Models.starsystem;

namespace Universe.Controllers
{
    public class StarSystems1Controller : Controller
    {
        private readonly DbUniverse _context;

        public StarSystems1Controller(DbUniverse context)
        {
            _context = context;
        }

        // GET: StarSystems1
        public async Task<IActionResult> Index()
        {
            var dbUniverse = _context.StarSystems.Include(s => s.Galaxy);
            return View(await dbUniverse.ToListAsync());
        }

        // GET: StarSystems1/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.StarSystems == null)
            {
                return NotFound();
            }

            var starSystem = await _context.StarSystems
                .Include(s => s.Galaxy)
                .FirstOrDefaultAsync(m => m.StarSystemId == id);
            if (starSystem == null)
            {
                return NotFound();
            }

            return View(starSystem);
        }

        // GET: StarSystems1/Create
        public IActionResult Create()
        {
            ViewData["GalaxyId"] = new SelectList(_context.Galaxies, "GalaxyId", "Name");
            return View();
        }

        // POST: StarSystems1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StarSystemId,Name,GalaxyId")] StarSystem starSystem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(starSystem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GalaxyId"] = new SelectList(_context.Galaxies, "GalaxyId", "Name", starSystem.GalaxyId);
            return View(starSystem);
        }

        // GET: StarSystems1/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.StarSystems == null)
            {
                return NotFound();
            }

            var starSystem = await _context.StarSystems.FindAsync(id);
            if (starSystem == null)
            {
                return NotFound();
            }
            ViewData["GalaxyId"] = new SelectList(_context.Galaxies, "GalaxyId", "Name", starSystem.GalaxyId);
            return View(starSystem);
        }

        // POST: StarSystems1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StarSystemId,Name,GalaxyId")] StarSystem starSystem)
        {
            if (id != starSystem.StarSystemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(starSystem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StarSystemExists(starSystem.StarSystemId))
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
            ViewData["GalaxyId"] = new SelectList(_context.Galaxies, "GalaxyId", "Name", starSystem.GalaxyId);
            return View(starSystem);
        }

        // GET: StarSystems1/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.StarSystems == null)
            {
                return NotFound();
            }

            var starSystem = await _context.StarSystems
                .Include(s => s.Galaxy)
                .FirstOrDefaultAsync(m => m.StarSystemId == id);
            if (starSystem == null)
            {
                return NotFound();
            }

            return View(starSystem);
        }

        // POST: StarSystems1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.StarSystems == null)
            {
                return Problem("Entity set 'DbUniverse.StarSystems'  is null.");
            }
            var starSystem = await _context.StarSystems.FindAsync(id);
            if (starSystem != null)
            {
                _context.StarSystems.Remove(starSystem);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StarSystemExists(int id)
        {
          return (_context.StarSystems?.Any(e => e.StarSystemId == id)).GetValueOrDefault();
        }
    }
}
