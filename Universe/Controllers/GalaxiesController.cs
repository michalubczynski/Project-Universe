using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Universe.Models;
using Universe.Models.galaxy;

namespace Universe.Controllers
{
    public class GalaxiesController : Controller
    {
        private readonly ISpaceObjectService<Galaxy> _galaxyService;
        public GalaxiesController(ISpaceObjectService<Galaxy> galaxyService)
        {
            _galaxyService = galaxyService;
        }

        // GET: Galaxies
        public async Task<IActionResult> Index()
        {
            var galaxies = await _galaxyService.GetAllSpaceObjectsAsync(); //TU

            if (galaxies == null)
            {
                return NotFound();
            } //TU

            return View(galaxies);
        }

        // GET: Galaxies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _galaxyService.GetSpaceObjectByIdAsync(id) == null) // TU
            {
                return NotFound();
            }
            var galaxy = await _galaxyService.GetSpaceObjectByIdAsync((int)id);
            if (galaxy == null)
            {
                return NotFound();
            }

            return View(galaxy);
        }

        // GET: Galaxies/Create
        // POST: Galaxies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GalaxyId,Name,Type,Mass")] Galaxy galaxy)
        {
            if (ModelState.IsValid)
            {
                await _galaxyService.AddSpaceObjectAsync(galaxy);   //TU
                return RedirectToAction(nameof(Index));
            }
            return View(galaxy);
        }

        // GET: Galaxies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _galaxyService.GetSpaceObjectByIdAsync(id) == null)
            {
                return NotFound();
            }

            var galaxy = await _galaxyService.GetSpaceObjectByIdAsync(id);
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
            if (id != galaxy.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _galaxyService.UpdateSpaceObjectAsync(galaxy);
                    //await _unitOfWork.GetRepository<Galaxy>().SaveAsync(); // Może być bez tego bo wywoływana jest w UpdateGalaxyAsync()
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GalaxyExists(galaxy.Id))
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
            if (id == null || _galaxyService.GetSpaceObjectByIdAsync(id) == null)
            {
                return NotFound();
            }
            else // To poprawiono ale nie zostalo to wykonane jawnie !! poprawić w innych projektach metode delete bo nigdzie wczesniej nie usuwala tej encji
            {
                await _galaxyService.RemoveSpaceObjectAsync((int)id);
            }
            var galaxy = await _galaxyService.GetSpaceObjectByIdAsync(id);
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
            if (_galaxyService.GetSpaceObjectByIdAsync(id) == null)
            {
                return Problem("Entity set 'DbUniverse.Galaxies'  is null.");
            }
            var galaxy = await _galaxyService.GetSpaceObjectByIdAsync(id);
            if (galaxy != null)
            {
                await _galaxyService.RemoveSpaceObjectAsync(id);
            }
            
            return RedirectToAction(nameof(Index));
        }

        private bool GalaxyExists(int id)
        {
            return _galaxyService.SpaceObjectExists(id);
        }
    }
}
