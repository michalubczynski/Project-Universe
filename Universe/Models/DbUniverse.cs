using Microsoft.EntityFrameworkCore;

namespace Universe.Models
{
    public class DbUniverse : DbContext
    {
        public DbSet<Galaxy> Galaxies { get; set; }
        public DbSet<StarSystem> StarSystems { get; set; }
        public DbSet<Star> Stars { get; set; }
        public DbSet<Discoverer> Discoverers { get; set;}
        public DbSet<Ship> Ships { get; set; }
        public DbSet<Planet> Planets { get; set; }

    }
}
