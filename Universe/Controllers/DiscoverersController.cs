﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Universe.Models;
using Universe.Models.discoverer;
using Universe.Models.ship;

namespace Universe.Controllers
{
    public class DiscoverersController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public DiscoverersController(IUnitOfWork context)
        {
            _unitOfWork = context;
        }

        // GET: Discoverers
        public async Task<IActionResult> Index()
        {
            var dis = await _unitOfWork.GetRepository<Discoverer>().GetListAsync(); //TU

            if (dis == null)
            {
                return NotFound();
            }
            return View(dis);
        }

        // GET: Discoverers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _unitOfWork.GetRepository<Discoverer>() == null)
            {
                return NotFound();
            }

            var dis = await _unitOfWork.GetRepository<Discoverer>()
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dis == null)
            {
                return NotFound();
            }

            return View(dis);
        }

        // GET: Discoverers/Create
        public IActionResult Create()
        {
            ViewData["DiscovererId"] = new SelectList(_unitOfWork.GetRepository<Discoverer>().GetList(), "DiscovererId", "Name");
            return View();
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
                _unitOfWork.GetRepository<Discoverer>().Insert(discoverer);
                await _unitOfWork.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ShipId"] = new SelectList(_unitOfWork.GetRepository<Ship>()?.GetList(), "DiscovererId", "Name", discoverer.ShipId);
            return View(discoverer);
        }

        // GET: Discoverers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _unitOfWork.GetRepository<Discoverer>() == null)
            {
                return NotFound();
            }

            var dis = await _unitOfWork.GetRepository<Discoverer>().GetByIDAsync(id);
            if (dis == null)
            {
                return NotFound();
            }
            if (dis.ShipId != null)
                ViewData["ShipId"] = new SelectList(_unitOfWork.GetRepository<Ship>().Include(m => m.Discoverer), "DiscovererId", "Name", dis.ShipId);
            else
                ViewData["ShipId"] = new SelectList(new ArrayList(0));
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
                    _unitOfWork.Update(discoverer);
                    await _unitOfWork.SaveChangesAsync();
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
            if (discoverer.ShipId != null)
                ViewData["ShipId"] = new SelectList(_unitOfWork.GetRepository<Ship>().Include(m => m.Discoverer), "DiscovererId", "Name", discoverer.ShipId);
            else
                ViewData["ShipId"] = new SelectList(new ArrayList(0));
            return View(discoverer);
        }

        // GET: Discoverers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _unitOfWork.GetRepository<Discoverer>() == null)
            {
                return NotFound();
            }

            var dis = await _unitOfWork.GetRepository<Discoverer>().FirstOrDefaultAsync(m => m.Id == id);

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
            if (_unitOfWork.GetRepository<Discoverer>() == null)
            {
                return Problem("Entity set 'DbUniverse.Discoverers'  is null.");
            }
            var dis = await _unitOfWork.GetRepository<Discoverer>().GetByIDAsync(id);
            if (dis != null)
            {
                _unitOfWork.GetRepository<Discoverer>().Delete(id);
            }

            await _unitOfWork.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DiscovererExists(int id)
        {
            return (_unitOfWork.GetRepository<Discoverer>()?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
