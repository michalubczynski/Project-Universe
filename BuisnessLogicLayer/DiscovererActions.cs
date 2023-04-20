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
        void DiscoverStar(Star o);
        void DiscoverPlanet(Planet o);
        void DiscoverStarSystem(StarSystem o);
        void DiscoverGalaxy(Galaxy o);
        public void ReserveShip(Ship o);
        public void ReturnShip();
        public void MarkBroken();
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
                    disRepo.Update(d);
                    await _unitOfWork.SaveChangesAsync();
                }
            }
            // nie mozna wypozyczyc bo ktos to ma
        }

        public async void ReturnShip()
        {
            var discRepo = _unitOfWork.GetRepository<Discoverer>();
            var shipRepo = _unitOfWork.GetRepository<Ship>();
            var d = discRepo.GetByIDAsync(Id).Result;
            var s = d.Ship;
            if (d != null)
            {
                if (s != null)
                {
                    s.Discoverer = null;
                    d.Ship = null;
                    shipRepo.Update(s);
                    discRepo.Update(d);
                    await _unitOfWork.SaveChangesAsync();
                }
                // nie mozna wypozyczyc bo nie ma statku
            }
        }
        public async void MarkBroken()
        {
            var ship = _unitOfWork.GetRepository<Discoverer>().GetByID(Id).Ship;
            if (ship != null)
                ship.IfBroken = true;
            _unitOfWork.GetRepository<Ship>().Update(ship);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
