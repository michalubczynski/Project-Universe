using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Universe.Models;
using Universe.Models.discoverer;
using Universe.Models.galaxy;
using Universe.Models.planet;
using Universe.Models.starsystem;

namespace BLL_BuisnessLogicLayer
{
    public interface IWorkService
    {
        Task<int> GetAllPlanetsCount();
        Task<Planet> GetHeaviestPlanet();
        Task AddRandomStars();
        Task MoveStarSystemToAnotherGalaxy(StarSystem starsystemToMove, Galaxy destinationGalaxy);
        Task MakeNewShip();
        Task HireNewDiscoverer(string name, string surname, int age);
        Task RewardExplorerByNewShip();
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
        }

        public Task MakeNewShip()
        {
            throw new NotImplementedException();
        }

        public async Task MoveStarSystemToAnotherGalaxy(StarSystem starsystemToMove, Galaxy destinationGalaxy)
        {
            _unitOfWork.GetRepository<StarSystem>().GetByIDAsync(starsystemToMove.Id).;
        }

        public Task RewardExplorerByNewShip()
        {
            throw new NotImplementedException();
        }
    }
}
