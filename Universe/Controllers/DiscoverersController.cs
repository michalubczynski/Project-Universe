using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using BLL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Universe.Models;
using Universe.Models.discoverer;
using Universe.Models.galaxy;
using Universe.Models.ship;

namespace Universe.Controllers
{
    public class DiscoverersController : Controller
    {
        private readonly ISpaceObjectService<Discoverer> _discoverersService;

        public DiscoverersController(ISpaceObjectService<Discoverer> context)
        {
            _discoverersService = context;
        }

        // GET: Discoverers
        public async Task<IActionResult> Index()
        {
            var dis = await _discoverersService.GetAllSpaceObjectsAsync(); //TU

            if (dis == null)
            {
                return NotFound();
            }
            return View(dis);
        }

        // GET: Discoverers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _discoverersService.GetSpaceObjectByIdAsync(id) == null) // TU
            {
                return NotFound();
            }
            var galaxy = await _discoverersService.GetSpaceObjectByIdAsync((int)id);
            if (galaxy == null)
            {
                return NotFound();
            }

            return View(galaxy);
        }
        // POST: Discoverers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DiscovererId,Name,Surname,Age,ShipId")] Discoverer discoverer)
        {
            if (ModelState.IsValid)
            {
                await _discoverersService.AddSpaceObjectAsync(discoverer);   //TU
                return RedirectToAction(nameof(Index));
            }
            return View(discoverer);
        }

        // GET: Discoverers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _discoverersService.GetSpaceObjectByIdAsync(id) == null)
            {
                return NotFound();
            }

            var galaxy = await _discoverersService.GetSpaceObjectByIdAsync(id);
            if (galaxy == null)
            {
                return NotFound();
            }
            return View(galaxy);
        }

        // POST: Discoverers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DiscovererId,Name,Surname,Age,ShipId")] Discoverer discoverer)
        {
            if (id != discoverer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _discoverersService.UpdateSpaceObjectAsync(discoverer);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DiscovererExists(discoverer.Id))
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
            // ogarnac to

            //if (discoverer.ShipId != null)
            //    ViewData["ShipId"] = new SelectList(_unitOfWork.GetRepository<Ship>().Include(m => m.Discoverer), "DiscovererId", "Name", discoverer.ShipId);
            //else
            //    ViewData["ShipId"] = new SelectList(new ArrayList(0));
            return View(discoverer);
        }

        // GET: Discoverers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _discoverersService.GetSpaceObjectByIdAsync(id) == null)
            {
                return NotFound();
            }
            else // To poprawiono ale nie zostalo to wykonane jawnie !! poprawić w innych projektach metode delete bo nigdzie wczesniej nie usuwala tej encji
            {
                await _discoverersService.RemoveSpaceObjectAsync((int)id);
            }
            var galaxy = await _discoverersService.GetSpaceObjectByIdAsync(id);
            if (galaxy == null)
            {
                return NotFound();
            }

            return View(galaxy);
        }

        // POST: Discoverers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_discoverersService.GetSpaceObjectByIdAsync(id) == null)
            {
                return Problem("Entity set 'DbUniverse.Galaxies'  is null.");
            }
            var galaxy = await _discoverersService.GetSpaceObjectByIdAsync(id);
            if (galaxy != null)
            {
                await _discoverersService.RemoveSpaceObjectAsync(id);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool DiscovererExists(int id)
        {
            return _discoverersService.SpaceObjectExists(id);
        }
    }
}
