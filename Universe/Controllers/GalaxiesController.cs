using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Universe.Models;
using Universe.Models.galaxy;

namespace Universe.Controllers
{
    public class GalaxiesController : Controller
    {
        private readonly DbUniverse _context;
        private readonly IUnitOfWork _unitOfWork;

        public GalaxiesController(DbUniverse context)
        {
            _context = context;
        }

        // GET: Galaxies
        public async Task<IActionResult> Index()
        {
              return _context.Galaxies != null ? 
                View(await _context.Galaxies.ToListAsync()) :
                          Problem("Entity set 'DbUniverse.Galaxies'  is null.");
        }

        // GET: Galaxies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Galaxies == null)
            {
                return NotFound();
            }

            var galaxy = await _context.Galaxies
                .FirstOrDefaultAsync(m => m.GalaxyId == id);
            if (galaxy == null)
            {
                return NotFound();
            }

            return View(galaxy);
        }

        // GET: Galaxies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Galaxies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GalaxyId,Name,Type,Mass")] Galaxy galaxy)
        {
            if (ModelState.IsValid)
            {
                _context.Add(galaxy);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(galaxy);
        }

        // GET: Galaxies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Galaxies == null)
            {
                return NotFound();
            }

            var galaxy = await _context.Galaxies.FindAsync(id);
            if (galaxy == null)
            {
                return NotFound();
            }
            return View(galaxy);
        }

        // POST: Galaxies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GalaxyId,Name,Type,Mass")] Galaxy galaxy)
        {
            if (id != galaxy.GalaxyId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(galaxy);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GalaxyExists(galaxy.GalaxyId))
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
            return View(galaxy);
        }

        // GET: Galaxies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Galaxies == null)
            {
                return NotFound();
            }

            var galaxy = await _context.Galaxies
                .FirstOrDefaultAsync(m => m.GalaxyId == id);
            if (galaxy == null)
            {
                return NotFound();
            }

            return View(galaxy);
        }

        // POST: Galaxies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Galaxies == null)
            {
                return Problem("Entity set 'DbUniverse.Galaxies'  is null.");
            }
            var galaxy = await _context.Galaxies.FindAsync(id);
            if (galaxy != null)
            {
                _context.Galaxies.Remove(galaxy);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GalaxyExists(int id)
        {
          return (_context.Galaxies?.Any(e => e.GalaxyId == id)).GetValueOrDefault();
        }
    }
}
