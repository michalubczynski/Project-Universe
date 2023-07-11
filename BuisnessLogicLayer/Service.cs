using Microsoft.EntityFrameworkCore;
using System.Text;
using Universe.Models;
using Universe.Models.discoverer;
using Universe.Models.galaxy;
using Universe.Models.planet;
using Universe.Models.ship;
using Universe.Models.star;
using Universe.Models.starsystem;

namespace BLL_BuisnessLogicLayer
{
    public class Service : IService
    {
        private readonly IUnitOfWork _unitOfWork;
        public Service(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task AddRandomStars(int count)
        {
            if (count < 1)
            {
                throw new InvalidDataException();
            }
            var stars = new Star[count];
            var rng = new Random();
            var starSystemRepo = _unitOfWork.GetRepository<StarSystem>();
            var starSystems = starSystemRepo.GetList().ToList();
            var starSystemIDs = starSystems.Select(s => s.Id).ToArray();

            for (int i = 0; i < count; i++)
            {
                stars[i] = new Star();
                stars[i].Age = rng.Next();
                stars[i].Luminosity = rng.NextDouble() * int.MaxValue;
                stars[i].Mass = rng.NextDouble() * int.MaxValue;
                stars[i].Name = "Random Star no. " + rng.Next() + "." + rng.Next();
                stars[i].Radius = rng.NextDouble() * int.MaxValue;
                stars[i].Temperature = rng.NextDouble() * int.MaxValue;
                stars[i].Type = Star.TypeOfStar.Main_sequence_stars;
                stars[i].StarSystemId = (starSystems.Count == 0) ? 0 : starSystemIDs[rng.Next(0, starSystems.Count - 1)];
                stars[i].StarSystem = starSystems.FirstOrDefault(s => s.Id == stars[i].StarSystemId);

                await _unitOfWork.GetRepository<Star>().InsertAsync(stars[i]);
            }

            await _unitOfWork.SaveChangesAsync();
        }


        public virtual int GetAllPlanetsCount()
        {
            var planets = _unitOfWork.GetRepository<Planet>().GetList();
            return planets.Count();
        }

        public int GetAllStarsCount()
        {
            var stars = _unitOfWork.GetRepository<Star>().GetList();
            return stars.Count();
        }

        public virtual Planet GetHeaviestPlanet()
        {
            var planet = _unitOfWork.GetRepository<Planet>().GetList().OrderByDescending(x => x.Mass).FirstOrDefault();
            return planet;
        }

        public async Task HireNewDiscoverer(string name, string surname, int age)
        {
            _unitOfWork.GetRepository<Discoverer>().Insert(new Discoverer { Name = name, Surname = surname, Age = age });
            _unitOfWork.GetRepository<Discoverer>().Save();
            await _unitOfWork.SaveChangesAsync();
        }

        public IQueryable<Discoverer> ShowDetailsDiscovererers()
        {
            var discoverers = _unitOfWork.GetRepository<Discoverer>().GetList().Include(d => d.Ship);
            return discoverers;
        }

        public IQueryable<Ship> ShowAllShips()
        {
            var Ships = _unitOfWork.GetRepository<Ship>().GetList().Include(s => s.Discoverer);
            return Ships;
        }

        public async Task MakeNewShip(int MaxRange, int MaxSpeed, string? model = null, int? discovererId = null)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringBuilder = new StringBuilder(10);
            for (int i = 0; i < 10; i++)
            {
                int randomIndex = random.Next(chars.Length);
                stringBuilder.Append(chars[randomIndex]);
            }
            Ship newShip;
            if (discovererId == null)
            {
                newShip = new Ship { Name = stringBuilder.ToString(), ShipModel = model, MaxSpeed = MaxSpeed, SingleChargeRange = MaxRange, Discoverer = null, IfBroken = false };
            }
            else
            {
                var discoverer = _unitOfWork.GetRepository<Discoverer>().GetByID(discovererId.Value);
                newShip = new Ship { Name = stringBuilder.ToString(), ShipModel = model, MaxSpeed = MaxSpeed, SingleChargeRange = MaxRange, Discoverer = discoverer, IfBroken = false };
            }
            _unitOfWork.GetRepository<Ship>().Insert(newShip);
            _unitOfWork.GetRepository<Ship>().Save();
            await _unitOfWork.SaveChangesAsync();

        }

        public async Task MoveStarSystemToAnotherGalaxy(int starsystemID, int destinationGalaxyID)
        {
            var starSystenRepo = _unitOfWork.GetRepository<StarSystem>();
            var starSystem = starSystenRepo.GetByID(starsystemID);
            var galaxyRepo = _unitOfWork.GetRepository<Galaxy>();
            var galaxy = galaxyRepo.GetByID(destinationGalaxyID);
            if (galaxy == null || starSystem == null)
                throw new InvalidDataException();
            starSystem.GalaxyId = destinationGalaxyID;
            starSystenRepo.Update(starSystem);
            if (galaxy.StarSystems == null)
            {
                galaxy.StarSystems = new List<StarSystem>();
            }
            galaxy.StarSystems.Add(starSystem);
            galaxyRepo.Update(galaxy);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task RewardExplorerByNewShip(int discovererID, string shipModel, string shipName, int maxSpeed, int singleChargeRange)
        {
            var discovererRepo = _unitOfWork.GetRepository<Discoverer>();
            var discoverer = discovererRepo.GetByID(discovererID);
            if (discoverer == null)
                throw new InvalidDataException();
            var s = new Ship()
            {
                Name = shipName,
                ShipModel = shipModel,
                MaxSpeed = maxSpeed,
                SingleChargeRange = singleChargeRange,
                Discoverer = discoverer,
                IfBroken = false
            };
            var shipRepo = _unitOfWork.GetRepository<Ship>();
            shipRepo.Insert(s);
            await _unitOfWork.SaveChangesAsync();
            discoverer.ShipId = s.Id;
            discoverer.Ship = s;
            discovererRepo.Update(discoverer);
            await discovererRepo.SaveAsync();
            s.Discoverer = discoverer;
            shipRepo.Update(s);
            await shipRepo.SaveAsync();
        }

        public IQueryable<StarSystem> GetAllStarSystems()
        {
            return _unitOfWork.GetRepository<StarSystem>().GetList();
        }

        public IQueryable<Galaxy> GetAllGalaxies()
        {
            return _unitOfWork.GetRepository<Galaxy>().GetList().Include(g => g.StarSystems);
        }

        public async Task FireDiscoverer(int id)
        {
            var repo = _unitOfWork.GetRepository<Discoverer>();
            repo.Delete(id);
            await repo.SaveAsync();
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
