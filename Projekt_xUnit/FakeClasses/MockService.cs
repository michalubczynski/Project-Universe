using BLL_BuisnessLogicLayer;
using Universe.Models.discoverer;
using Universe.Models.galaxy;
using Universe.Models.planet;
using Universe.Models.ship;
using Universe.Models.starsystem;

namespace Tests_xUnit.FakeClasses
{
    public class MockService : IService
    {
        public int StarCount { get; set; }
        public Ship Ship { get; set; }
        public Discoverer Discoverer { get; set; }
        public StarSystem StarSystem { get; set; }
        public Galaxy Galaxy { get; set; }
        public List<Discoverer> discoverers { get; set; }
        public Task AddRandomStars(int count)
        {
            StarCount = count;
            return Task.CompletedTask;
        }

        public Task FireDiscoverer(int id)
        {
            var dis = discoverers.AsEnumerable().Select(d => d).Where(d => d.Id == id).First();
            discoverers.Remove(dis);
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
            Discoverer = new Discoverer()
            {
                Name = name,
                Surname = surname,
                Age = age
            };
            return Task.CompletedTask;
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
            StarSystem.Galaxy = Galaxy;
            StarSystem.GalaxyId = Galaxy.Id;
            Galaxy.StarSystems = new List<StarSystem>
            {
                StarSystem
            };
            return Task.CompletedTask;
        }

        public Task RewardExplorerByNewShip(int discovererID, string shipModel, string shipName, int maxSpeed, int singleChargeRange)
        {
            Discoverer = new Discoverer()
            {
                Id = discovererID
            };
            Ship = new Ship()
            {
                MaxSpeed = maxSpeed,
                ShipModel = shipModel,
                SingleChargeRange = singleChargeRange,
                Name = shipName,
                Discoverer = Discoverer
            };
            Discoverer.Ship = Ship;
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
