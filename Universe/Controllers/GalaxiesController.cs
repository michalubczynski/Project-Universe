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
        private readonly IUnitOfWork _unitOfWork;

        public GalaxiesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Galaxies
        public async Task<IActionResult> Index()
        {
              var galaxies = await _unitOfWork.GetRepository<Galaxy>().GetListAsync(); //TU

            if (galaxies == null)
            {
                return NotFound();
            } //TU

            return View(galaxies);
        }

        // GET: Galaxies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _unitOfWork.GetRepository<Galaxy>() == null) // TU
            {
                return NotFound();
            }
            var galaxy = await _unitOfWork.GetRepository<Galaxy>() //TU
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
                _unitOfWork.GetRepository<Galaxy>().Insert(galaxy);   //TU
                await _unitOfWork.SaveChangesAsync();  //TU
                return RedirectToAction(nameof(Index));
            }
            return View(galaxy);
        }

        // GET: Galaxies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _unitOfWork.GetRepository<Galaxy>() == null)
            {
                return NotFound();
            }

            var galaxy = await _unitOfWork.GetRepository<Galaxy>().GetByIDAsync(id);
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
                    _unitOfWork.GetRepository<Galaxy>().Update(galaxy);
                    await _unitOfWork.GetRepository<Galaxy>().SaveAsync();
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
            if (id == null || _unitOfWork.GetRepository<Galaxy>() == null)
            {
                return NotFound();
            }

            var galaxy = await _unitOfWork.GetRepository<Galaxy>()
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
            if (_unitOfWork.GetRepository<Galaxy>() == null)
            {
                return Problem("Entity set 'DbUniverse.Galaxies'  is null.");
            }
            var galaxy = await _unitOfWork.GetRepository<Galaxy>().GetByIDAsync(id);
            if (galaxy != null)
            {
                _unitOfWork.GetRepository<Galaxy>().Delete(id);
            }
            
            await _unitOfWork.GetRepository<Galaxy>().SaveAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GalaxyExists(int id)
        {
            return (bool)(_unitOfWork.GetRepository<Galaxy>()?.Any(e => e.GalaxyId == id));
        }
    }
}
