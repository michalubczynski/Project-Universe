using BLL_BuisnessLogicLayer;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Numerics;
using Universe.Models.discoverer;
using Universe.Models.galaxy;
using Universe.Models.planet;
using Universe.Models.ship;
using Universe.Models.starsystem;

namespace TestingBLL.Controllers
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
        public async Task<IActionResult> GetAllPlanetsCount()
        {
            var count = await _iService.GetAllPlanetsCount();
            return Ok(count);
        }


        [HttpGet("planets/heaviest")]
        [ProducesResponseType(typeof(Planet), 200)]
        public async Task<IActionResult> GetHeaviestPlanet()
        {
            var planet = await _iService.GetHeaviestPlanet();
            return Ok(planet);
        }


        [HttpPost("stars/random")]
        [ProducesResponseType(200)]
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


        [HttpPost("discoverer/hire")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> HireNewDiscoverer(string name, string surname, int age)
        {
            await _iService.HireNewDiscoverer(name, surname, age);
            return Ok();
        }

        [HttpGet("discoverer/Show")]
        [ProducesResponseType(typeof(IEnumerable<Discoverer>), 200)]
        public async Task<IActionResult> ShowDiscoverers()
        {
           var list = await _iService.ShowDetailsDiscovererers();
            return Ok(list);
        }


        [HttpPost("explorer/AssignShiptoExploler")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> RewardExplorerByNewShip(int discovererToAward, Ship newShip)
        {
            await _iService.RewardExplorerByNewShip(discovererToAward, newShip);
            return Ok();
        }


    }
}
