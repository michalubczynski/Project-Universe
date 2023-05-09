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

namespace BLL_BuisnessLogicLayer
{
	public interface IService
	{
		Task<int> GetAllPlanetsCount();
		Task<Planet> GetHeaviestPlanet();
		Task AddRandomStars(int count);
		Task MoveStarSystemToAnotherGalaxy(StarSystem starsystemToMove, Galaxy destinationGalaxy);
		Task MakeNewShip(int MaxRange, int MaxSpeed, string? model = null, Discoverer? discoverer = null);
		Task HireNewDiscoverer(string name, string surname, int age);
		Task RewardExplorerByNewShip(Discoverer discovererToAward, Ship newShip);
	}
}
