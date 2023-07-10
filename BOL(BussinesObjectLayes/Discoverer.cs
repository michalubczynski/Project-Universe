using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Universe.Models.ship;
using Universe.Models.spaceobject;
using Universe.Models.starsystem;

namespace Universe.Models.discoverer
{
	[Table("Discoverers")]
	public class Discoverer : DbEntity
	{
		[Required] public string Surname { get; set; }
		[Required] public int Age { get; set; }
		public int? ShipId { get; set; }
		[ForeignKey(nameof(ShipId))]
		// 1:Discoverer-1:Ship
		[JsonIgnore] // dto
		public Ship? Ship { get; set; }
		// N:StarSystems-N:Discoverers
		public virtual ICollection<StarSystem> StarSystems { get; set; }
	}
}
