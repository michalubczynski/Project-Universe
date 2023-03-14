using Microsoft.EntityFrameworkCore;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using DbContext = System.Data.Entity.DbContext;

namespace Universe.Models
{
    public class DbUniverse : DbContext
    {
        public System.Data.Entity.DbSet<Galaxy> Galaxies { get; set; }
        public System.Data.Entity.DbSet<StarSystem> StarSystems { get; set; }
        public System.Data.Entity.DbSet<Star> Stars { get; set; }
        public System.Data.Entity.DbSet<Discoverer> Discoverers { get; set; }
        public System.Data.Entity.DbSet<Ship> Ships { get; set; }
        public System.Data.Entity.DbSet<Planet> Planets { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }


    }
}
