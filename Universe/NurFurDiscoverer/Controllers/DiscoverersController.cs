using BLL_BuisnessLogicLayer;
using Microsoft.AspNetCore.Mvc;
using Universe.Models.discoverer;

namespace NurFurDiscoverer.Controllers
{
    public class DiscoverersController : Controller
    {
        private readonly IService _iService;

        public DiscoverersController(IService iService)
        {
            _iService = iService;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet("discoverer/Show")]
        [ProducesResponseType(typeof(IEnumerable<Discoverer>), 200)]
        public IActionResult ShowDiscoverers()
        {
            var list = _iService.ShowDetailsDiscovererers();
            return Ok(list);
        }
    }
}
