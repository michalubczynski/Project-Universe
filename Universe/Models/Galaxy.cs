using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Universe.Models
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
