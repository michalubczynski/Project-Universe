using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Universe.Models;
using Universe.Models.galaxy;
using Universe.Models.starsystem;

namespace Universe.Controllers
{
    public class StarSystemsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public StarSystemsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: StarSystems
        public async Task<IActionResult> Index()
        {
            var starSystems = await _unitOfWork.GetRepository<StarSystem>().GetListAsync(); //TU

            if (starSystems == null)
            {
                return NotFound();
            } //TU NIE JESTEM PEWNY BO WYGENEROWANY AUTOMATYCZNIE WYGLADA

/*            
            var dbUniverse = _context.StarSystems.Include(s => s.Galaxy);
            return View(await dbUniverse.ToListAsync());
*/
            //CALOSC

            return View(starSystems);
        }

        // GET: StarSystems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _unitOfWork.GetRepository<StarSystem>() == null)
            {
                return NotFound();
            }

            var starSystem = await _unitOfWork.GetRepository<StarSystem>()
                .Include(s => s.Galaxy)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (starSystem == null)
            {
                return NotFound();
            }

            return View(starSystem);
        }

        // GET: StarSystems/Create
        public IActionResult Create()
        {
            ViewData["GalaxyId"] = new SelectList(_unitOfWork.GetRepository<Galaxy>().GetList(), "GalaxyId", "Name");
            return View();
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
                _unitOfWork.GetRepository<StarSystem>().Insert(starSystem);
                await _unitOfWork.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GalaxyId"] = new SelectList(_unitOfWork.GetRepository<Galaxy>().GetList(), "GalaxyId", "Name", starSystem.GalaxyId);
            return View(starSystem);
        }

        // GET: StarSystems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _unitOfWork.GetRepository<StarSystem>() == null)
            {
                return NotFound();
            }

            var starSystem = await _unitOfWork.GetRepository<StarSystem>().GetByIDAsync(id);
            if (starSystem == null)
            {
                return NotFound();
            }
            ViewData["GalaxyId"] = new SelectList(_unitOfWork.GetRepository<Galaxy>().GetList(), "GalaxyId", "Name", starSystem.GalaxyId);
            return View(starSystem);
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
                    _unitOfWork.Update(starSystem);
                    await _unitOfWork.SaveChangesAsync();
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
            ViewData["GalaxyId"] = new SelectList(_unitOfWork.GetRepository<Galaxy>().GetList(), "GalaxyId", "Name", starSystem.GalaxyId);
            return View(starSystem);
        }

        // GET: StarSystems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _unitOfWork.GetRepository<StarSystem>() == null)
            {
                return NotFound();
            }

            var starSystem = await _unitOfWork.GetRepository<StarSystem>().Include(s => s.Galaxy).FirstOrDefaultAsync(m => m.Id == id);

            if (starSystem == null)
            {
                return NotFound();
            }

            return View(starSystem);
        }

        // POST: StarSystems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_unitOfWork.GetRepository<StarSystem>() == null)
            {
                return Problem("Entity set 'DbUniverse.StarSystems'  is null.");
            }
            var starSystem = await _unitOfWork.GetRepository<StarSystem>().GetByIDAsync(id);
            if (starSystem != null)
            {
                _unitOfWork.GetRepository<StarSystem>().Delete(id);
            }
            
            await _unitOfWork.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StarSystemExists(int id)
        {
          return (_unitOfWork.GetRepository<StarSystem>()?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
