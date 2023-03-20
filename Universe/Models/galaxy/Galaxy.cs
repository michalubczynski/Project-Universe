using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Universe.Models.planet;

namespace Universe.Models.galaxy
{
    [Table("Galaxies")]
    public class Galaxy
    {

        [Required] public int GalaxyId { get; set; }
        [Required] public string Name { get; set; }
         public string Type { get; set; }
         public double Mass { get; set; }

        // 1:Galaxy-N:StarSystems
        public ICollection<Planet>? StarSystems { get; set; } 

    }
}
