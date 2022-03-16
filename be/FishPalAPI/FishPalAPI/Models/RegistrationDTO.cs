using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FishPalAPI.Models
{
    public class RegistrationDTO
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public List<int> clubs { get; set; }
    }
}
