using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FishPalAPI.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext() { }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            // TO DO
            // Make this line work

            //Database.Migrate();
        }

        public DbSet<Club> Clubs { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<UserInformation> UserInformation { get; set; }
    }
}
