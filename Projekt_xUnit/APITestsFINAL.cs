using Moq;
using BLL_BuisnessLogicLayer;
using API.Controllers;
using Microsoft.AspNetCore.Mvc;
using Universe.Models.planet;
using Universe.Models.starsystem;
using Universe.Models.galaxy;
using Universe.Models.ship;
using Universe.Models.discoverer;
using Tests_xUnit.FakeClasses;

namespace Tests_xUnit
{
	public class APITestsFINAL
	{
		// to zostalo
		//public async Task<IActionResult> MoveStarSystemToAnotherGalaxy(int starsystemToMoveID, int destinationGalaxyID)
		[Fact]
		public void GetAllPlanetsCountTestShouldReturnValidNumber()
		{
			var count = 3;
			var mockService = new Mock<IService>();
			mockService.Setup(s => s.GetAllPlanetsCount()).Returns(count);
			var api = new ServiceAPIController(mockService.Object);
			var apiResult = api.GetAllPlanetsCount();
			var okResult = Assert.IsType<OkObjectResult>(apiResult);
			var result = Assert.IsType<int>(okResult.Value);
			Assert.Equal(count, result);
		}
		[Fact]
		public void GetAllStarsCountTestShouldReturnValidNumber()
		{
			var count = 5;
			var mockService = new Mock<IService>();
			mockService.Setup(s => s.GetAllStarsCount()).Returns(count);
			var api = new ServiceAPIController(mockService.Object);
			var apiResult = api.GetAllStarsCount();
			var okResult = Assert.IsType<OkObjectResult>(apiResult);
			var result = Assert.IsType<int>(okResult.Value);
			Assert.Equal(count, result);
		}
		[Fact]
		public void GetHeaviestPlanetTestShouldReturnPlanet()
		{
			var p1 = new Planet() {
				Name = "p1",
				Mass = 1,
				Type = TypeOfPlanets.Gas_Giants
			};
			var mockBLL = new Mock<IService>();
			mockBLL.Setup(s => s.GetHeaviestPlanet()).Returns(p1);
			var api = new ServiceAPIController(mockBLL.Object);
			var apiResult = api.GetHeaviestPlanet();
			var okResult = Assert.IsType<OkObjectResult>(apiResult);
			var result = Assert.IsType<Planet>(okResult.Value);
			Assert.Equal(p1, result);
		}
		[Fact]
		public void GetHeaviestPlanetTestShouldReturnNull()
		{
			var mockBLL = new Mock<IService>();
			var p = new Planet();
			mockBLL.Setup(s => s.GetHeaviestPlanet()).Returns(p);
			var api = new ServiceAPIController(mockBLL.Object);
			var apiResult = api.GetHeaviestPlanet();
			var okResult = Assert.IsType<OkObjectResult>(apiResult);
			var result = Assert.IsType<Planet>(okResult.Value);
			Assert.Equal(p, result);
		}
		[Fact]
		public void GetAllStarSystemsTestShouldReturnNull()
		{
			var mockBLL = new Mock<IService>();
			var s = new List<StarSystem>();
			mockBLL.Setup(s => s.GetAllStarSystems()).Returns(s.AsQueryable());
			var api = new ServiceAPIController(mockBLL.Object);
			var apiResult = api.GetAllStarSystems();
			var okResult = Assert.IsType<OkObjectResult>(apiResult);
			//var result = Assert.Null(okResult.Value);
		}
		[Fact]
		public void GetAllStarSystemsTestShouldReturnList()
		{
			var mockBLL = new Mock<IService>();
			var s = new List<StarSystem>() {
				new StarSystem()
				{
					Name = "s1"
				},
				new StarSystem()
				{
					Name = "s2"
				}
			};
			mockBLL.Setup(s => s.GetAllStarSystems()).Returns(s.AsQueryable());
			var api = new ServiceAPIController(mockBLL.Object);
			var apiResult = api.GetAllStarSystems();
			var okResult = Assert.IsType<OkObjectResult>(apiResult);
			var result = Assert.IsType<EnumerableQuery<StarSystem>>(okResult.Value);
			Assert.Equal(s, result);
		}
		[Fact]
		public void GetAllGalaxiesTestShouldReturnNull()
		{
			var mockBLL = new Mock<IService>();
			var s = new List<Galaxy>();
			mockBLL.Setup(s => s.GetAllGalaxies()).Returns(s.AsQueryable());
			var api = new ServiceAPIController(mockBLL.Object);
			var apiResult = api.GetAllGalaxies();
			var okResult = Assert.IsType<OkObjectResult>(apiResult);
			//var result = Assert.Null(okResult.Value);
		}
		[Fact]
		public void GetAllGalaxiesTestShouldReturnList()
		{
			var mockBLL = new Mock<IService>();
			var s = new List<Galaxy>() {
				new Galaxy()
				{
					Name = "s1"
				},
				new Galaxy()
				{
					Name = "s2"
				}
			};
			mockBLL.Setup(s => s.GetAllGalaxies()).Returns(s.AsQueryable());
			var api = new ServiceAPIController(mockBLL.Object);
			var apiResult = api.GetAllGalaxies();
			var okResult = Assert.IsType<OkObjectResult>(apiResult);
			var result = Assert.IsType<EnumerableQuery<Galaxy>>(okResult.Value);
			Assert.Equal(s, result);
		}
		[Fact]
		public void ShowShipsTestShouldReturnNull()
		{
			var mockBLL = new Mock<IService>();
			var s = new List<Ship>();
			mockBLL.Setup(s => s.ShowAllShips()).Returns(s.AsQueryable());
			var api = new ServiceAPIController(mockBLL.Object);
			var apiResult = api.ShowShips();
			var okResult = Assert.IsType<OkObjectResult>(apiResult);
			//var result = Assert.Null(okResult.Value);
		}
		[Fact]
		public void ShowShipsTestShouldReturnList()
		{
			var mockBLL = new Mock<IService>();
			var s = new List<Ship>() {
				new Ship()
				{
					Name = "s1"
				},
				new Ship()
				{
					Name = "s2"
				}
			};
			mockBLL.Setup(s => s.ShowAllShips()).Returns(s.AsQueryable());
			var api = new ServiceAPIController(mockBLL.Object);
			var apiResult = api.ShowShips();
			var okResult = Assert.IsType<OkObjectResult>(apiResult);
			var result = Assert.IsType<EnumerableQuery<Ship>>(okResult.Value);
			Assert.Equal(s, result);
		}
		[Fact]
		public void ShowDiscoverersTestShouldReturnNull()
		{
			var mockBLL = new Mock<IService>();
			var s = new List<Discoverer>();
			mockBLL.Setup(s => s.ShowDetailsDiscovererers()).Returns(s.AsQueryable());
			var api = new ServiceAPIController(mockBLL.Object);
			var apiResult = api.ShowDiscoverers();
			var okResult = Assert.IsType<OkObjectResult>(apiResult);
			//var result = Assert.Null(okResult.Value);
		}
		[Fact]
		public void ShowDiscoverersTestShouldReturnList()
		{
			var mockBLL = new Mock<IService>();
			var s = new List<Discoverer>() {
				new Discoverer()
				{
					Name = "s1"
				},
				new Discoverer()
				{
					Name = "s2"
				}
			};
			mockBLL.Setup(s => s.ShowDetailsDiscovererers()).Returns(s.AsQueryable());
			var api = new ServiceAPIController(mockBLL.Object);
			var apiResult = api.ShowDiscoverers();
			var okResult = Assert.IsType<OkObjectResult>(apiResult);
			var result = Assert.IsType<EnumerableQuery<Discoverer>>(okResult.Value);
			Assert.Equal(s, result);
		}
		[Fact]
		public void RandomStarsTest()
		{
			var mockBLL = new MockService();
			mockBLL.StarCount = 1;
			var api = new ServiceAPIController(mockBLL);
			var apiResult = api.AddRandomStars(3);
			Assert.IsType<OkResult>(apiResult.Result);
			Assert.Equal(3, mockBLL.StarCount);
		}
		[Fact]
		public void MakeNewShipTest()
		{
			var mockBLL = new MockService();
			var maxRange = 1;
			var maxSpeed = 1;
			var model = "s";
			var api = new ServiceAPIController(mockBLL);
			var apiResult = api.MakeNewShip(maxRange, maxSpeed, model);
			Assert.IsType<OkResult>(apiResult.Result);
			Assert.Equal(maxSpeed, mockBLL.Ship.MaxSpeed);
			Assert.Equal(model, mockBLL.Ship.ShipModel);
			Assert.Equal(maxRange, mockBLL.Ship.SingleChargeRange);
		}
		[Fact]
		public void RewardExplorerByNewShipTest()
		{
			var mockBLL = new MockService();
			var maxRange = 1;
			var maxSpeed = 1;
			var model = "s";
			var dID = 1;
			var name = "s";
			var api = new ServiceAPIController(mockBLL);
			var apiResult = api.RewardExplorerByNewShip(dID, model, name, maxSpeed, maxRange);
			Assert.IsType<OkResult>(apiResult.Result);
			Assert.Equal(maxSpeed, mockBLL.Ship.MaxSpeed);
			Assert.Equal(model, mockBLL.Ship.ShipModel);
			Assert.Equal(maxRange, mockBLL.Ship.SingleChargeRange);
			Assert.Equal(dID, mockBLL.Discoverer.Id);
			Assert.Equal(name, mockBLL.Ship.Name);
			Assert.Equal(mockBLL.Discoverer, mockBLL.Ship.Discoverer);
		}
		[Fact]
		public void HireNewDiscovererTest()
		{
			var mockBLL = new MockService();
			var name = "d";
			var age = 1;
			var surname = "s";
			var api = new ServiceAPIController(mockBLL);
			var apiResult = api.HireNewDiscoverer(name, surname, age);
			Assert.IsType<OkResult>(apiResult.Result);
			Assert.Equal(name, mockBLL.Discoverer.Name);
			Assert.Equal(surname, mockBLL.Discoverer.Surname);
			Assert.Equal(age, mockBLL.Discoverer.Age);
		}
		[Fact]
		public void MoveStarSystemToAnotherGalaxyTest()
		{
			var mockBLL = new MockService();
			mockBLL.Galaxy = new Galaxy() {
				Id = 1
			};
			mockBLL.StarSystem = new StarSystem()
			{
				Id = 1
			};
			var api = new ServiceAPIController(mockBLL);
			var apiResult = api.MoveStarSystemToAnotherGalaxy(1, 1);
			Assert.IsType<OkResult>(apiResult.Result);
			Assert.Equal(mockBLL.StarSystem, mockBLL.Galaxy.StarSystems.First());
			Assert.Equal(mockBLL.StarSystem.GalaxyId, mockBLL.Galaxy.Id);
		}
	}
}
