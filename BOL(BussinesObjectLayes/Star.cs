using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Universe.Models.spaceobject;
using Universe.Models.starsystem;

namespace Universe.Models.star
{
	[Table("Stars")]
	public class Star : DbEntity, IEntityTypeConfiguration<Star>
	{
		public enum TypeOfStar
		{
			Main_sequence_stars,
			Red_giant_stars,
			White_dwarf_stars,
			Neutron_stars,
			Black_holes
		}
		[Required] public TypeOfStar Type { get; set; }
		[Required][Range(1, sizeof(double))] public double Temperature { get; set; }
		[Required][Range(1, sizeof(double))] public double Radius { get; set; }
		[Required][Range(1, sizeof(double))] public int Age { get; set; }
		[Required][Range(1, sizeof(double))] public double Luminosity { get; set;}
		[Required][Range(1, sizeof(double))] public double Mass { get; set;}

		public int StarSystemId { get; set; }
		[ForeignKey(nameof(StarSystemId))]
		public StarSystem StarSystem { get; set; } 

		public void Configure(EntityTypeBuilder<Star> builder)
		{
			builder.HasOne(s => s.StarSystem).WithMany(st => st.Stars).OnDelete(DeleteBehavior.Cascade);
		}
	}
}
