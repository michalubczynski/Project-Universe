using BLL_BuisnessLogicLayer;
using Microsoft.AspNetCore.Mvc;
using Universe.Models.discoverer;
using Universe.Models.galaxy;
using Universe.Models.planet;
using Universe.Models.ship;
using Universe.Models.starsystem;

namespace TestingBLL.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ServiceAPIController : ControllerBase
	{
		private readonly IService _iService;

		public ServiceAPIController(IService workService)
		{
			_iService = workService;
		}
		[HttpGet]
		public async Task<int> GetAllPlanetsCount()
		{
			return await _iService.GetAllPlanetsCount();
		}
		[HttpGet]
		public Planet GetHeaviestPlanet()
		{
			return _iService.GetHeaviestPlanet();
		}
		[HttpPost]
		public void AddRandomStars(int count)
		{
			_iService.AddRandomStars(count);
		}

	/*	[HttpPost]
		public void MoveStarSystemToAnotherGalaxy(StarSystem starsystemToMove, Galaxy destinationGalaxy)
		{
			_iService.MoveStarSystemToAnotherGalaxy(starsystemToMove, destinationGalaxy);
		}*/


        [HttpPost]
        public void MoveStarSystemToAnotherGalaxy(int starsystemID, int destinationGalaxyID)
		{
			_iService.MoveStarSystemToAnotherGalaxy(starsystemID, destinationGalaxyID);

		}


        [HttpPost]
		public void MakeNewShip(int MaxRange, int MaxSpeed, string? model = null, int? discoverer = null)
		{
			_iService.MakeNewShip(MaxRange, MaxSpeed, model, discoverer);
		}
		[HttpPost]
		public void HireNewDiscoverer(string name, string surname, int age)
		{
			_iService.HireNewDiscoverer(name, surname, age);
		}


        [HttpPost]
        public void RewardExplorerByNewShip(int discovererID, string shipModel, string shipName, int maxSpeed, int singleChargeRange)
        {
            _iService.RewardExplorerByNewShip(discovererID, shipModel, shipName, maxSpeed, singleChargeRange);
        }

    }
}
