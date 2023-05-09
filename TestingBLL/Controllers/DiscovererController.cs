using Microsoft.AspNetCore.Mvc;

namespace TestingBLL.Controllers
{
	public class DiscovererController : Controller
	{

		//AddRandomStars()
		//GetAllPlanetsCount()
		//GetHeaviestPlanet()

		public IActionResult Index()
		{
			return View();
		}
	}
}
