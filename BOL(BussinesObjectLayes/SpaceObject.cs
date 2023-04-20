using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Universe.Models.spaceobject
{
    public class SpaceObject
    {
        [Key][Required] public int Id { get; set; }
    }
}
