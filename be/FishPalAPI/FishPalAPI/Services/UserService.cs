using FishPalAPI.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FishPalAPI.Services
{
    public class UserService
    {
        private ApplicationDbContext context;

        public UserService()
        {
            context = new ApplicationDbContext();
        }

        public string getUserRole(User user)
        {
            //return context.Users.Include(a => a.role).Where(o => o.Id == user.Id).FirstOrDefault().role.Description;
            return "A3";
        }
    }
}
