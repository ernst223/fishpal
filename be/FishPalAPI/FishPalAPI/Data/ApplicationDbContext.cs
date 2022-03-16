using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace FishPalAPI.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext() { }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            Database.Migrate();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql("server=localhost;user id=root;Password=Ernst123?;database=fishPalDB", new MySqlServerVersion(new Version(8, 0, 28)));
            }
        }

        public DbSet<Club> Clubs { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<UserInformation> UserInformation { get; set; }
        public DbSet<Facet> Facets { get; set; }
        public DbSet<Role> Role { get; set; }
    }
}
