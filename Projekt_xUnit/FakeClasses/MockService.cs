using BLL_BuisnessLogicLayer;
using Microsoft.AspNetCore.Http;
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
		public Ship Ship {get; set;}
		public Discoverer Discoverer {get; set;}
		public Task AddRandomStars(int count)
		{
			StarCount = count;
			return Task.CompletedTask;
		}

		public IQueryable<Galaxy> GetAllGalaxies()
		{
			throw new NotImplementedException();
		}

		public int GetAllPlanetsCount()
		{
			throw new NotImplementedException();
		}

		public int GetAllStarsCount()
		{
			return StarCount;
		}

		public IQueryable<StarSystem> GetAllStarSystems()
		{
			throw new NotImplementedException();
		}

		public Planet GetHeaviestPlanet()
		{
			throw new NotImplementedException();
		}

		public Task HireNewDiscoverer(string name, string surname, int age)
		{
			throw new NotImplementedException();
		}

		public Task MakeNewShip(int MaxRange, int MaxSpeed, string? model = null, int? discovererID = null)
		{
			Ship = new Ship()
			{
				MaxSpeed = MaxSpeed,
				ShipModel = model,
				SingleChargeRange = MaxRange
			};
			return Task.CompletedTask;
		}

		public Task MoveStarSystemToAnotherGalaxy(int starSystemId, int galaxyId)
		{
			throw new NotImplementedException();
		}

		public Task RewardExplorerByNewShip(int discovererID, string shipModel, string shipName, int maxSpeed, int singleChargeRange)
		{
			Discoverer = new Discoverer() {
				Id = discovererID
			};
			Ship = new Ship()
			{
				MaxSpeed = maxSpeed,
				ShipModel = shipModel,
				SingleChargeRange = singleChargeRange,

			};
			return Task.CompletedTask;
		}

		public IQueryable<Ship> ShowAllShips()
		{
			throw new NotImplementedException();
		}

		public IQueryable<Discoverer> ShowDetailsDiscovererers()
		{
			throw new NotImplementedException();
		}
	}
}
