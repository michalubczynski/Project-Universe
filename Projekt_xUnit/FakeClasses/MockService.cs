using BLL_BuisnessLogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Universe.Models.discoverer;
using Universe.Models.galaxy;
using Universe.Models.planet;
using Universe.Models.ship;
using Universe.Models.starsystem;

namespace Tests_xUnit.FakeClasses
{
	internal class MockService : IService
	{
		public int StarCount {get; set;}
		public Task AddRandomStars(int count)
		{
			StarCount = count;
			return Task.CompletedTask;
		}

		public Task<int> GetAllPlanetsCount()
		{
			throw new NotImplementedException();
		}

		public Task<int> GetAllStarsCount()
		{
			throw new NotImplementedException();
		}

		public Task<Planet> GetHeaviestPlanet()
		{
			throw new NotImplementedException();
		}

		public Task HireNewDiscoverer(string name, string surname, int age)
		{
			throw new NotImplementedException();
		}

		public Task MakeNewShip(int MaxRange, int MaxSpeed, string? model = null, int? discoverer = null)
		{
			throw new NotImplementedException();
		}

		public Task MoveStarSystemToAnotherGalaxy(int starsystemToMove, int destinationGalaxy)
		{
			throw new NotImplementedException();
		}

		public Task RewardExplorerByNewShip(int discovererToAward, Ship newShip)
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<Discoverer>> ShowDetailsDiscovererers()
		{
			throw new NotImplementedException();
		}
	}
}
