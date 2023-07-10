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
using Universe.Models.star;
using Universe.Models.starsystem;

namespace Universe.Controllers
{
    public class StarSystemsController : Controller
    {
        private readonly IEntityService<StarSystem> _service;

        public StarSystemsController(IEntityService<StarSystem> s)
        {
            _service = s;
        }

        // GET: StarSystems
        public async Task<IActionResult> Index()
        {
            var objs = _service.GetAllSpaceObjects(); //
            if (objs == null)
            {
                return NotFound();
            } //TU
            return View(objs);
        }

        // GET: StarSystems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) // TU
            {
                return NotFound();
            }
            var obj = await _service.GetSpaceObjectByIdAsync((int)id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        // POST: StarSystems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StarSystemId,Name,GalaxyId")] StarSystem starSystem)
        {
            if (ModelState.IsValid)
            {
                await _service.AddSpaceObjectAsync(starSystem);   //TU
                return RedirectToAction(nameof(Index));
            }
            //ViewData["StarSystemId"] = new SelectList(_unitOfWork.GetRepository<StarSystem>().GetList(), "StarId", "Name", star.Id);
            return View(starSystem);
        }

        // GET: StarSystems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var obj = await _service.GetSpaceObjectByIdAsync(id);
            if (obj == null)
            {
                return NotFound();
            }
            //ViewData["GalaxyId"] = new SelectList(_unitOfWork.GetRepository<Galaxy>().GetList(), "GalaxyId", "Name", starSystem.GalaxyId);
            return View(obj);
        }

        // POST: StarSystems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StarSystemId,Name,GalaxyId")] StarSystem starSystem)
        {
            if (id != starSystem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _service.UpdateSpaceObjectAsync(starSystem);
                    //await _unitOfWork.GetRepository<Galaxy>().SaveAsync(); // Może być bez tego bo wywoływana jest w UpdateGalaxyAsync()
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StarSystemExists(starSystem.Id))
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
            //ViewData["GalaxyId"] = new SelectList(_unitOfWork.GetRepository<Galaxy>().GetList(), "GalaxyId", "Name", starSystem.GalaxyId);
            return View(starSystem);
        }

        // GET: StarSystems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var obj = await _service.GetSpaceObjectByIdAsync(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        // POST: StarSystems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var obj = await _service.GetSpaceObjectByIdAsync(id);
            if (obj == null)
            {
                return Problem("Entity set 'DbUniverse.Galaxies'  is null.");
            }
            await _service.RemoveSpaceObjectAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private bool StarSystemExists(int id)
        {
            return _service.SpaceObjectExists(id);
        }
    }
}
