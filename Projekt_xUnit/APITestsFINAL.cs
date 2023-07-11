using Moq;
using BLL_BuisnessLogicLayer;
using Microsoft.AspNetCore.Mvc;

namespace Tests_xUnit
{
	public class APITestsFINAL
	{
		//public IActionResult GetAllPlanetsCount()
		//public IActionResult GetAllStarsCount()
		//public IActionResult GetHeaviestPlanet()
		//public IActionResult GetAllStarSystems()
		//public IActionResult GetAllGalaxies()
		//[ProducesResponseType(typeof(Star[]), 200)]
		//public async Task<IActionResult> AddRandomStars(int count)
		//public async Task<IActionResult> MoveStarSystemToAnotherGalaxy(int starsystemToMoveID, int destinationGalaxyID)
		//public async Task<IActionResult> MakeNewShip(int MaxRange, int MaxSpeed, string? model = null, int? discoverer = null)
		//public IActionResult ShowShips()
		//public async Task<IActionResult> HireNewDiscoverer(string name, string surname, int age)
		//public IActionResult ShowDiscoverers()
		//public async Task<IActionResult> RewardExplorerByNewShip(int discovererID, string shipModel, string shipName, int maxSpeed, int singleChargeRange)

		[Fact]
		public void GetAllPlanetsCountTestShouldReturnValidNumber()
		{
			var count = 3;
			var mockBLL = new Mock<Service>();
			mockBLL.Setup(s => s.GetAllPlanetsCount()).Returns(count);
			var api = new ServiceAPIController(mockBLL.Object);
			var result = api.GetAllPlanetsCount();
			Assert.IsAssignableFrom<IActionResult>(result);
			Assert.Equal(count, result);
		}
		[Fact]
		public void GetAllStarsCountTestShouldReturnValidNumber()
		{
			var count = 5;
			var mockBLL = new Mock<Service>();
			mockBLL.Setup(s => s.GetAllStarsCount()).Returns(count);
			var api = new ServiceAPIController(mockBLL.Object);
			var result = api.GetAllStarsCount();
			Assert.IsAssignableFrom<IActionResult>(result);
			Assert.Equal(count, result);
		}
		public void GetAllStarsCountTestShouldReturnValidNumber()
		{
			var count = 5;
			var mockBLL = new Mock<Service>();
			mockBLL.Setup(s => s.GetAllStarsCount()).Returns(count);
			var api = new ServiceAPIController(mockBLL.Object);
			var result = api.GetAllStarsCount();
			Assert.IsAssignableFrom<IActionResult>(result);
			Assert.Equal(count, result);
		}
	}
}
