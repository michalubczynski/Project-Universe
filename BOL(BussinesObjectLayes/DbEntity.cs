using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Universe.Models.spaceobject
{
	public class DbEntity
	{
		[Key][Required] public int Id { get; set; }
		[Required][StringLength(100)] public string Name { get; set; }
	}
}
