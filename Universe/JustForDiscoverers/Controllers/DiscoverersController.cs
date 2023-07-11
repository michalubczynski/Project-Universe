using BLL_BuisnessLogicLayer;
using Microsoft.AspNetCore.Mvc;
using Universe.Models.discoverer;

namespace JustForDiscoverers.Controllers
{
    public class DiscoverersController : Controller
    {
        private readonly IService _iService;
        public IActionResult Index(IService workService)
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
