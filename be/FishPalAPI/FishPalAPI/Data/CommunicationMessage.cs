using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FishPalAPI.Data
{
    public class CommunicationMessage
    {
        [Key]
        public int Id { get; set; }
        public UserProfile Recipient { get; set; }
        public Communication Communication { get; set; }
    }
}
