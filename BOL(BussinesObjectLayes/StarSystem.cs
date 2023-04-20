using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Universe.Models.spaceobject;
using Universe.Models.galaxy;
using Universe.Models.planet;
using Universe.Models.star;
using Universe.Models.discoverer;

namespace Universe.Models.starsystem
{
    [Table("StarSystems")]
    public class StarSystem : SpaceObject
    {   //Wymaga poprawy w strukturze bo StarSystem nie ma Masy. To tylko kierunkowe skupiska gwiazd na sferze niebieskiej, a nie obiekty fizyczne z określoną masą. 
        [Required] public String Name { get; set; }

        // N:Starsystems-1:Galaxy
        public int GalaxyId { get; set; }
        [ForeignKey(nameof(GalaxyId))]
        public Galaxy Galaxy { get; set; }

        // 1:StarSystem-N:Planets
        public ICollection<Planet> Planets { get; set; }

        // 1:StarSystem-N:Stars
        public ICollection<Star> Stars { get; set; }

        // N:StarSystems-N:Discoverers
        public virtual ICollection<Discoverer> Discoverers { get; set;}
        }
}

