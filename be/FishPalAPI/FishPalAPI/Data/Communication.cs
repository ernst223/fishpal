using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FishPalAPI.Data
{
    public class Communication
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Note { get; set; }
        public UserProfile CreatedBy { get; set; }
        public bool Aproved { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime AprovalDate { get; set; }
    }
}
