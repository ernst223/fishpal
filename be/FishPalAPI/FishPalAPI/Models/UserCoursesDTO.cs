using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FishPalAPI.Models
{
    public class UserCoursesDTO
    {
        public int Id { get; set; }
        public int profileId { get; set; }
        public string userName { get; set; }
        public string userEmail { get; set; }
        public string memberNumber { get; set; }
        public int courseId { get; set; }
        public string courseName { get; set; }
        public string courseDescription { get; set; }
        public bool Approved { get; set; }
    }
}
