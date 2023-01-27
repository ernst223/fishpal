using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FishPalAPI.Models
{
    public class EventDTO
    {
        public int eventId { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public string TypeOfEvent { get; set; }
        public string userProfile { get; set; }
        public string userEmail { get; set; }
        public string memberNumber { get; set; }
    }
}
