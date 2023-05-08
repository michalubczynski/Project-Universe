using BLL_BuisnessLogicLayer;
using BuisnessLogicLayer;
using DAL_DataAccessLayer;
using Universe.Models.galaxy;
using Universe.Models.planet;

namespace Projekt_xUnit
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {

        var planets = new List<Planet>{
         new Planet { Id = 1, Name = "Planet A", Mass = 10 },
         new Planet { Id = 2, Name = "Planet B", Mass = 5 },
         new Planet { Id = 3, Name = "Planet C", Mass = 15 }
        };

            var fakeRepository = new FakeRepository<Planet>(entity => entity.Id);

            foreach (var planet in planets)
            {
                fakeRepository.Insert(planet);
            }

            TestUnitOfWork unitOfWork = new TestUnitOfWork();
            unitOfWork.
            var service = new Service(unitOfWork);

            service.GetHeaviestPlanet();
        }
    }
}