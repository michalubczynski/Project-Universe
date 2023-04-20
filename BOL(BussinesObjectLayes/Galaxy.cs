using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Universe.Models.spaceobject;
using Universe.Models.planet;

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
    public class Galaxy : SpaceObject
    {
        [Required] public string Name { get; set; }
         public TypeOfGalaxy Type { get; set; }
         public double Mass { get; set; } // 'M☉' unit is Solar Mass

        // 1:Galaxy-N:StarSystems
        public ICollection<Planet>? StarSystems { get; set; } 

    }
}
