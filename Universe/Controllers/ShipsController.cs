using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using BLL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Universe.Models;
using Universe.Models.discoverer;
using Universe.Models.planet;
using Universe.Models.ship;
using Universe.Models.star;
using Universe.Models.starsystem;

namespace Universe.Controllers
{
    public class ShipsController : Controller
    {
        private readonly ISpaceObjectService<Ship> _service;

        public ShipsController(ISpaceObjectService<Ship> context)
        {
            _service = context;
        }

        // GET: Ships
        public async Task<IActionResult> Index()
        {
            var ship = await _service.GetAllSpaceObjectsAsync(); //TU
            if (ship == null)
            {
                return NotFound();
            }
            return View(ship);
        }

        // GET: Ships/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) // TU
            {
                return NotFound();
            }
            var ship = await _service.GetSpaceObjectByIdAsync((int)id);
            if (ship == null)
            {
                return NotFound();
            }

            return View(ship);
        }

        // POST: Ships/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ShipId,ShipName,ShipModel,MaxSpeed,SingleChargeRange")] Ship ship)
        {
            if (ModelState.IsValid)
            {
                await _service.AddSpaceObjectAsync(ship);   //TU
                return RedirectToAction(nameof(Index));
            }
            //ViewData["DiscovererId"] = new SelectList(_unitOfWork.GetRepository<Discoverer>().GetList(), "DiscovererId", "Name", ship.Id);
            return View(ship);
        }

        // GET: Ships/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var ship = await _service.GetSpaceObjectByIdAsync(id);
            if (ship == null)
            {
                return NotFound();
            }
            // ogarnac to

            //if (discoverer.ShipId != null)
            //    ViewData["ShipId"] = new SelectList(_unitOfWork.GetRepository<Ship>().Include(m => m.Discoverer), "DiscovererId", "Name", discoverer.ShipId);
            //else
            //    ViewData["ShipId"] = new SelectList(new ArrayList(0));
            return View(ship);
        }

        // POST: Ships/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ShipId,ShipName,ShipModel,MaxSpeed,SingleChargeRange")] Ship ship)
        {
            if (id != ship.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _service.UpdateSpaceObjectAsync(ship);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShipExists(ship.Id))
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
            return View(ship);
        }

        // GET: Ships/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var ship = await _service.GetSpaceObjectByIdAsync(id);
            if (ship == null)
            {
                return NotFound();
            }

            return View(ship);
        }

        // POST: Ships/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ship = await _service.GetSpaceObjectByIdAsync(id);
            if (ship != null)
            {
                await _service.RemoveSpaceObjectAsync(id);
            }
            return RedirectToAction(nameof(Index));
        }
        private bool ShipExists(int id)
        {
            return _service.SpaceObjectExists(id);
        }
    }
}
