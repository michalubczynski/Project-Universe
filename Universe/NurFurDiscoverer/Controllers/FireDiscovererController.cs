using BLL_BuisnessLogicLayer;
using Microsoft.AspNetCore.Mvc;
using Universe.Models.discoverer;

namespace NurFurDiscoverer.Controllers
{
    public class FireDiscovererController : Controller
    {
        private readonly IService _iService;

        public FireDiscovererController(IService iService)
        {
            _iService = iService;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet("discoverer/fire")]
        [ProducesResponseType(typeof(IEnumerable<Discoverer>), 200)]
        public IActionResult FireDiscoverer()
        {
            var list = _iService.ShowDetailsDiscovererers();
            if (list == null)
            {
                return NotFound();
            }
            return View("FireDiscoverer", list);
        }
		[HttpPost("discoverer/fire/{id}")]
		public async Task<IActionResult> FireDiscoverer(int id)
		{
			await _iService.FireDiscoverer(id);
            return RedirectToAction("Show", "Discoverer");
        }
	}
}
