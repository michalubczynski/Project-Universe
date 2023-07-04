using BLL_BuisnessLogicLayer;
using Microsoft.AspNetCore.Mvc;
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
		[HttpGet(Name = "GetAllPlanetsCount")]
		public async Task<int> GetAllPlanetsCount()
		{
			return await _iService.GetAllPlanetsCount();
		}
		/*		[HttpGet(Name = "GetHeaviestPlanet")]
				public async Task<Planet> GetHeaviestPlanet()
				{
					return await _iService.GetHeaviestPlanet();
				}*/
		[HttpPost(Name = "AddRandomStars")]
		public void AddRandomStars(int count)
		{
			_iService.AddRandomStars(count);
		}
		//[HttpPost]
		//public void MoveStarSystemToAnotherGalaxy([FromBody] StarSystem starsystemToMove, [FromBody] Galaxy destinationGalaxy)
		//{
		//	_iService.MoveStarSystemToAnotherGalaxy(starsystemToMove, destinationGalaxy);
		//}
/*		[HttpPost(Name = "MakeNewShip")]
		public void MakeNewShip(int MaxRange, int MaxSpeed, string? model = null, Discoverer? discoverer = null)
		{
			_iService.MakeNewShip(MaxRange, MaxSpeed, model, discoverer);
		}*/
		/*		[HttpPost(Name = "HireNewDiscoverer")]
				public void HireNewDiscoverer(string name, string surname, int age)
				{
					_iService.HireNewDiscoverer(name, surname, age);
				}*/
		//[HttpPost]
		//public void RewardExplorerByNewShip([FromBody] Discoverer discovererToAward, [FromBody] Ship newShip)
		//{
		//	_iService.RewardExplorerByNewShip(discovererToAward, newShip);
		//}
	}
}
