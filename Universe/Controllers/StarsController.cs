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
    public class StarsController : Controller
    {
        private readonly IEntityService<Star> _service;

        public StarsController(IEntityService<Star> context)
        {
            _service = context;
        }

        // GET: Stars
        public async Task<IActionResult> Index()
        {
            var objs = _service.GetAllSpaceObjects(); //
            if (objs == null)
            {
                return NotFound();
            } //TU
            return View(objs);
        }

        // GET: Stars/Details/5
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

        // POST: Stars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StarId,Name,StarSystemId")] Star star)
        {
            if (ModelState.IsValid)
            {
                await _service.AddSpaceObjectAsync(star);   //TU
                return RedirectToAction(nameof(Index));
            }
            //ViewData["StarSystemId"] = new SelectList(_unitOfWork.GetRepository<StarSystem>().GetList(), "StarId", "Name", star.Id);
            return View(star);
        }

        // GET: Stars/Edit/5
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
            //ViewData["StarSystemId"] = new SelectList(_unitOfWork.GetRepository<StarSystem>().GetList(), "StarSystemId", "Name", star.StarSystemId
            return View(obj);
        }

        // POST: Stars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StarId,Name,Type,Temperature,Radius,Age,Luminosity,Mass,StarSystemId")] Star star)
        {
            if (id != star.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _service.UpdateSpaceObjectAsync(star);
                    //await _unitOfWork.GetRepository<Galaxy>().SaveAsync(); // Może być bez tego bo wywoływana jest w UpdateGalaxyAsync()
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StarExists(star.Id))
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
            //ViewData["StarSystemId"] = new SelectList(_unitOfWork.GetRepository<StarSystem>().GetList(), "StarSysyemId", "Name", star.StarSystemId);
            return View(star);
        }

        // GET: Stars/Delete/5
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

        // POST: Stars/Delete/5
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

        private bool StarExists(int id)
        {
            return _service.SpaceObjectExists(id);
        }
    }
}
