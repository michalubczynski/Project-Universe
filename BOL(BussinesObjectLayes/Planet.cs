using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Universe.Models.spaceobject;
using Universe.Models.starsystem;

namespace Universe.Models.planet
{
	public enum TypeOfPlanets
	{
		Gas_Giants,
		Dwarf_Planets,
		Super_Earth,
		Terrestrial_Planets,
		Outer_Planets
	}
	[Table("Planets")]
	public class Planet : DbEntity, IEntityTypeConfiguration<Planet>
	{
		public TypeOfPlanets Type { get; set; }
		[Range(0, sizeof(double))] public double Mass { get; set; }
		public int? StarSystemId { get; set; }
		[ForeignKey(nameof(StarSystemId))]
		public StarSystem? StarSystem { get; set; } // <ML> "?" is necessary?

		public void Configure(EntityTypeBuilder<Planet> builder)
		{
			builder.HasOne(s => s.StarSystem).WithMany(p => p.Planets).OnDelete(DeleteBehavior.Cascade);
		}
	}
}
