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
				stars[i].StarSystem = (starSystemRepo.GetList().Count() == 0) ? null : starSystemRepo.GetByIDAsync(stars[i].StarSystemId).Result;

				await _unitOfWork.GetRepository<Star>().InsertAsync(stars[i]);
			}
			await _unitOfWork.SaveChangesAsync();
		}

		public async Task<int> GetAllPlanetsCount()
		{
			var planets = await _unitOfWork.GetRepository<Planet>().GetListAsync();
			return planets.Count();
		}

		public async Task<Planet> GetHeaviestPlanet()
		{
			var planets = await _unitOfWork.GetRepository<Planet>().GetListAsync();
			return planets.MaxBy(p => p.Mass);
		}

		public async Task HireNewDiscoverer(string name, string surname, int age)
		{
			_unitOfWork.GetRepository<Discoverer>().Insert(new Discoverer { Name = name, Surname = surname, Age = age });
			await _unitOfWork.SaveChangesAsync();
		}

        public async Task<IEnumerable<Discoverer>> ShowDetailsDiscovererers()
        {
            var discoverers = await _unitOfWork.GetRepository<Discoverer>().GetListAsync();
			return discoverers;
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
			await _unitOfWork.SaveChangesAsync();

		}

		public async Task MoveStarSystemToAnotherGalaxy(int starsystemID, int destinationGalaxyID)
		{
			var starSystem = await _unitOfWork.GetRepository<StarSystem>().GetByIDAsync(starsystemID);
			var destinationGalaxy = await _unitOfWork.GetRepository<Galaxy>().GetByIDAsync(destinationGalaxyID);
			if (destinationGalaxy == null || starSystem == null)
				throw new InvalidDataException();
			starSystem.GalaxyId = destinationGalaxyID;
			await _unitOfWork.SaveChangesAsync();
		}

		public async Task RewardExplorerByNewShip(int discovererToAwardID, Ship newShip)
		{
			var discoverer = await _unitOfWork.GetRepository<Discoverer>().GetByIDAsync(discovererToAwardID);
			if (discoverer == null)
				throw new InvalidDataException();
			discoverer.ShipId = newShip.Id;
			newShip.Discoverer = discoverer;
			await _unitOfWork.SaveChangesAsync();
		}
	}
}
