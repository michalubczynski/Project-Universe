using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
			var starSystemIDs = new int[starSystemRepo.GetList().Count()];
			for (int i = 0; i < starSystemIDs.Length; i++)
			{
				starSystemIDs[i] = starSystemRepo.GetList().ElementAt(i).Id;
			}
			for (int i = 0; i < count; i++)
			{
				stars[i] = new Star();
				stars[i].Age = rng.Next();
				stars[i].Luminosity = rng.NextDouble() * rng.Next();
				stars[i].Mass = rng.NextDouble() * rng.Next();
				stars[i].Name = "Random Star no. " + rng.Next() + "." + rng.Next();
				stars[i].Radius = rng.NextDouble() * rng.Next();
				stars[i].Temperature = rng.NextDouble() * rng.Next();
				stars[i].Type = Star.TypeOfStar.Main_sequence_stars;
				stars[i].StarSystemId = (starSystemRepo.GetList().Count() == 0) ? 0 : starSystemIDs[rng.Next(0, starSystemRepo.GetList().Count() - 1)];
				stars[i].StarSystem = (starSystemRepo.GetList().Count() == 0) ? null : starSystemRepo.GetByID(stars[i].StarSystemId);
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
        //    System.InvalidCastException : Unable to cast object of type 'System.Threading.Tasks.Task`1[System.Collections.Generic.IEnumerable`1[Universe.Models.discoverer.Discoverer]]' to type 'System.Collections.Generic.IEnumerable`1[Universe.Models.discoverer.Discoverer]'.

        public async Task KillDiscoverer(string name, string surname, int age)
        {
			var dList = _unitOfWork.GetRepository<Discoverer>().GetList();
			foreach (Discoverer d in dList)
			{
				if (d.Name == name && d.Surname == surname && d.Age == age)
				{
					_unitOfWork.GetRepository<Discoverer>().Delete(d.Id);

				}
			}
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

        public async Task MakeNewShip(int MaxRange, int MaxSpeed, string? model = null, int? discovererId= null)
		{
			int newestIdShip = _unitOfWork.GetRepository<Ship>().GetList().Last().Id+1;
			string _model;
			if (model == null)
			{
				_model = _unitOfWork.GetRepository<Ship>().GetList().Last().ShipModel;
			}
			else { _model = model; }
			Ship newShip;
			if (discovererId == null)
			{
				newShip = new Ship { Name = newestIdShip.ToString(), ShipModel = _model, MaxSpeed = MaxSpeed, SingleChargeRange = MaxRange, Discoverer = null, IfBroken = false };
			}
			else
			{
				var discoverer = _unitOfWork.GetRepository<Discoverer>().GetByID(discovererId.Value);
				newShip = new Ship { Name = newestIdShip.ToString(), ShipModel = _model, MaxSpeed = MaxSpeed, SingleChargeRange = MaxRange, Discoverer = discoverer, IfBroken = false };
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
			var s = new Ship() {
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
            return _unitOfWork.GetRepository<Galaxy>().GetList();
        }
    }
}
