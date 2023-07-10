using BLL_BuisnessLogicLayer;
using Microsoft.EntityFrameworkCore;
using Models;
using Moq;
using System.Collections;
using System.Linq;
using Tests_xUnit.FakeClasses;
using Universe.Models;
using Universe.Models.planet;
using Universe.Models.star;

namespace Projekt_xUnit
{
	public class TestsV1
	{
		[Fact]
		public void BLLTest()
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
			Assert.Equal(planets.ElementAt(2), result);
		}
		[Fact]
		public void MockTest()
		{
			var mockRepo = new Mock<IRepository<Planet>>();
			var p1 = new Planet
			{
				Id = 0,
				Name = "aaa"
			};
			var p2 = new Planet
			{
				Id = 1,
				Name = "bbb"
			};
			mockRepo.Setup(x => x.GetList().AsEnumerable<Planet>()).Returns(new List<Planet>{p1, p2});
			var unitOfWork = new UnitOfWork();
			unitOfWork.AddRepository(mockRepo.Object);
			var service = new Service(unitOfWork);
			var result = service.GetAllPlanetsCount();
			Assert.Equal(2, result.Result);
		}
		public static DbUniverse GetTestDbContext(string dbName)
		{
			// Create db context options specifying in memory database
			var options = new DbContextOptionsBuilder<DbUniverse>()
			.UseInMemoryDatabase(databaseName: dbName)
			.Options;

			//Use this to instantiate the db context
			return new DbUniverse(options);

		}
		private DbUniverse GetTestDatabase()
		{
			var testContext = GetTestDbContext(nameof(AddRandomStarsTest));
			return testContext;
		}
		[Fact]
		public void AddRandomStarsTest()
		{
			var testContext = GetTestDatabase();
			var starsRepo = new Repository<Planet>(testContext);
			var s = new Planet
			{
				Id = 0,
				Name = "aaa"
			};
			starsRepo.Insert(s);
			Assert.Single(starsRepo.GetList());
		}
		[Fact]
		public void DummyPlanetRepoTest()
		{
			Action dummyActCreate = () => new Repository<Planet>(null);
			Assert.Throws<NullReferenceException>(dummyActCreate);
		}
	}
}