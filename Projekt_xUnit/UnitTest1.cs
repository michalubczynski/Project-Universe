using BLL_BuisnessLogicLayer;
using BuisnessLogicLayer;
using DAL_DataAccessLayer;
using System.Numerics;
using Universe.Models;
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

            var fakeRepository = new FakeRepository<Planet>();

            foreach (var p in planets)
            {
                fakeRepository.Insert(p);
            }

            TestUnitOfWork unitOfWork = new TestUnitOfWork();
            unitOfWork.AddRepository(fakeRepository);
            var service = new Service(unitOfWork);
            var result = service.GetHeaviestPlanet();
            Assert.Equal(planets.ElementAt(2).Id, result.Result.Id);
        }
        [Fact]
        public void Test2()
        {
            var mockRepo = new MockEntityRepository<Planet>().MockCountAsync(3).MockGetByID(new Planet { Id = 1, Name = "Planet A", Mass = 10 });
            var unitOfWork = new UnitOfWork();
            unitOfWork.AddRepository(mockRepo.Object);
            var service = new Service(unitOfWork);
            var result = service.GetAllPlanetsCount();
            Assert.Equal(3, result.Result);
        }
    }
}