using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FishPalAPI.Models
{
    public class LoginProfilesDTO
    {
        public int Id { get; set; }
        public string userId { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public string club { get; set; }
        public string federation { get; set; }

        public int federationId { get; set; }

        public int provinceId { get; set; }
    }
}
