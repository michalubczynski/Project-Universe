using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Universe.Models.discoverer;
using Universe.Models.spaceobject;


namespace Universe.Models.ship
{
    [Table("Ships")]
    public class Ship : SpaceObject
    {
        [Required][RegularExpression("^[s,S]tar[s,S]hip_\\d+$")] public string ShipName { get; set; } // example Starship_1, starShip_2 etc.
        [RegularExpression("^[m,M]\\d+$")] public string ShipModel { get; set; } // example m12, M0001 etc.
        [Range(1, sizeof(double))] public int MaxSpeed { get; set; } // (km/s)
        [Range(1, sizeof(double))] public int SingleChargeRange { get; set; }
        // 1:Ship-1:Discoverer
        public Discoverer? Discoverer { get; set; }
        public bool IfBroken { get; set; }
}
}
