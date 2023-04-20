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
using Universe.Models.starsystem;

namespace BLL_BuisnessLogicLayer
{
    public interface IWorkService
    {
        Task<int> GetAllPlanetsCount();
        Task<Planet> GetHeaviestPlanet();
        Task AddRandomStars();
        Task MoveStarSystemToAnotherGalaxy(StarSystem starsystemToMove, Galaxy destinationGalaxy);
        Task MakeNewShip(int MaxRange, int MaxSpeed, string? model = null, Discoverer? discoverer = null);
        Task HireNewDiscoverer(string name, string surname, int age);
        Task RewardExplorerByNewShip(Discoverer discovererToAward, Ship newShip);
    }
    public class Service : IWorkService
    {
        private readonly IUnitOfWork _unitOfWork;
        public Service(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public Task AddRandomStars()
        {
            throw new NotImplementedException();
        }

        public async Task<int> GetAllPlanetsCount()
        {
            return _unitOfWork.GetRepository<Planet>().GetList().Count();
        }

        public Task<Planet> GetHeaviestPlanet()
        {
            throw new NotImplementedException();
        }

        public async Task HireNewDiscoverer(string name, string surname, int age)
        {
            _unitOfWork.GetRepository<Discoverer>().Insert(new Discoverer(name, surname, age));
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

            Ship newShip = new Ship(newestIdShip.ToString(), model, MaxSpeed, MaxRange, discoverer);
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
