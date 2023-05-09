using BLL_BuisnessLogicLayer;
using Microsoft.AspNetCore.Mvc;
using Universe.Models.discoverer;
using Universe.Models.galaxy;
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

		public async Task<IActionResult> GetAllPlanetsCount()
		{
			var count = await _workService.GetAllPlanetsCount();
			return View(count);
		}

		public async Task<IActionResult> GetHeaviestPlanet()
		{
			var planet = await _workService.GetHeaviestPlanet();
			return View(planet);
		}

		[HttpPost]
		public async Task<IActionResult> AddRandomStars(int count)
		{
			await _workService.AddRandomStars(count);
			return View();
		}
		[HttpGet]
		public IActionResult AddRandomStars()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> MoveStarSystemToAnotherGalaxy(StarSystem starsystemToMove, Galaxy destinationGalaxy)
		{
			await _workService.MoveStarSystemToAnotherGalaxy(starsystemToMove, destinationGalaxy);
			return View();
		}
		[HttpGet]
		public IActionResult MoveStarSystemToAnotherGalaxy()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> MakeNewShip(int MaxRange, int MaxSpeed, string? model = null, Discoverer? discoverer = null)
		{
			await _workService.MakeNewShip(MaxRange, MaxSpeed, model, discoverer);
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> HireNewDiscoverer(string name, string surname, int age)
		{
			await _workService.HireNewDiscoverer(name, surname, age);
			return View();
		}
		[HttpGet]
		public async Task<IActionResult> HireNewDiscoverer()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> RewardExplorerByNewShip(Discoverer discovererToAward, Ship newShip)
		{
			await _workService.RewardExplorerByNewShip(discovererToAward, newShip);
			return View();
		}
		[HttpPost]
		public IActionResult RewardExplorerByNewShip()
		{
			return View();
		}
		public IActionResult Index()
		{
			return View();
		}
	}
}
