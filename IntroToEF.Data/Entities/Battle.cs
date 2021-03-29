﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IntroToEF.Data.Entities
    {
    public class Battle
        {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public int Year { get; set; }

        [MaxLength(100)]
        public string Location { get; set; }

        public List<Samurai> Samurais { get; set; } = new List<Samurai>();
        }
    }