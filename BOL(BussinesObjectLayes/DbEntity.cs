using System.ComponentModel.DataAnnotations;

namespace Universe.Models.spaceobject
{
    public class DbEntity
    {
        [Key][Required] public int Id { get; set; }
        [Required][StringLength(100)] public string Name { get; set; }
    }
}
