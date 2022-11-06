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

namespace FishPalAPI.Services
{
    public class UserService
    {
        private ApplicationDbContext context;

        public UserService()
        {
            context = new ApplicationDbContext();
        }

        public List<mobileUserInfoDTO> allUserInfo(string userName, int? federationId)
        {
            var currentUser = context.Users.Include(x => x.profiles).ThenInclude(x => x.club).ThenInclude(x => x.Province).ThenInclude(x => x.Facets).Where(x => x.UserName == userName).FirstOrDefault();

            List<UserProfile> specificProfile = new List<UserProfile>();

            if (federationId != null)
            {
                specificProfile = currentUser.profiles.Where(x => x.club.Facet.Id == federationId).ToList();
            }else{
                specificProfile = currentUser.profiles;
            }

            List<mobileUserInfoDTO> result = new List<mobileUserInfoDTO>();
            foreach (var profile in specificProfile)
            {
                result.Add(new mobileUserInfoDTO()
                {
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
                    FacetLogoBase64 = profile.club.Facet.Base64String
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
            var role = context.Role.Where(a => a.Description == "E0").FirstOrDefault();
            if(role == null)
            {
                context.Role.Add(new Role()
                {
                    Description = "E0"
                });
                context.SaveChanges();
                var newRole = context.Role.Where(a => a.Description == "E0").FirstOrDefault();

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
