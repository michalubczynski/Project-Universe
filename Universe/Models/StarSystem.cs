using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Universe.Models
{
    [Table("StarSystems")]
    public class StarSystem
    {
        [Key] public int StarSystemId { get; set; }
        [Required] public int name { get; set; }

        // N:Starsystems-1:Galaxy
        public int GalaxyId { get; set; }
        [ForeignKey(nameof(GalaxyId))]
        public Galaxy Galaxy { get; set; }

        // 1:StarSystem-N:Planets
        public ICollection<Planet> Planets { get; set; }

        // 1:StarSystem-N:Stars
        public ICollection<Star> Stars { get; set; }

        // N:StarSystems-N:Discoverers
        public ICollection<Discoverer> Discoverers { get; set;}
        }
}

