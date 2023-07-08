using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Universe.Models.spaceobject;
using Universe.Models.planet;
using Universe.Models.starsystem;

namespace Universe.Models.galaxy
{
	public enum TypeOfGalaxy
	{
		spiral, //spiralna
		elliptical, //eliptyczna
		peculiar, // soczewkowata
		irregular //nieregularna
	}
	[Table("Galaxies")]
	public class Galaxy : DbEntity
	{
		 public TypeOfGalaxy Type { get; set; }
		[Range(1, sizeof(double))] public double Mass { get; set; } // 'M☉' unit of Solar Mass

		// 1:Galaxy-N:StarSystems
		public ICollection<StarSystem>? StarSystems { get; set; } 

	}
}
