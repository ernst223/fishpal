using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FishPalAPI.Models.UserInformation.Other
{
    public class OtherAnglingAchievementsDTO
    {
        public int Id { get; set; }
        public string Achievement { get; set; }
        public string Year { get; set; }
        public string Team { get; set; }
        public string TeamMembers { get; set; }
        public string Tournament { get; set; }
        public string Venue { get; set; }
    }
}
