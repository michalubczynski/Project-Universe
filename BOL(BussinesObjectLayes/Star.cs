using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Universe.Models.starsystem;

namespace Universe.Models.star
{
    [Table("Stars")]
    public class Star : IEntityTypeConfiguration<Star>
    {
        public enum TypeOfStar
        {
            Main_sequence_stars,
            Red_giant_stars,
            White_dwarf_stars,
            Neutron_stars,
            Black_holes
        }

        [Key]
        public int StarId { get; set; }
        [Required] public string Name { get; set; }
        [Required] public TypeOfStar Type { get; set; }
        [Required] public double Temperature { get; set; }
        [Required] public double Radius { get; set; }
        [Required] public int Age { get; set; }
        [Required] public double Luminosity { get; set;}
        [Required] public double Mass { get; set;}

        public int StarSystemId { get; set; }
        [ForeignKey(nameof(StarSystemId))]
        public StarSystem StarSystem { get; set; } // <ML> "?" is necessary?

        public void Configure(EntityTypeBuilder<Star> builder)
        {
            builder.HasOne(s => s.StarSystem).WithMany(st => st.Stars).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
