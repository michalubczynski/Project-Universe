using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Universe.Models.discoverer;
using Universe.Models.galaxy;
using Universe.Models.planet;
using Universe.Models.ship;
using Universe.Models.star;
using Universe.Models.starsystem;

namespace Universe.Models
{
    public class DbUniverse : DbContext
    {
        public DbSet<Galaxy> Galaxies { get; set; }
        public DbSet<StarSystem> StarSystems { get; set; }
        public DbSet<Star> Stars { get; set; }
        public DbSet<Discoverer> Discoverers { get; set; }
        public DbSet<Ship> Ships { get; set; }
        public DbSet<Planet> Planets { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=UniverseDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ship>().HasOne(d=>d.Discoverer).WithOne(s => s.Ship).HasForeignKey<Discoverer>(d => d.ShipId);
            modelBuilder.ApplyConfigurationsFromAssembly(System.Reflection.Assembly.GetExecutingAssembly());

            //FluentApi Refilling data
            modelBuilder.Entity<Galaxy>().HasData(
            new Galaxy { GalaxyId = 1, Mass = 10e12, Name = "Droga Mleczna", Type = TypeOfGalaxy.spiral },
            new Galaxy { GalaxyId = 2, Mass = 10e12, Name = "Messier 87", Type = TypeOfGalaxy.elliptical },
            new Galaxy { GalaxyId = 3, Mass = 10e15, Name = "GAL-CLUS-022058s", Type = TypeOfGalaxy.peculiar },
            new Galaxy { GalaxyId = 4, Mass = 10e9, Name = "Wielka Mgławica Magellana", Type = TypeOfGalaxy.irregular }
            );
            modelBuilder.Entity<StarSystem>().HasData(
            new StarSystem { StarSystemId = 1, Name = "Wolarz", GalaxyId = 1},
            new StarSystem { StarSystemId = 2, Name = "Orzel", GalaxyId = 2 },
            new StarSystem { StarSystemId = 3, Name = "Skorpion", GalaxyId = 3 },
            new StarSystem { StarSystemId = 4, Name = "Strzelec", GalaxyId = 4 }
                );
            modelBuilder.Entity<Planet>().HasData(
                new Planet { PlanetId = 1, Mass = 2e12, Type = TypeOfPlanets.Gas_Giants, Name = "Jupiter", StarSystemId = 1 },
                new Planet { PlanetId = 2, Mass = 2e12, Type = TypeOfPlanets.Gas_Giants, Name = "Neptune", StarSystemId = 1 },
                new Planet { PlanetId = 3, Mass = 2e12, Type = TypeOfPlanets.Gas_Giants, Name = "Uranus", StarSystemId = 1 },
                new Planet { PlanetId = 4, Mass = 2e12, Type = TypeOfPlanets.Gas_Giants, Name = "Saturn", StarSystemId = 1 },
                new Planet { PlanetId = 5, Mass = 3e12, Type = TypeOfPlanets.Dwarf_Planets, Name = "Pluto", StarSystemId = 1 },
                new Planet { PlanetId = 6, Mass = 4e12, Type = TypeOfPlanets.Super_Earth, Name = "Kepler-438b", StarSystemId = 2},
                new Planet { PlanetId = 7, Mass = 5e12, Type = TypeOfPlanets.Terrestrial_Planets, Name = "Earth", StarSystemId = 1 },
                new Planet { PlanetId = 8, Mass = 5e12, Type = TypeOfPlanets.Terrestrial_Planets, Name = "Mars", StarSystemId = 1 },
                new Planet { PlanetId = 9, Mass = 6e12, Type = TypeOfPlanets.Outer_Planets, Name = "Charon", StarSystemId = 3 }
                );
            modelBuilder.Entity<Star>().HasData(
                new Star { StarId = 1, Age = (int)10e3, Luminosity = 23, Mass = 10e5, Name = "Zeta", Radius = 22.1, Temperature = 30, Type = Star.TypeOfStar.Main_sequence_stars, StarSystemId = 1 },
                new Star { StarId = 2, Age = (int)10e2, Luminosity = 233, Mass = 10e4, Name = "Aldebaran", Radius = 232.1, Temperature = 303, Type = Star.TypeOfStar.Red_giant_stars, StarSystemId = 1 },
                new Star { StarId = 3, Age = (int)10e3, Luminosity = 3, Mass = 10e5, Name = "SiriusB", Radius = 322.1, Temperature = 130, Type = Star.TypeOfStar.White_dwarf_stars , StarSystemId = 1 },
                new Star { StarId = 4, Age = (int)10e4, Luminosity = 235, Mass = 10e7, Name = "PSR_B1509-58", Radius = 2.1, Temperature = 0, Type = Star.TypeOfStar.Neutron_stars, StarSystemId = 3 },
                new Star { StarId = 5, Age = (int)10e4, Luminosity = 0, Mass = 10e7, Name = "Cygnus X-1", Radius = 33334.1, Temperature = -22, Type = Star.TypeOfStar.Black_holes, StarSystemId = 2 }
                );
                modelBuilder.Entity<Ship>().HasData(
                new Ship { ShipId = 1, MaxSpeed = 10, ShipModel = "Super", ShipName = "Mewa", SingleChargeRange = 12 },
                new Ship { ShipId = 2, MaxSpeed = 100, ShipModel = "Duper", ShipName = " Jaszczomp", SingleChargeRange = 120 },
                new Ship { ShipId = 3, MaxSpeed = 1000, ShipModel = "DuperSuper", ShipName = "Orzel", SingleChargeRange = 122 }
                );
            modelBuilder.Entity<Discoverer>().HasData(
                new Discoverer { DiscovererId = 1, Name = "Piotrek", Surname = "Piotrowski", Age = 43, ShipId = 1 },
                new Discoverer { DiscovererId = 2, Name = "Marek", Surname = "Markowski", Age = 34 },
                new Discoverer { DiscovererId = 3, Name = "Darek", Surname = "Darkowski", Age = 30 }
                );

        }
    }
}
