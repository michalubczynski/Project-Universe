using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        private readonly IUnitOfWork _unitOfWork;

        public StarsController(IUnitOfWork context)
        {
            _unitOfWork = context;
        }

        // GET: Stars
        public async Task<IActionResult> Index()
        {
            var stars = await _unitOfWork.GetRepository<Star>().GetListAsync(); //TU

            if (stars == null)
            {
                return NotFound();
            }
            return View(stars);
            //var dbUniverse = _context.Stars.Include(s => s.StarSystem);
            //return View(await dbUniverse.ToListAsync());
        }

        // GET: Stars/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _unitOfWork.GetRepository<Star>() == null)
            {
                return NotFound();
            }

            var star = await _unitOfWork.GetRepository<Star>()
                .Include(s => s.StarSystem)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (star == null)
            {
                return NotFound();
            }

            return View(star);
        }

        // GET: Stars/Create
        public IActionResult Create()
        {
            ViewData["StarId"] = new SelectList(_unitOfWork.GetRepository<Star>().GetList(), "StarId", "Name");
            return View();
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
                _unitOfWork.GetRepository<Star>().Insert(star);
                await _unitOfWork.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StarSystemId"] = new SelectList(_unitOfWork.GetRepository<StarSystem>().GetList(), "StarId", "Name", star.Id);
            return View(star);
        }

        // GET: Stars/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _unitOfWork.GetRepository<Star>() == null)
            {
                return NotFound();
            }

            var star = await _unitOfWork.GetRepository<Star>().GetByIDAsync(id);
            if (star == null)
            {
                return NotFound();
            }
            ViewData["StarSystemId"] = new SelectList(_unitOfWork.GetRepository<StarSystem>().GetList(), "StarSystemId", "Name", star.StarSystemId);
            return View(star);
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
                    _unitOfWork.Update(star);
                    await _unitOfWork.SaveChangesAsync();
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
            ViewData["StarSystemId"] = new SelectList(_unitOfWork.GetRepository<StarSystem>().GetList(), "StarSysyemId", "Name", star.StarSystemId);
            return View(star);
        }

        // GET: Stars/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _unitOfWork.GetRepository<Star>() == null)
            {
                return NotFound();
            }

            var star = await _unitOfWork.GetRepository<Star>().Include(s => s.StarSystem).FirstOrDefaultAsync(m => m.Id == id);

            if (star == null)
            {
                return NotFound();
            }

            return View(star);
        }

        // POST: Stars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_unitOfWork.GetRepository<Star>() == null)
            {
                return Problem("Entity set 'DbUniverse.StarSystems'  is null.");
            }
            var star = await _unitOfWork.GetRepository<Star>().GetByIDAsync(id);
            if (star != null)
            {
                _unitOfWork.GetRepository<Star>().Delete(id);
            }

            await _unitOfWork.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StarExists(int id)
        {
            return (_unitOfWork.GetRepository<Star>()?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
