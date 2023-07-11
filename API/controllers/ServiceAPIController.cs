using BLL_BuisnessLogicLayer;
using Microsoft.AspNetCore.Mvc;
using Universe.Models.discoverer;
using Universe.Models.galaxy;
using Universe.Models.planet;
using Universe.Models.ship;
using Universe.Models.star;
using Universe.Models.starsystem;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ServiceAPIController : ControllerBase
    {
        private readonly IService _iService;
        public ServiceAPIController(IService workService)
        {
            _iService = workService;
        }

        [HttpGet("planets/count")]
        [ProducesResponseType(typeof(int), 200)]
        public IActionResult GetAllPlanetsCount()
        {
            var count = _iService.GetAllPlanetsCount();
            return Ok(count);
        }

        [HttpGet("stars/count")]
        [ProducesResponseType(typeof(int), 200)]
        public IActionResult GetAllStarsCount()
        {
            var count = _iService.GetAllStarsCount();
            return Ok(count);
        }

        [HttpGet("planets/heaviest")]
        [ProducesResponseType(typeof(Planet), 200)]
        public IActionResult GetHeaviestPlanet()
        {
            var planet = _iService.GetHeaviestPlanet();
            return Ok(planet);
        }

        [HttpGet("starsystem/all")]
        [ProducesResponseType(typeof(StarSystem[]), 200)]
        public IActionResult GetAllStarSystems()
        {
            var starsystems = _iService.GetAllStarSystems();
            return Ok(starsystems);
        }

        [HttpGet("Galaxy/all")]
        [ProducesResponseType(typeof(Galaxy[]), 200)]
        public IActionResult GetAllGalaxies()
        {
            var galax = _iService.GetAllGalaxies();
            return Ok(galax);
        }

        [HttpPost("stars/random")]
        [ProducesResponseType(typeof(Star[]), 200)]
        public async Task<IActionResult> AddRandomStars(int count)
        {
            await _iService.AddRandomStars(count);
            return Ok();
        }

        [HttpPost("starsystem/move")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> MoveStarSystemToAnotherGalaxy(int starsystemToMoveID, int destinationGalaxyID)
        {
            await _iService.MoveStarSystemToAnotherGalaxy(starsystemToMoveID, destinationGalaxyID);
            return Ok();
        }

        [HttpPost("ship/new")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> MakeNewShip(int MaxRange, int MaxSpeed, string? model = null, int? discoverer = null)
        {
            await _iService.MakeNewShip(MaxRange, MaxSpeed, model, discoverer);
            return Ok();
        }

        [HttpGet("ship/all")]
        [ProducesResponseType(typeof(IEnumerable<Ship>), 200)]
        public IActionResult ShowShips()
        {
            var list = _iService.ShowAllShips();
            return Ok(list);
        }



        [HttpPost("discoverer/hire")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> HireNewDiscoverer(string name, string surname, int age)
        {
            await _iService.HireNewDiscoverer(name, surname, age);
            return Ok();
        }

        [HttpGet("discoverer/Show")]
        [ProducesResponseType(typeof(IEnumerable<Discoverer>), 200)]
        public IActionResult ShowDiscoverers()
        {
           var list = _iService.ShowDetailsDiscovererers();
            return Ok(list);
        }

        [HttpPost("explorer/AssignShiptoExploler")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> RewardExplorerByNewShip(int discovererID, string shipModel, string shipName, int maxSpeed, int singleChargeRange)
        {
            await _iService.RewardExplorerByNewShip(discovererID, shipModel, shipName, maxSpeed, singleChargeRange);
            return Ok();
        }
    }
}
