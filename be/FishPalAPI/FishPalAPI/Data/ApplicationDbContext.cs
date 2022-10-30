using FishPalAPI.Data.Communication;
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
                //optionsBuilder.UseMySql("datasource = localhost; port = 7777; username = root; password = awe7; database = fishPalDB", new MySqlServerVersion(new Version(8, 0, 28)));
                //optionsBuilder.UseMySql("server=localhost;user id = root; Password=awe7;database=fishPalDB", new MySqlServerVersion(new Version(8, 0, 28)));
                optionsBuilder.UseMySql("server=localhost;user id=root;Password=Ernst123?;database=fishPalDB", new MySqlServerVersion(new Version(8, 0, 28)));
            }
        }

        public DbSet<Club> Clubs { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<UserInformation> UserInformation { get; set; }
        public DbSet<Facet> Facets { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<OrderItems> OrderItems { get; set; }
        public DbSet<Messages> Messages { get; set; }
        public DbSet<MessageReceivers> MessageReceivers { get; set; }
        public DbSet<Federation> Federation { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<DocumentMessage> DocumentMessages { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<PersonalInformation> PersonalInformation { get; set; }
    }
}
