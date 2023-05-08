using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Universe.Models;
using Universe.Models.discoverer;
using Universe.Models.galaxy;
using Universe.Models.planet;
using Universe.Models.star;
using Universe.Models.starsystem;

namespace Universe.Controllers
{
    public class PlanetsController : Controller
    {
        private readonly IEntityService<Planet> _service;

        public PlanetsController(IEntityService<Planet> u)
        {
            _service = u;
        }

        // GET: Planets
        public async Task<IActionResult> Index()
        {
            var planet = await _service.GetAllSpaceObjectsAsync(); //TU
            if (planet == null)
            {
                return NotFound();
            }
            return View(planet);
        }

        // GET: Planets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) // TU
            {
                return NotFound();
            }
            var planet = await _service.GetSpaceObjectByIdAsync((int)id);
            if (planet == null)
            {
                return NotFound();
            }

            return View(planet);
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
                await _service.AddSpaceObjectAsync(planet);   //TU
                return RedirectToAction(nameof(Index));
            }
            return View(planet);
        }

        // GET: Planets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var planet = await _service.GetSpaceObjectByIdAsync(id);
            if (planet == null)
            {
                return NotFound();
            }
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
                    await _service.UpdateSpaceObjectAsync(planet);
                    //await _unitOfWork.GetRepository<Galaxy>().SaveAsync(); // Może być bez tego bo wywoływana jest w UpdateGalaxyAsync()
                }
                catch (DbUpdateConcurrencyException)
                {
                    if(PlanetExists(planet.Id))
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
            //ViewData["StarSystemId"] = new SelectList(_unitOfWork.GetRepository<StarSystem>().GetList(), "StarSysyemId", "Name", planet.StarSystemId);
            return View(planet);
        }

        // GET: Planets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var planet = await _service.GetSpaceObjectByIdAsync(id);
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
            var planet = await _service.GetSpaceObjectByIdAsync(id);
            if (planet == null)
            {
                return Problem("Entity set 'DbUniverse.Galaxies'  is null.");
            }
            await _service.RemoveSpaceObjectAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private bool PlanetExists(int id)
        {
            return _service.SpaceObjectExists(id);
        }
    }
}
