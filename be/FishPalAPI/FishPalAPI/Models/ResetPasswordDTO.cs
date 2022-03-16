using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FishPalAPI.Models
{
    public class ResetPasswordDTO
    {
        public string token { get; set; }
        public string newPassword { get; set; }
        public string userName { get; set; }
    }
}
