﻿using System.ComponentModel.DataAnnotations.Schema;

namespace NETCORE_7_WepApiProject.Models
{
    public class Character
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; } = "Fredo";
        public int HitPoints { get; set; } = 100;
        public int Strength { get; set; } = 10;
        public int Defense { get; set; } = 10;
        public int Intelligent { get; set; } = 10;
        public RpgClass Class { get; set; } = RpgClass.Knight;

    }
}
