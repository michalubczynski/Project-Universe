using BLL_BuisnessLogicLayer;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Universe.Models.planet;
using Universe.Models.star;
using Universe.Models.starsystem;
using Universe.Models;
using Universe.Models.discoverer;
using Universe.Models.galaxy;
using Universe.Models.ship;

namespace Tests_xUnit
{
    public class TestBLLAtFinal
    {
        private Service _service;
        private Mock<IUnitOfWork> _unitOfWorkMock;

        public TestBLLAtFinal()
        {
            // Create a mock for IUnitOfWork
            _unitOfWorkMock = new Mock<IUnitOfWork>();

            // Initialize the Service with the mock
            _service = new Service(_unitOfWorkMock.Object);
        }

        [Fact]
        public async Task AddRandomStars_WithValidCount_ShouldInsertStars()
        {
            // Arrange
            int count = 5;
            var starSystemRepoMock = new Mock<IRepository<StarSystem>>();
            var starSystemList = new List<StarSystem> { new StarSystem { Id = 1 }, new StarSystem { Id = 2 } };
            starSystemRepoMock.Setup(r => r.GetList()).Returns(starSystemList.AsQueryable());
            _unitOfWorkMock.Setup(uow => uow.GetRepository<StarSystem>()).Returns(starSystemRepoMock.Object);

            var starRepoMock = new Mock<IRepository<Star>>();
            _unitOfWorkMock.Setup(uow => uow.GetRepository<Star>()).Returns(starRepoMock.Object);

            // Act
            await _service.AddRandomStars(count);

            // Assert
            starRepoMock.Verify(r => r.InsertAsync(It.IsAny<Star>()), Times.Exactly(count));
            _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);


        }

        [Fact]
        public async Task AddRandomStars_WithInvalidCount_ShouldThrowException()
        {
            // Arrange
            int count = -1;

            // Act & Assert
            await Assert.ThrowsAsync<InvalidDataException>(async () => await _service.AddRandomStars(count));
        }

        [Fact]
        public async Task GetAllPlanetsCount_ShouldReturnPlanetsCount()
        {
            // Arrange
            var planetRepoMock = new Mock<IRepository<Planet>>();
            var planets = new List<Planet> { new Planet(), new Planet() };
            planetRepoMock.Setup(r => r.GetListAsync()).ReturnsAsync(planets);
            _unitOfWorkMock.Setup(uow => uow.GetRepository<Planet>()).Returns(planetRepoMock.Object);

            // Act
            var count = await _service.GetAllPlanetsCount();

            // Assert
            Assert.Equal(planets.Count, count);
        }

        [Fact]
        public async Task GetAllStarsCount_ShouldReturnStarsCount()
        {
            // Arrange
            var starRepoMock = new Mock<IRepository<Star>>();
            var stars = new List<Star> { new Star(), new Star(), new Star() };
            starRepoMock.Setup(r => r.GetListAsync()).ReturnsAsync(stars);
            _unitOfWorkMock.Setup(uow => uow.GetRepository<Star>()).Returns(starRepoMock.Object);

            // Act
            var count = await _service.GetAllStarsCount();

            // Assert
            Assert.Equal(stars.Count, count);
        }

        [Fact]
        public async Task GetHeaviestPlanet_ShouldReturnHeaviestPlanet()
        {
            // Arrange
            var planetRepoMock = new Mock<IRepository<Planet>>();
            var planets = new List<Planet>
    {
        new Planet { Mass = 10 },
        new Planet { Mass = 15 },
        new Planet { Mass = 5 }
    };
            planetRepoMock.Setup(r => r.GetListAsync()).ReturnsAsync(planets);
            _unitOfWorkMock.Setup(uow => uow.GetRepository<Planet>()).Returns(planetRepoMock.Object);

            // Act
            var heaviestPlanet = await _service.GetHeaviestPlanet();

            // Assert
            Assert.Equal(15, heaviestPlanet.Mass);
        }

        [Fact]
        public async Task HireNewDiscoverer_ShouldInsertNewDiscoverer()
        {
            // Arrange
            var discovererRepoMock = new Mock<IRepository<Discoverer>>();
            _unitOfWorkMock.Setup(uow => uow.GetRepository<Discoverer>()).Returns(discovererRepoMock.Object);

            // Act
            await _service.HireNewDiscoverer("John", "Doe", 30);

            // Assert
            discovererRepoMock.Verify(r => r.Insert(It.IsAny<Discoverer>()), Times.Once);
            discovererRepoMock.Verify(r => r.Save(), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task ShowDetailsDiscoverers_ShouldReturnAllDiscoverers()
        {
            // Arrange
            var discovererRepoMock = new Mock<IRepository<Discoverer>>();
            var discoverers = new List<Discoverer> { new Discoverer(), new Discoverer() };
            discovererRepoMock.Setup(r => r.GetListAsync()).ReturnsAsync(discoverers);
            _unitOfWorkMock.Setup(uow => uow.GetRepository<Discoverer>()).Returns(discovererRepoMock.Object);

            // Act
            var result = await _service.ShowDetailsDiscovererers();

            // Assert
            Assert.Equal(discoverers, result);
        }

        [Fact]
        public async Task ShowAllShips_ShouldReturnAllShipsWithDiscoverers()
        {
            // Arrange
            var shipRepoMock = new Mock<IRepository<Ship>>();
            var ships = new List<Ship>
    {
        new Ship { Name = "Ship1", Discoverer = new Discoverer { Name = "Discoverer1" } },
        new Ship { Name = "Ship2", Discoverer = new Discoverer { Name = "Discoverer2" } }
    };
            shipRepoMock.Setup(r => r.GetList()).Returns(ships.AsQueryable());
            _unitOfWorkMock.Setup(uow => uow.GetRepository<Ship>()).Returns(shipRepoMock.Object);

            // Act
            var result = await _service.ShowAllShips();

            // Assert
            Assert.Equal(ships, result);
        }

        [Fact]
        public async Task MakeNewShip_WithDiscovererId_ShouldCreateNewShipWithDiscoverer()
        {
            // Arrange
            int maxRange = 100;
            int maxSpeed = 200;
            int discovererId = 1;

            var shipRepoMock = new Mock<IRepository<Ship>>();
            var discovererRepoMock = new Mock<IRepository<Discoverer>>();
            var discoverer = new Discoverer { Id = discovererId };
            discovererRepoMock.Setup(r => r.GetByID(discovererId)).Returns(discoverer);
            _unitOfWorkMock.Setup(uow => uow.GetRepository<Ship>()).Returns(shipRepoMock.Object);
            _unitOfWorkMock.Setup(uow => uow.GetRepository<Discoverer>()).Returns(discovererRepoMock.Object);

            // Act
            await _service.MakeNewShip(maxRange, maxSpeed, discovererId: discovererId);

            // Assert
            shipRepoMock.Verify(r => r.Insert(It.IsAny<Ship>()), Times.Once);
            shipRepoMock.Verify(r => r.Save(), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task MoveStarSystemToAnotherGalaxy_WithValidData_ShouldMoveStarSystemToGalaxy()
        {
            // Arrange
            int starSystemId = 1;
            int destinationGalaxyId = 2;

            var starSystemRepoMock = new Mock<IRepository<StarSystem>>();
            var starSystem = new StarSystem { Id = starSystemId };
            starSystemRepoMock.Setup(r => r.GetByID(starSystemId)).Returns(starSystem);
            _unitOfWorkMock.Setup(uow => uow.GetRepository<StarSystem>()).Returns(starSystemRepoMock.Object);

            var galaxyRepoMock = new Mock<IRepository<Galaxy>>();
            var galaxy = new Galaxy { Id = destinationGalaxyId, StarSystems = new List<StarSystem>() };
            galaxyRepoMock.Setup(r => r.GetByID(destinationGalaxyId)).Returns(galaxy);
            _unitOfWorkMock.Setup(uow => uow.GetRepository<Galaxy>()).Returns(galaxyRepoMock.Object);

            // Act
            await _service.MoveStarSystemToAnotherGalaxy(starSystemId, destinationGalaxyId);

            // Assert
            starSystemRepoMock.Verify(r => r.Update(starSystem), Times.Once);
            galaxyRepoMock.Verify(r => r.Update(galaxy), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task RewardExplorerByNewShip_WithValidData_ShouldRewardExplorerWithNewShip()
        {
            // Arrange
            int discovererId = 1;
            string shipModel = "Model1";
            string shipName = "Ship1";
            int maxSpeed = 200;
            int singleChargeRange = 100;

            var discovererRepoMock = new Mock<IRepository<Discoverer>>();
            var discoverer = new Discoverer { Id = discovererId };
            discovererRepoMock.Setup(r => r.GetByIDAsync(discovererId)).ReturnsAsync(discoverer);
            _unitOfWorkMock.Setup(uow => uow.GetRepository<Discoverer>()).Returns(discovererRepoMock.Object);

            var shipRepoMock = new Mock<IRepository<Ship>>();
            _unitOfWorkMock.Setup(uow => uow.GetRepository<Ship>()).Returns(shipRepoMock.Object);

            // Act
            await _service.RewardExplorerByNewShip(discovererId, shipModel, shipName, maxSpeed, singleChargeRange);

            // Assert
            shipRepoMock.Verify(r => r.Insert(It.IsAny<Ship>()), Times.Once);
            shipRepoMock.Verify(r => r.Save(), Times.Once);
            discovererRepoMock.Verify(r => r.Update(discoverer), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
        [Fact]
        public async Task GetAllGalaxies_ShouldReturnAllGalaxies()
        {
            // Arrange
            var galaxyRepoMock = new Mock<IRepository<Galaxy>>();
            var galaxies = new List<Galaxy> { new Galaxy(), new Galaxy() };
            galaxyRepoMock.Setup(r => r.GetListAsync()).ReturnsAsync(galaxies);
            _unitOfWorkMock.Setup(uow => uow.GetRepository<Galaxy>()).Returns(galaxyRepoMock.Object);

            // Act
            var result = await _service.GetAllGalaxies();

            // Assert
            Assert.Equal(galaxies, result);
        }
        [Fact]
        public async Task KillDiscoverer_WithExistingDiscoverer_ShouldDeleteDiscoverer()
        {
            // Arrange

            List<Discoverer> discovererList = new List<Discoverer>
        {
            new Discoverer { Name = "John", Surname = "Doe", Age = 30 },
            new Discoverer { Name = "Jane", Surname = "Smith", Age = 25 }
        };

            var discovererRepoMock = new Mock<IRepository<Discoverer>>();
            discovererRepoMock.Setup(r => r.GetListAsync()).ReturnsAsync(discovererList.AsEnumerable());
            _unitOfWorkMock.Setup(uow => uow.GetRepository<Discoverer>()).Returns(discovererRepoMock.Object);

            discovererRepoMock.Setup(r => r.Delete(It.IsAny<Discoverer>().Id))
                .Callback<Discoverer>(discoverer => discovererList.Remove(discoverer));

            // Act
            await _service.KillDiscoverer("John", "Doe", 30);

            // Assert
            discovererRepoMock.Verify(r => r.Delete(It.IsAny<Discoverer>().Id), Times.Once);
            discovererRepoMock.Verify(r => r.Save(), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task KillDiscoverer_WithNonExistingDiscoverer_ShouldNotDeleteDiscoverer()
        {
            // Arrange
            var discovererList = new List<Discoverer>
            {
                new Discoverer { Name = "John", Surname = "Doe", Age = 30 },
                new Discoverer { Name = "Jane", Surname = "Smith", Age = 25 }
            };

            var discovererRepoMock = new Mock<IRepository<Discoverer>>();
            discovererRepoMock.Setup(r => r.GetListAsync()).ReturnsAsync(discovererList.AsEnumerable());
            _unitOfWorkMock.Setup(uow => uow.GetRepository<Discoverer>()).Returns(discovererRepoMock.Object);

            // Act
            await _service.KillDiscoverer("Mike", "Smith", 40);

            // Assert
            discovererRepoMock.Verify(r => r.Delete(It.IsAny<int>()), Times.Never);
            discovererRepoMock.Verify(r => r.Save(), Times.Never);
            _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}