using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FishPalAPI.Data
{
    public class User: IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public UserInformation userInformation { get; set; }
        public Role role { get; set; }
        public List<Club> clubs { get; set; }
    }
}
