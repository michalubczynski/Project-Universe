using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Universe.Models.starsystem;

namespace Universe.Models.planet
{
    [Table("Planets")]
    public class Planet : IEntityTypeConfiguration<Planet>
    {
        [Key]
        public int PlanetId { get; set; }
        [Required] public string Name { get; set;}
        public string Type { get; set; }
        public double Mass { get; set; }
        public int? StarSystemId { get; set; }
        [ForeignKey(nameof(StarSystemId))]
        public StarSystem? StarSystem { get; set; } // <ML> "?" is necessary?

        public void Configure(EntityTypeBuilder<Planet> builder)
        {
            builder.HasOne(s => s.StarSystem).WithMany(p => p.Planets).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
