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

namespace BuisnessLogicLayer
{
    public interface IDiscovererOperation
    {
        void DiscoverStar(Star s);
        void DiscoverPlanet(Planet p);
        void DiscoverStarSystem(StarSystem ss);
        void DiscoverGalaxy(Galaxy g);
        public void ReserveShip(Ship s);
        public void ReturnShip();
    }

    internal class DiscovererActions : IDiscovererOperation
    {
        private readonly IUnitOfWork _unitOfWork;
        public int Id { get; set; }

        public DiscovererActions(IUnitOfWork unitOfWork, int id)
        {
            _unitOfWork = unitOfWork;
            Id = id;
        }

        public async void DiscoverGalaxy(Galaxy o)
        {
            await _unitOfWork.GetRepository<Galaxy>().InsertAsync(o);
            await _unitOfWork.SaveChangesAsync();
        }

        public async void DiscoverPlanet(Planet o)
        {
            await _unitOfWork.GetRepository<Planet>().InsertAsync(o);
            await _unitOfWork.SaveChangesAsync();
        }

        public async void DiscoverStar(Star o)
        {
            await _unitOfWork.GetRepository<Star>().InsertAsync(o);
            await _unitOfWork.SaveChangesAsync();
        }

        public async void DiscoverStarSystem(StarSystem o)
        {
            await _unitOfWork.GetRepository<StarSystem>().InsertAsync(o);
            await _unitOfWork.SaveChangesAsync();
        }

        public async void ReserveShip(Ship o)
        {
            var disRepo = _unitOfWork.GetRepository<Discoverer>();
            if (o.Discoverer == null) {
                Discoverer d = await disRepo.GetByIDAsync(Id);
                if (d != null) {
                    d.Ship = o;
                    o.Discoverer = d;
                    disRepo.Update(d);
                    _unitOfWork.GetRepository<Ship>().Update(o);
                    await _unitOfWork.SaveChangesAsync();
                }
            }
            else {
                // nie mozna wypozyczyc bo ktos to ma
            }
        }

        public async void ReturnShip()
        {
            Discoverer d = await _unitOfWork.GetRepository<Discoverer>().GetByIDAsync(Id);
            if (d != null)
            {
                if (d.Ship != null)
                {
                    d.Ship.Discoverer = null;
                    d.Ship = null;
                }
                else
                {
                    // nie mozna wypozyczyc bo nie ma statku
                }
            }
        }
    }
}
