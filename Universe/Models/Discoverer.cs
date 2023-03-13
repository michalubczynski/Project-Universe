using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Universe.Models
{
    [Table("Discoverers")]
    public class Discoverer
    {
        [Key] public int DiscovererId { get; set; }
        [Required] public string Name { get; set; }
        [Required] public string Surname { get; set; }
        [Required] public int Age { get; set; }
        // 1:Discoverer-1:Ship
         public Ship? Ship { get; set; }
    }
}
