using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Universe.Models;
using Universe.Models.discoverer;
using Universe.Models.ship;
using Universe.Models.star;
using Universe.Models.starsystem;

namespace Universe.Controllers
{
    public class ShipsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ShipsController(IUnitOfWork context)
        {
            _unitOfWork = context;
        }

        // GET: Ships
        public async Task<IActionResult> Index()
        {
            var ships = await _unitOfWork.GetRepository<Ship>().GetListAsync(); //TU

            if (ships == null)
            {
                return NotFound();
            }
            return View(ships);
        }

        // GET: Ships/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _unitOfWork.GetRepository<Ship>() == null)
            {
                return NotFound();
            }

            var ship = await _unitOfWork.GetRepository<Ship>()
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ship == null)
            {
                return NotFound();
            }

            return View(ship);
        }

        // GET: Ships/Create
        public IActionResult Create()
        {
            ViewData["ShipId"] = new SelectList(_unitOfWork.GetRepository<Ship>().GetList(), "ShipId", "Name");
            return View();
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
                _unitOfWork.GetRepository<Ship>().Insert(ship);
                await _unitOfWork.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DiscovererId"] = new SelectList(_unitOfWork.GetRepository<Discoverer>().GetList(), "DiscovererId", "Name", ship.Id);
            return View(ship);
        }

        // GET: Ships/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _unitOfWork.GetRepository<Ship>() == null)
            {
                return NotFound();
            }

            var ship = await _unitOfWork.GetRepository<Ship>().GetByIDAsync(id);
            if (ship == null)
            {
                return NotFound();
            }
            ViewData["DiscovererId"] = new SelectList(_unitOfWork.GetRepository<Discoverer>().GetList(), "DiscovererId", "Name", ship.Discoverer);
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
                    _unitOfWork.Update(ship);
                    await _unitOfWork.SaveChangesAsync();
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
            ViewData["DiscovererId"] = new SelectList(_unitOfWork.GetRepository<Discoverer>().GetList(), "DiscovererId", "Name", ship.Discoverer);
            return View(ship);
        }
        // GET: Ships/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _unitOfWork.GetRepository<Ship>() == null)
            {
                return NotFound();
            }

            var ship = await _unitOfWork.GetRepository<Ship>().FirstOrDefaultAsync(m => m.Id == id);

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
            if (_unitOfWork.GetRepository<Ship>() == null)
            {
                return Problem("Entity set 'DbUniverse.Ships'  is null.");
            }
            var ship = await _unitOfWork.GetRepository<Ship>().GetByIDAsync(id);
            if (ship != null)
            {
                _unitOfWork.GetRepository<Ship>().Delete(id);
            }

            await _unitOfWork.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool ShipExists(int id)
        {
            return (_unitOfWork.GetRepository<Ship>()?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
