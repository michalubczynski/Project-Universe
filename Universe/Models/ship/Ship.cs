﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Universe.Models.discoverer;

namespace Universe.Models.ship
{
    [Table("Ships")]
    public class Ship
    {
        [Key]
        public int ShipId { get; set; }
        [Required]
        public string ShipName { get; set; }
        public string ShipModel { get; set; }
        public int MaxSpeed { get; set; }
        public int SingleChargeRange { get; set; }
        // 1:Ship-1:Discoverer
        public Discoverer? Discoverer { get; set; }


    }
}