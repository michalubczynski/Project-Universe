using BLL_BuisnessLogicLayer;
using Microsoft.AspNetCore.Mvc;
using Universe.Models.discoverer;
using Universe.Models.galaxy;
using Universe.Models.planet;
using Universe.Models.ship;
using Universe.Models.starsystem;

namespace TestingBLL.Controllers
{
	public class ServiceController : Controller
	{
		private readonly IService _workService;

		public ServiceController(IService workService)
		{
			_workService = workService;
		}

        [HttpGet("planets/count")]
        [ProducesResponseType(typeof(int), 200)]
        public async Task<IActionResult> GetAllPlanetsCount()
        {
            var count = await _workService.GetAllPlanetsCount();
            return Ok(count);
        }


        [HttpGet("planets/heaviest")]
        [ProducesResponseType(typeof(Planet), 200)]
        public async Task<IActionResult> GetHeaviestPlanet()
        {
            var planet = await _workService.GetHeaviestPlanet();
            return Ok(planet);
        }

        [HttpPost("stars/random")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> AddRandomStars(int count)
        {
            await _workService.AddRandomStars(count);
            return Ok();
        }

        [HttpPost("starsystem/move")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> MoveStarSystemToAnotherGalaxy(int starsystemToMove, int destinationGalaxy)
        {
            await _workService.MoveStarSystemToAnotherGalaxy(starsystemToMove, destinationGalaxy);
            return Ok();
        }

        [HttpPost("ship/new")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> MakeNewShip(int MaxRange, int MaxSpeed, string? model = null, int? discoverer = null)
        {
            await _workService.MakeNewShip(MaxRange, MaxSpeed, model, discoverer);
            return Ok();
        }

        [HttpPost("discoverer/hire")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> HireNewDiscoverer(string name, string surname, int age)
        {
            await _workService.HireNewDiscoverer(name, surname, age);
            return Ok();
        }

        [HttpPost("explorer/reward")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> RewardExplorerByNewShip(int discovererID, string shipModel, string shipName, int maxSpeed, int singleChargeRange)
        {
            await _workService.RewardExplorerByNewShip(discovererID, shipModel, shipName, maxSpeed, singleChargeRange);
            return Ok();
        }

        public IActionResult Index()
		{
			return View();
		}
	}
}
