﻿using Microsoft.IdentityModel.Tokens;
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
				throw new InvalidDataException();
			var stars = new Star[count];
			var rng = new Random();
			var starSystemRepo = _unitOfWork.GetRepository<StarSystem>();
			for (int i = 0; i < count; i++)
			{
				stars[i] = new Star {
					Id = (starSystemRepo.CountAsync().Result == 0) ? 0 : _unitOfWork.GetRepository<Star>().GetList().Last().Id + 1,
					Age = rng.Next(),
					Luminosity = rng.NextDouble() * rng.Next(),
					Mass = rng.NextDouble() * rng.Next(),
					Name = "Random Star no. " + rng.Next() + "." + rng.Next(),
					Radius = rng.NextDouble() * rng.Next(),
					Temperature = rng.NextDouble() * rng.Next(),
					Type = Star.TypeOfStar.Main_sequence_stars,
					StarSystemId = (starSystemRepo.GetList().Count() == 0) ? 0 : rng.Next(0, starSystemRepo.GetList().Count() - 1),
					StarSystem = (starSystemRepo.GetList().Count() == 0) ? null : starSystemRepo.GetByIDAsync(stars[i].StarSystemId).Result
				};
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

		public async Task MakeNewShip(int MaxRange, int MaxSpeed, string? model = null, Discoverer? discoverer= null)
		{
			int newestIdShip = _unitOfWork.GetRepository<Ship>().GetList().Last().Id+1;
			string _model;
			if (model == null)
			{
				_model = _unitOfWork.GetRepository<Ship>().GetList().Last().ShipModel;
			}
			else { _model = model; }

			Ship newShip = new Ship { Name = newestIdShip.ToString(), ShipModel = _model, MaxSpeed = MaxSpeed, SingleChargeRange = MaxRange, Discoverer = discoverer, IfBroken = false };
			_unitOfWork.GetRepository<Ship>().Insert(newShip);
			await _unitOfWork.SaveChangesAsync();

		}

		public async Task MoveStarSystemToAnotherGalaxy(StarSystem starsystemToMove, Galaxy destinationGalaxy)
		{
			var starSystem = await _unitOfWork.GetRepository<StarSystem>().GetByIDAsync(starsystemToMove.Id);
			starSystem.GalaxyId = destinationGalaxy.Id;
			await _unitOfWork.SaveChangesAsync();
		}

		public async Task RewardExplorerByNewShip(Discoverer discovererToAward, Ship newShip)
		{
			var discoverer = await _unitOfWork.GetRepository<Discoverer>().GetByIDAsync(discovererToAward.Id);
			discoverer.ShipId = newShip.Id;
			await _unitOfWork.SaveChangesAsync();
		}
	}
}
