using FishPalAPI.Data;
using FishPalAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using PostmarkDotNet;
using System.Threading.Tasks;
using FishPalAPI.Models.MobileAppModels;
using FishPalAPI.Data.Member_Information.Geo_Province_Information;
using FishPalAPI.Data.Member_Information.Boat_Information;
using FishPalAPI.Data.Member_Information.Training;
using FishPalAPI.Data.Member_Information.Provincial_Information;
using FishPalAPI.Models.UserInformation.Boat_Information;

namespace FishPalAPI.Services
{
    public class UserService
    {
        private ApplicationDbContext context;

        public UserService()
        {
            context = new ApplicationDbContext();
        }

        public async Task<List<PublicEventDTO>> getPublicEvents()
        {
            var publicEvents = context.Events.Where(a => a.Approved == true).ToList();
            List<PublicEventDTO> result = new List<PublicEventDTO>();
            foreach(var entry in publicEvents)
            {
                result.Add(new PublicEventDTO()
                {
                    eventId = entry.Id,
                    startDate = entry.StartDate,
                    description = entry.Description,
                    endDate = entry.EndDate,
                    TypeOfEvent = entry.TypeOfEvent,
                    title = entry.Title
                });
            }
            return result;
        }

        public bool seed()
        {
            // This method is to seed the database to its basic form.

            // Seeding Roles
            var roles = context.Role.ToList();
            if (roles.Where(a => a.Description == "A1").FirstOrDefault() == null) { context.Role.Add(new Role { Description = "A1", FullName = "President" }); }
            if (roles.Where(a => a.Description == "A2").FirstOrDefault() == null) { context.Role.Add(new Role { Description = "A2", FullName = "Vice-President" }); }
            if (roles.Where(a => a.Description == "A3").FirstOrDefault() == null) { context.Role.Add(new Role { Description = "A3", FullName = "Secretary" }); }
            if (roles.Where(a => a.Description == "A4").FirstOrDefault() == null) { context.Role.Add(new Role { Description = "A4", FullName = "Treasurer" }); }
            if (roles.Where(a => a.Description == "A5").FirstOrDefault() == null) { context.Role.Add(new Role { Description = "A5", FullName = "Registration officer" }); }
            if (roles.Where(a => a.Description == "A6").FirstOrDefault() == null) { context.Role.Add(new Role { Description = "A6", FullName = "Records officer" }); }
            if (roles.Where(a => a.Description == "A7").FirstOrDefault() == null) { context.Role.Add(new Role { Description = "A7", FullName = "Conservation officer" }); }
            if (roles.Where(a => a.Description == "A8").FirstOrDefault() == null) { context.Role.Add(new Role { Description = "A8", FullName = "Colours officer" }); }
            if (roles.Where(a => a.Description == "A9").FirstOrDefault() == null) { context.Role.Add(new Role { Description = "A9", FullName = "Tournament official" }); }
            if (roles.Where(a => a.Description == "A10").FirstOrDefault() == null) { context.Role.Add(new Role { Description = "A10", FullName = "Development and Transformation officer" }); }
            if (roles.Where(a => a.Description == "A11").FirstOrDefault() == null) { context.Role.Add(new Role { Description = "A11", FullName = "Coach" }); }
            if (roles.Where(a => a.Description == "A12").FirstOrDefault() == null) { context.Role.Add(new Role { Description = "A12", FullName = "Anglers representative" }); }
            if (roles.Where(a => a.Description == "A13").FirstOrDefault() == null) { context.Role.Add(new Role { Description = "A13", FullName = "Officials commitee" }); }

            if (roles.Where(a => a.Description == "B1").FirstOrDefault() == null) { context.Role.Add(new Role { Description = "B1", FullName = "President" }); }
            if (roles.Where(a => a.Description == "B2").FirstOrDefault() == null) { context.Role.Add(new Role { Description = "B2", FullName = "Vice-President" }); }
            if (roles.Where(a => a.Description == "B3").FirstOrDefault() == null) { context.Role.Add(new Role { Description = "B3", FullName = "Secretary" }); }
            if (roles.Where(a => a.Description == "B4").FirstOrDefault() == null) { context.Role.Add(new Role { Description = "B4", FullName = "Treasurer" }); }
            if (roles.Where(a => a.Description == "B5").FirstOrDefault() == null) { context.Role.Add(new Role { Description = "B5", FullName = "Registration officer" }); }
            if (roles.Where(a => a.Description == "B6").FirstOrDefault() == null) { context.Role.Add(new Role { Description = "B6", FullName = "Records officer" }); }
            if (roles.Where(a => a.Description == "B7").FirstOrDefault() == null) { context.Role.Add(new Role { Description = "B7", FullName = "Conservation officer" }); }
            if (roles.Where(a => a.Description == "B8").FirstOrDefault() == null) { context.Role.Add(new Role { Description = "B8", FullName = "Colours officer" }); }
            if (roles.Where(a => a.Description == "B9").FirstOrDefault() == null) { context.Role.Add(new Role { Description = "B9", FullName = "Tournament official" }); }
            if (roles.Where(a => a.Description == "B10").FirstOrDefault() == null) { context.Role.Add(new Role { Description = "B10", FullName = "Development and Transformation officer" }); }
            if (roles.Where(a => a.Description == "B11").FirstOrDefault() == null) { context.Role.Add(new Role { Description = "B11", FullName = "Coach" }); }
            if (roles.Where(a => a.Description == "B12").FirstOrDefault() == null) { context.Role.Add(new Role { Description = "B12", FullName = "Anglers representative" }); }
            if (roles.Where(a => a.Description == "B13").FirstOrDefault() == null) { context.Role.Add(new Role { Description = "B13", FullName = "Officials commitee" }); }

            if (roles.Where(a => a.Description == "C1").FirstOrDefault() == null) { context.Role.Add(new Role { Description = "C1", FullName = "President" }); }
            if (roles.Where(a => a.Description == "C2").FirstOrDefault() == null) { context.Role.Add(new Role { Description = "C2", FullName = "Vice-President" }); }
            if (roles.Where(a => a.Description == "C3").FirstOrDefault() == null) { context.Role.Add(new Role { Description = "C3", FullName = "Secretary" }); }
            if (roles.Where(a => a.Description == "C4").FirstOrDefault() == null) { context.Role.Add(new Role { Description = "C4", FullName = "Treasurer" }); }
            if (roles.Where(a => a.Description == "C5").FirstOrDefault() == null) { context.Role.Add(new Role { Description = "C5", FullName = "Registration officer" }); }
            if (roles.Where(a => a.Description == "C6").FirstOrDefault() == null) { context.Role.Add(new Role { Description = "C6", FullName = "Records officer" }); }
            if (roles.Where(a => a.Description == "C7").FirstOrDefault() == null) { context.Role.Add(new Role { Description = "C7", FullName = "Conservation officer" }); }
            if (roles.Where(a => a.Description == "C8").FirstOrDefault() == null) { context.Role.Add(new Role { Description = "C7", FullName = "Colours officer" }); }
            if (roles.Where(a => a.Description == "C9").FirstOrDefault() == null) { context.Role.Add(new Role { Description = "C8", FullName = "Tournament official" }); }
            if (roles.Where(a => a.Description == "C10").FirstOrDefault() == null) { context.Role.Add(new Role { Description = "C9", FullName = "Development and Transformation officer" }); }
            if (roles.Where(a => a.Description == "C11").FirstOrDefault() == null) { context.Role.Add(new Role { Description = "C10", FullName = "Coach" }); }
            if (roles.Where(a => a.Description == "C12").FirstOrDefault() == null) { context.Role.Add(new Role { Description = "C11", FullName = "Anglers representative" }); }
            if (roles.Where(a => a.Description == "C13").FirstOrDefault() == null) { context.Role.Add(new Role { Description = "C12", FullName = "Officials commitee" }); }

            if (roles.Where(a => a.Description == "C1").FirstOrDefault() == null) { context.Role.Add(new Role { Description = "D1", FullName = "President" }); }
            if (roles.Where(a => a.Description == "C2").FirstOrDefault() == null) { context.Role.Add(new Role { Description = "D2", FullName = "Vice-President" }); }
            if (roles.Where(a => a.Description == "C3").FirstOrDefault() == null) { context.Role.Add(new Role { Description = "D3", FullName = "Secretary" }); }
            if (roles.Where(a => a.Description == "C4").FirstOrDefault() == null) { context.Role.Add(new Role { Description = "D4", FullName = "Treasurer" }); }
            if (roles.Where(a => a.Description == "C5").FirstOrDefault() == null) { context.Role.Add(new Role { Description = "D5", FullName = "Registration officer" }); }
            if (roles.Where(a => a.Description == "C6").FirstOrDefault() == null) { context.Role.Add(new Role { Description = "D6", FullName = "Records officer" }); }
            if (roles.Where(a => a.Description == "C7").FirstOrDefault() == null) { context.Role.Add(new Role { Description = "D7", FullName = "Conservation officer" }); }
            if (roles.Where(a => a.Description == "C8").FirstOrDefault() == null) { context.Role.Add(new Role { Description = "D8", FullName = "Colours officer" }); }
            if (roles.Where(a => a.Description == "C9").FirstOrDefault() == null) { context.Role.Add(new Role { Description = "D9", FullName = "Tournament official" }); }
            if (roles.Where(a => a.Description == "C10").FirstOrDefault() == null) { context.Role.Add(new Role { Description = "D10", FullName = "Development and Transformation officer" }); }
            if (roles.Where(a => a.Description == "C11").FirstOrDefault() == null) { context.Role.Add(new Role { Description = "D11", FullName = "Coach" }); }
            if (roles.Where(a => a.Description == "C12").FirstOrDefault() == null) { context.Role.Add(new Role { Description = "D12", FullName = "Anglers representative" }); }
            if (roles.Where(a => a.Description == "C13").FirstOrDefault() == null) { context.Role.Add(new Role { Description = "D13", FullName = "Officials commitee" }); }

            if (roles.Where(a => a.Description == "E1").FirstOrDefault() == null) { context.Role.Add(new Role { Description = "E1", FullName = "Club member" }); }
            if (roles.Where(a => a.Description == "E2").FirstOrDefault() == null) { context.Role.Add(new Role { Description = "E2", FullName = "Social member" }); }

            context.SaveChanges();

            return true;
        }

        public List<mobileUserInfoDTO> allUserInfo(int profileId, bool returnAll)
        {
            var currentProfile = context.UserProfiles.Include(a => a.userInformation).ThenInclude(a => a.boatInformation).Include(a => a.club).ThenInclude(a => a.Province)
                        .ThenInclude(a => a.Facets).Where(a => a.Id == profileId).FirstOrDefault();
            var currentUser = context.Users.Include(a => a.profiles).Where(a => a.profiles.Contains(currentProfile)).FirstOrDefault();
            List<UserProfile> specificProfile = new List<UserProfile>();
            if (returnAll == true)
            {
                foreach(var entry in currentUser.profiles)
                {
                    specificProfile.Add(context.UserProfiles.Include(a => a.userInformation).ThenInclude(a => a.boatInformation).Include(a => a.club).ThenInclude(a => a.Province)
                        .ThenInclude(a => a.Facets).Where(a => a.Id == entry.Id).FirstOrDefault());
                }
            }
            else
            {
                specificProfile.Add(currentProfile);
            }

            BoatInformationDTO boatInformation;

        List<mobileUserInfoDTO> result = new List<mobileUserInfoDTO>();
            foreach (var profile in specificProfile)
            {
                boatInformation = new BoatInformationDTO()
                {
                    BoatOwner = profile.userInformation.boatInformation.BoatOwner,
                    BoatNumber = profile.userInformation.boatInformation.BoatNumber,
                    HullType = profile.userInformation.boatInformation.HullType,
                    HullColour = profile.userInformation.boatInformation.HullColour,
                    MotorMake = profile.userInformation.boatInformation.MotorMake,
                    HorsePower = profile.userInformation.boatInformation.HorsePower,
                    TowVehicleRegistrationNumber = profile.userInformation.boatInformation.TowVehicleRegistrationNumber,
                    TrailerRegistrationNumber = profile.userInformation.boatInformation.TrailerRegistrationNumber,
                    CofNumber = profile.userInformation.boatInformation.CofNumber,
                    CofExpiryDate = profile.userInformation.boatInformation.CofExpiryDate
                };

            result.Add(new mobileUserInfoDTO()
                {
                    ProfileExpiryDate = profile.expiryDate,
                    ProfileId = profile.Id,
                    UserId = currentUser.Id,
                    Name = currentUser.Name,
                    Surname = currentUser.Surname,
                    FacetName = profile.club.Facet.Federation,
                    TypeName = profile.club.Facet.Name,
                    FacetId = profile.club.Facet.Id,
                    ClubId = profile.club.Id,
                    ClubName = profile.club.Name,
                    Province = profile.club.Province.Name,
                    ProvinceId = profile.club.Province.Id,
                    ProfileCreationDate = profile.creationTime,
                    FacetLogoBase64 = profile.club.Facet.Base64String,
                    BoatInfo = boatInformation
            });
            }
            return result;
        }

        public List<LoginProfilesDTO> getUserProfiles(User user)
        {
            var tempUser = context.Users.Include(a => a.profiles).Where(o => o.Id == user.Id).FirstOrDefault();
            if (tempUser != null)
            {
                List<LoginProfilesDTO> result = new List<LoginProfilesDTO>();
                foreach (var entry in tempUser.profiles)
                {
                    var tempProfile = context.UserProfiles.Include(a => a.club).ThenInclude(a=>a.Facet).ThenInclude(a => a.Provinces)
                        .Include(a => a.role).Where(a => a.Id == entry.Id).FirstOrDefault();
                    LoginProfilesDTO tempResult = new LoginProfilesDTO();
                    Facet tempFacet = null;
                    Club tempClub = null;
                    tempResult.Id = entry.Id;

                    if (tempProfile.role != null)
                    {
                        tempResult.Role = tempProfile.role.Description;
                    }
                    if (tempProfile.club != null)
                    {
                        tempClub = context.Clubs.Include(a => a.Facet).Where(a => a.Id == tempProfile.club.Id).FirstOrDefault();
                        tempResult.federation = tempClub.Facet.Federation;
                        tempResult.club = tempProfile.club.Name;
                        tempResult.Name = tempResult.federation + " role: " + tempResult.Role;
                        tempResult.federationId = tempProfile.club.Facet.Id;
                        tempResult.provinceId = tempProfile.club.Province.Id;
                        tempResult.userId = user.Id;
                    }
                    if (tempFacet != null)
                    {
                        tempResult.Name = tempFacet.Name + " " + tempProfile.role.Description;
                    }
                    result.Add(tempResult);
                };
                return result;
            }
                return null;
         }

        public User getLastUser()
        {
            return context.Users.OrderByDescending(a => a.EmployeeId).FirstOrDefault();
        }

        public bool addUserClubs(string userId, List<int> clubs)
        {
            var user = context.Users.Include(a => a.profiles).Where(a => a.Id == userId).FirstOrDefault();
            List<Club> tempClubs = new List<Club>();
            foreach(var entry in clubs)
            {
                tempClubs.Add(context.Clubs.Where(a => a.Id == entry).FirstOrDefault());
            }

            if (user != null)
            {
                foreach(var club in tempClubs)
                {
                    addUserProfileAndClubAndDefaultRole(user, club);
                }
                context.SaveChanges();
            }
            return true;
        }

        public void addUserProfileAndClubAndDefaultRole(User user, Club club)
        {
            Role tempRoleToAdd;
            var role = context.Role.Where(a => a.Description == "E1").FirstOrDefault();
            if(role == null)
            {
                context.Role.Add(new Role()
                {
                    Description = "E1",
                    FullName = "Club member",
                });
                context.SaveChanges();
                var newRole = context.Role.Where(a => a.Description == "E1").FirstOrDefault();

                tempRoleToAdd = newRole;
            } else
            {
                tempRoleToAdd = role;
            }

            UserProfile tempProfileToAdd = new UserProfile()
            {
                role = tempRoleToAdd,
                club = club,
                creationTime = DateTime.Now,
                userInformation = getDefaultUserInformation()
            };

            user.profiles.Add(tempProfileToAdd);
            context.SaveChanges();
        }

        public UserInformation getDefaultUserInformation()
        {
            PersonalInformation personalInfo = new PersonalInformation();
            MedicalInformation medicalInformation = new MedicalInformation();
            GeoProvinceInformation geoProvinceInformation = new GeoProvinceInformation();
            BoatInformation boatInformation = new BoatInformation();
            Training training = new Training();
            ProvincialInformation provincialInformation = new ProvincialInformation();
            List<OtherAnglingAchievements> otherAnglingAchievements = new List<OtherAnglingAchievements>();
            List<AnglishAdministrationHistory> anglishAdministrationHistories = new List<AnglishAdministrationHistory>();
            List<ProvincialInformationComtteeMembers> provincialInformationComtteeMembers = new List<ProvincialInformationComtteeMembers>();
            List<ProvincialInformationPriorPeriods> provincialInformationPriorPeriods = new List<ProvincialInformationPriorPeriods>();
            List<MedicalInformationAllergies> medicalInformationAllergies = new List<MedicalInformationAllergies>();
            List<MedicalInformationEmergencyContacts> medicalInformationEmergencyContacts = new List<MedicalInformationEmergencyContacts>();
            List<MedicalInformationMedicalConditions> medicalInformationMedicalConditions = new List<MedicalInformationMedicalConditions>();
            List<MedicalInformationPharmacies> medicalInformationPharmacies = new List<MedicalInformationPharmacies>();
            List<MedicalInformationPhysicians> medicalInformationPhysicians = new List<MedicalInformationPhysicians>();
            ClubInformation clubInformation = new ClubInformation();
            List<ClubInformationComitteeMembers> comitteeMembers = new List<ClubInformationComitteeMembers>();
            List<ClubInformationPriorPeriods> priorPeriods = new List<ClubInformationPriorPeriods>();
            clubInformation.ComitteeMembers = comitteeMembers;
            clubInformation.PriorPeriods = priorPeriods;
            provincialInformation.PriorPeriods = provincialInformationPriorPeriods;
            provincialInformation.ComitteeMembers = provincialInformationComtteeMembers;
            medicalInformation.MedicalInformationEmergencyContacts = medicalInformationEmergencyContacts;
            medicalInformation.MedicalInformationAllergies = medicalInformationAllergies;
            medicalInformation.MedicalInformationMedicalConditions = medicalInformationMedicalConditions;
            medicalInformation.MedicalInformationPharmacies = medicalInformationPharmacies;
            medicalInformation.MedicalInformationPhysicians = medicalInformationPhysicians;

            return new UserInformation
            {
                geoProvinceInformation = geoProvinceInformation,
                personalInformation = personalInfo,
                medicalInformation = medicalInformation,
                boatInformation = boatInformation,
                training = training,
                clubInformation = clubInformation,
                provincialInformation = provincialInformation,
                otherAnglingAchievements = otherAnglingAchievements,
                anglishAdministrationHistories = anglishAdministrationHistories
            };
        }

        public bool removeUserProfiles(string userId)
        {
            var user = context.Users.Include(a => a.profiles).Where(a => a.Id == userId).FirstOrDefault();
            user.profiles = null;
            context.SaveChanges();
            return true;
        }

        public async Task<bool> sendConfirmEmailAsync(User user)
        {
            var message = new TemplatedPostmarkMessage
            {
                From = "admin@fishpal.co.za",
                To = user.UserName,
                TemplateAlias = "confirmemail",
                TemplateModel = new Dictionary<string, object> {
                    { "name", user.Name + " " + user.Surname },
                    { "action_url", user.Id },
                    { "username", user.UserName }
                  },
            };

            var client = new PostmarkClient("fc1530d0-ed32-488f-8f40-33fb54be4801");

            var response = await client.SendMessageAsync(message);

            if (response.Status != PostmarkStatus.Success)
            {
                Console.WriteLine("Response was: " + response.Message);
            }
            return true;
        }

        public async Task<bool> sendResetPasswordEmailAsync(ResetPasswordDTO resetModel, string name)
        {
            var message = new TemplatedPostmarkMessage
            {
                From = "admin@fishpal.co.za",
                To = resetModel.userName,
                TemplateAlias = "password-reset",
                TemplateModel = new Dictionary<string, object> {
                        { "name", name },
                        { "token", resetModel.token },
                        { "username", resetModel.userName },
                },
            };

            var client = new PostmarkClient("fc1530d0-ed32-488f-8f40-33fb54be4801");

            var response = await client.SendMessageAsync(message);

            if (response.Status != PostmarkStatus.Success)
            {
                Console.WriteLine("Response was: " + response.Message);
            }
            return true;
        }
    }
}
