using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FishPalAPI.Data
{
    public class DocumentMessage
    {
        [Key]
        public int Id { get; set; }
        public UserProfile Recipient { get; set; }
        public Document Document { get; set; }
        public bool Acknowledged { get; set; }
    }
}
