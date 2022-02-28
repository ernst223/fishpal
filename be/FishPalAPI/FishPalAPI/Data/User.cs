using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FishPalAPI.Data
{
    public class User: IdentityUser
    {
        public UserInformation userInformation { get; set; }
        public Role role { get; set; }
    }
}
