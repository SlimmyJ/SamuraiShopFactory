using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroToEF.Data.Entities
    {
    public class BattleSamurai
        {
        public int SamuraisId { get; set; }
        public Samurai Samurai { get; set; }
        public int BattlesId { get; set; }
        public Battle Battle { get; set; }
        }
    }