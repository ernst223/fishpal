using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FishPalAPI.Data
{
    public class OtherAnglingAchievements
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Achievement { get; set; }
        public string Year { get; set; }
        public string Team { get; set; }
        public string TeamMembers { get; set; }
        public string Tournament { get; set; }
        public string Venue { get; set; }
    }
}
