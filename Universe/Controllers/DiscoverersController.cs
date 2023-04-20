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
        private readonly ISpaceObjectService<Discoverer> _service;

        public DiscoverersController(ISpaceObjectService<Discoverer> context)
        {
            _service = context;
        }

        // GET: Discoverers
        public async Task<IActionResult> Index()
        {
            var dis = await _service.GetAllSpaceObjectsAsync(); //TU

            if (dis == null)
            {
                return NotFound();
            }
            return View(dis);
        }

        // GET: Discoverers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) // TU
            {
                return NotFound();
            }
            var dis = await _service.GetSpaceObjectByIdAsync((int)id);
            if (dis == null)
            {
                return NotFound();
            }

            return View(dis);
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
                await _service.AddSpaceObjectAsync(discoverer);   //TU
                return RedirectToAction(nameof(Index));
            }
            return View(discoverer);
        }

        // GET: Discoverers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var dis = await _service.GetSpaceObjectByIdAsync(id);
            if (dis == null)
            {
                return NotFound();
            }
            return View(dis);
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
                    await _service.UpdateSpaceObjectAsync(discoverer);
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
            if (id == null)
            {
                return NotFound();
            }
            var dis = await _service.GetSpaceObjectByIdAsync(id);
            if (dis == null)
            {
                return NotFound();
            }

            return View(dis);
        }

        // POST: Discoverers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dis = await _service.GetSpaceObjectByIdAsync(id);
            if (dis != null)
            {
                await _service.RemoveSpaceObjectAsync(id);
            }
            return RedirectToAction(nameof(Index));
        }

        private bool DiscovererExists(int id)
        {
            return _service.SpaceObjectExists(id);
        }
    }
}
