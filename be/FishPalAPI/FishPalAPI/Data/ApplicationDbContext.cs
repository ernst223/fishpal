using FishPalAPI.Data.Member_Information.Boat_Information;
using FishPalAPI.Data.Member_Information.Geo_Province_Information;
using FishPalAPI.Data.Member_Information.Provincial_Information;
using FishPalAPI.Data.Member_Information.Training;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;

namespace FishPalAPI.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext() { }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration)
            : base(options)
        {
            Database.Migrate();
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                //var connectionString = Configuration.GetConnectionString("DefaultConnection");

                // Basson server
                optionsBuilder.UseMySql("server = localhost; user id = root; Password = AWE7; database = fishPalDB; SSL Mode = None", new MySqlServerVersion(new Version(8, 0, 28)));
                
                // Basson localhost
                //optionsBuilder.UseMySql("server=localhost;user id=root;Password=awe7;database=fishPalDB", new MySqlServerVersion(new Version(8, 0, 28)));
                
                // Ernst localhost
                //optionsBuilder.UseMySql("server=localhost;user id=root;Password=Ernst123?;database=fishPalDB", new MySqlServerVersion(new Version(8, 0, 28)));
            }
        }

        public DbSet<Club> Clubs { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<UserInformation> UserInformation { get; set; }
        public DbSet<Facet> Facets { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<OrderItems> OrderItems { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<DocumentMessage> DocumentMessages { get; set; }
        public DbSet<Communication> Communications { get; set; }
        public DbSet<CommunicationMessage> CommunicationMessages { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<PersonalInformation> PersonalInformation { get; set; }
        public DbSet<MedicalInformation> MedicalInformation { get; set; }
        public DbSet<GeoProvinceInformation> GeoProvinecInformation { get; set; }
        public DbSet<BoatInformation> BoatInformation { get; set; }
        public DbSet<Training> Training { get; set; }
        public DbSet<MedicalInformationPhysicians> MedicalInformationPhysicians { get; set; }
        public DbSet<MedicalInformationPharmacies> MedicalInformationPharmacies { get; set; }
        public DbSet<MedicalInformationEmergencyContacts> MedicalInformationEmergencyContacts { get; set; }
        public DbSet<MedicalInformationMedicalConditions> MedicalInformationMedicalConditions { get; set; }
        public DbSet<MedicalInformationAllergies> MedicalInformationAllergies { get; set; }
        public DbSet<ClubInformation> ClubInformation { get; set; }
        public DbSet<ClubInformationComitteeMembers> ClubInformationComitteeMembers { get; set; }
        public DbSet<ClubInformationPriorPeriods> ClubInformationPriorPeriods { get; set; }
        public DbSet<ProvincialInformation> ProvincialInformation { get; set; }
        public DbSet<ProvincialInformationPriorPeriods> ProvincialInformationPriorPeriods { get; set; }
        public DbSet<ProvincialInformationComtteeMembers> ProvincialInformationComtteeMembers { get; set; }
        public DbSet<AnglishAdministrationHistory> AnglishAdministrationHistories { get; set; }
        public DbSet<OtherAnglingAchievements> OtherAnglingAchievements { get; set; }
        public DbSet<Courses> Courses { get; set; }
        public DbSet<UserCourses> UserCourses { get; set; }
        public DbSet<Event> Events { get; set; }
    }
}
