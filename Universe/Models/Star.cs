using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Universe.Models
{
    [Table("Stars")]
    public class Star : IEntityTypeConfiguration<Star>
    {
        [Key]
        public int StarId { get; set; }
        [Required] public string Name { get; set; }
        [Required] public string Type { get; set; }
        [Required] public double Temperature { get; set; }
        [Required] public double Radius { get; set; }
        [Required] public int Age { get; set; }
        [Required] public int Luminosity { get; set;}
        [Required] public int Mass { get; set;}

        public int StarSystemId { get; set; }
        [ForeignKey(nameof(StarSystemId))]
        public StarSystem StarSystem { get; set; } // <ML> "?" is necessary?

        public void Configure(EntityTypeBuilder<Star> builder)
        {
            builder.HasOne(s=>s.StarSystem).WithMany(st=>st.Stars).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
