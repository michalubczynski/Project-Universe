using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Universe.Models;
using Universe.Models.planet;
using Universe.Models.star;
using Universe.Models.starsystem;

namespace Universe.Controllers
{
    public class PlanetsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public PlanetsController(IUnitOfWork u)
        {
            _unitOfWork = u;
        }

        // GET: Planets
        public async Task<IActionResult> Index()
        {
            var planets = await _unitOfWork.GetRepository<Planet>().GetListAsync(); //TU

            if (planets == null)
            {
                return NotFound();
            }
            return View(planets);
        }

        // GET: Planets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _unitOfWork.GetRepository<Planet>() == null)
            {
                return NotFound();
            }

            var plantet = await _unitOfWork.GetRepository<Planet>()
                .Include(s => s.StarSystem)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (plantet == null)
            {
                return NotFound();
            }

            return View(plantet);
        }

        // GET: Planets/Create
        public IActionResult Create()
        {
            ViewData["PlanetId"] = new SelectList(_unitOfWork.GetRepository<StarSystem>().GetList(), "PlanetId", "Name");
            return View();
        }

        // POST: Planets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PlanetId,Name,Type,Mass,StarSystemId")] Planet planet)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.GetRepository<Planet>().Insert(planet);
                await _unitOfWork.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StarSystemId"] = new SelectList(_unitOfWork.GetRepository<StarSystem>().GetList(), "PlanetId", "Name", planet.Id);
            return View(planet);
        }

        // GET: Planets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _unitOfWork.GetRepository<Planet>() == null)
            {
                return NotFound();
            }

            var planet = await _unitOfWork.GetRepository<Planet>().GetByIDAsync(id);
            if (planet == null)
            {
                return NotFound();
            }
            ViewData["StarSystemId"] = new SelectList(_unitOfWork.GetRepository<StarSystem>().GetList(), "StarSystemId", "Name", planet.StarSystemId);
            return View(planet);
        }

        // POST: Planets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PlanetId,Name,Type,Mass,StarSystemId")] Planet planet)
        {
            if (id != planet.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.Update(planet);
                    await _unitOfWork.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlanetExists(planet.Id))
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
            ViewData["StarSystemId"] = new SelectList(_unitOfWork.GetRepository<StarSystem>().GetList(), "StarSysyemId", "Name", planet.StarSystemId);
            return View(planet);
        }

        // GET: Planets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _unitOfWork.GetRepository<Planet>() == null)
            {
                return NotFound();
            }

            var planet = await _unitOfWork.GetRepository<Planet>().Include(s => s.StarSystem).FirstOrDefaultAsync(m => m.Id == id);

            if (planet == null)
            {
                return NotFound();
            }

            return View(planet);
        }

        // POST: Planets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_unitOfWork.GetRepository<Planet>() == null)
            {
                return Problem("Entity set 'DbUniverse.Planets'  is null.");
            }
            var planet = await _unitOfWork.GetRepository<Planet>().GetByIDAsync(id);
            if (planet != null)
            {
                _unitOfWork.GetRepository<Planet>().Delete(id);
            }

            await _unitOfWork.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlanetExists(int id)
        {
            return (_unitOfWork.GetRepository<Planet>()?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
