using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Linq.Expressions;
using Universe.Models.discoverer;
using Universe.Models.galaxy;
using Universe.Models.planet;
using Universe.Models.ship;
using Universe.Models.star;
using Universe.Models.starsystem;

namespace Models
{
	public class DbUniverse : DbContext
	{
		public DbSet<Galaxy> Galaxies { get; set; }
		public DbSet<StarSystem> StarSystems { get; set; }
		public DbSet<Star> Stars { get; set; }
		public DbSet<Discoverer> Discoverers { get; set; }
		public DbSet<Ship> Ships { get; set; }
		public DbSet<Planet> Planets { get; set; }


		public DbUniverse(DbContextOptions<DbUniverse> options):base(options) { 
		
		}

		public async Task<int> CountAsync<T>(Expression<Func<T, bool>> predicate = null) where T : class
		{
			var entities = Set<T>();
			if (predicate == null)
			{
				return await entities.CountAsync();
			}
			else
			{
				return await entities.CountAsync(predicate);
			}
		}
		public async Task<bool> AnyAsync<T>(Expression<Func<T, bool>> predicate = null) where T : class
		{
			var entities = Set<T>();
			if (predicate == null)
			{
				return await entities.AnyAsync();
			}
			else
			{
				return await entities.AnyAsync(predicate);
			}
		}
		public async Task<T> FirstOrDefaultAsync<T>(Expression<Func<T, bool>> predicate = null) where T : class
		{
			return await Set<T>().FirstOrDefaultAsync(predicate);
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			string connectionString = "Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = baza; Integrated Security = True; Connect Timeout = 30; Encrypt = False; Trust Server Certificate = False; Application Intent = ReadWrite; Multi Subnet Failover = False";
			optionsBuilder.UseSqlServer(connectionString);
		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Ship>().HasOne(d => d.Discoverer).WithOne(s => s.Ship).HasForeignKey<Discoverer>(d => d.ShipId);
			modelBuilder.ApplyConfigurationsFromAssembly(System.Reflection.Assembly.GetExecutingAssembly());

			//FluentApi Refilling data
			modelBuilder.Entity<Galaxy>().HasData(
			new Galaxy { Id = 1, Mass = 10e12, Name = "Droga Mleczna", Type = TypeOfGalaxy.spiral },
			new Galaxy { Id = 2, Mass = 10e12, Name = "Messier 87", Type = TypeOfGalaxy.elliptical },
			new Galaxy { Id = 3, Mass = 10e15, Name = "GAL-CLUS-022058s", Type = TypeOfGalaxy.peculiar },
			new Galaxy { Id = 4, Mass = 10e9, Name = "Wielka Mgławica Magellana", Type = TypeOfGalaxy.irregular }
			);
			modelBuilder.Entity<StarSystem>().HasData(
			new StarSystem { Id = 1, Name = "Wolarz", GalaxyId = 1 },
			new StarSystem { Id = 2, Name = "Orzel", GalaxyId = 2 },
			new StarSystem { Id = 3, Name = "Skorpion", GalaxyId = 3 },
			new StarSystem { Id = 4, Name = "Strzelec", GalaxyId = 4 }
				);
			modelBuilder.Entity<Planet>().HasData(
				new Planet { Id = 1, Mass = 2e12, Type = TypeOfPlanets.Gas_Giants, Name = "Jupiter", StarSystemId = 1 },
				new Planet { Id = 2, Mass = 2e12, Type = TypeOfPlanets.Gas_Giants, Name = "Neptune", StarSystemId = 1 },
				new Planet { Id = 3, Mass = 2e12, Type = TypeOfPlanets.Gas_Giants, Name = "Uranus", StarSystemId = 1 },
				new Planet { Id = 4, Mass = 2e12, Type = TypeOfPlanets.Gas_Giants, Name = "Saturn", StarSystemId = 1 },
				new Planet { Id = 5, Mass = 3e12, Type = TypeOfPlanets.Dwarf_Planets, Name = "Pluto", StarSystemId = 1 },
				new Planet { Id = 6, Mass = 4e12, Type = TypeOfPlanets.Super_Earth, Name = "Kepler-438b", StarSystemId = 2 },
				new Planet { Id = 7, Mass = 5e12, Type = TypeOfPlanets.Terrestrial_Planets, Name = "Earth", StarSystemId = 1 },
				new Planet { Id = 8, Mass = 5e12, Type = TypeOfPlanets.Terrestrial_Planets, Name = "Mars", StarSystemId = 1 },
				new Planet { Id = 9, Mass = 6e12, Type = TypeOfPlanets.Outer_Planets, Name = "Charon", StarSystemId = 3 }
				);
			modelBuilder.Entity<Star>().HasData(
				new Star { Id = 1, Age = (int)10e3, Luminosity = 23, Mass = 10e5, Name = "Zeta", Radius = 22.1, Temperature = 30, Type = Star.TypeOfStar.Main_sequence_stars, StarSystemId = 1 },
				new Star { Id = 2, Age = (int)10e2, Luminosity = 233, Mass = 10e4, Name = "Aldebaran", Radius = 232.1, Temperature = 303, Type = Star.TypeOfStar.Red_giant_stars, StarSystemId = 1 },
				new Star { Id = 3, Age = (int)10e3, Luminosity = 3, Mass = 10e5, Name = "SiriusB", Radius = 322.1, Temperature = 130, Type = Star.TypeOfStar.White_dwarf_stars, StarSystemId = 1 },
				new Star { Id = 4, Age = (int)10e4, Luminosity = 235, Mass = 10e7, Name = "PSR_B1509-58", Radius = 2.1, Temperature = 0, Type = Star.TypeOfStar.Neutron_stars, StarSystemId = 3 },
				new Star { Id = 5, Age = (int)10e4, Luminosity = 0, Mass = 10e7, Name = "Cygnus X-1", Radius = 33334.1, Temperature = -22, Type = Star.TypeOfStar.Black_holes, StarSystemId = 2 }
				);
			modelBuilder.Entity<Ship>().HasData(
				new Ship { Id = 1, MaxSpeed = 10, ShipModel = "m0001", Name = "StarShip_1", SingleChargeRange = 12, IfBroken = false, Discoverer = null },
				new Ship { Id = 2, MaxSpeed = 100, ShipModel = "m0002", Name = " StarShip_2", SingleChargeRange = 120, IfBroken = false },
				new Ship { Id = 3, MaxSpeed = 1000, ShipModel = "m0003", Name = "StarShip_3", SingleChargeRange = 122, IfBroken = true }
			);
			modelBuilder.Entity<Discoverer>().HasData(
				new Discoverer { Id = 1, Name = "Piotrek", Surname = "Piotrowski", Age = 43, ShipId = 1 },
				new Discoverer { Id = 2, Name = "Marek", Surname = "Markowski", Age = 34 },
				new Discoverer { Id = 3, Name = "Darek", Surname = "Darkowski", Age = 30 }
			);
		}
	}
}
