using AutoMapper;
using FishPalAPI.Data;
using FishPalAPI.Models;
using FishPalAPI.Models.RoleManagement;
using FishPalAPI.Models.UserInformation.ClubInformation;
using FishPalAPI.Models.UserInformation.MedicalInformation;
using FishPalAPI.Models.UserInformation.Other;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FishPalAPI.Services
{
    public class UserInformationService
    {
        private ApplicationDbContext context;
        private UserService userService;
        private CommunicationService communicationService;
        private readonly IMapper _mapper;

        public UserInformationService(IMapper mapper)
        {
            _mapper = mapper;
            context = new ApplicationDbContext();
            userService = new UserService();
            communicationService = new CommunicationService();
        }

        public List<OtherAnglingAchievementsDTO> getOtherAnglingAchievements(int profileId)
        {
            var currentProfile = context.UserProfiles.Include(a => a.userInformation).ThenInclude(a => a.otherAnglingAchievements)
                .Where(a => a.Id == profileId).FirstOrDefault();
            if (currentProfile.userInformation == null)
            {
                currentProfile.userInformation = userService.getDefaultUserInformation();
                context.SaveChanges();

                // Refetching
                currentProfile = context.UserProfiles.Include(a => a.userInformation).ThenInclude(a => a.otherAnglingAchievements)
                .Where(a => a.Id == profileId).FirstOrDefault();
            }

            List<OtherAnglingAchievementsDTO> otherAnglingAchievements = new List<OtherAnglingAchievementsDTO>();
            foreach (var entry in currentProfile.userInformation.otherAnglingAchievements)
            {
                otherAnglingAchievements.Add(_mapper.Map<OtherAnglingAchievementsDTO>(entry));
            }

            return otherAnglingAchievements;
        }

        public void updateOtherAnglingAchievements(List<OtherAnglingAchievementsDTO> otherAnglingAchievementsDTO, int profileId)
        {
            var currentProfile = context.UserProfiles.Include(a => a.userInformation).ThenInclude(a => a.otherAnglingAchievements)
                .Where(a => a.Id == profileId).FirstOrDefault();

            List<OtherAnglingAchievements> otherAnglingAchievements = new List<OtherAnglingAchievements>();
            foreach (var entry in otherAnglingAchievementsDTO)
            {
                otherAnglingAchievements.Add(_mapper.Map<OtherAnglingAchievements>(entry));
            }
            foreach (var deleteEntry in currentProfile.userInformation.otherAnglingAchievements)
            {
                context.Remove(deleteEntry);
            }

            currentProfile.userInformation.otherAnglingAchievements = otherAnglingAchievements;

            context.SaveChanges();
        }

        public List<AnglishAdministrationHistoryDTO> getAnglishAdministrationHistory(int profileId)
        {
            var currentProfile = context.UserProfiles.Include(a => a.userInformation).ThenInclude(a => a.anglishAdministrationHistories)
                .Where(a => a.Id == profileId).FirstOrDefault();
            if (currentProfile.userInformation == null)
            {
                currentProfile.userInformation = userService.getDefaultUserInformation();
                context.SaveChanges();

                // Refetching
                currentProfile = context.UserProfiles.Include(a => a.userInformation).ThenInclude(a => a.anglishAdministrationHistories)
                .Where(a => a.Id == profileId).FirstOrDefault();
            }

            List<AnglishAdministrationHistoryDTO> anglishAdministrationHistories = new List<AnglishAdministrationHistoryDTO>();
            foreach (var entry in currentProfile.userInformation.anglishAdministrationHistories)
            {
                anglishAdministrationHistories.Add(_mapper.Map<AnglishAdministrationHistoryDTO>(entry));
            }

            return anglishAdministrationHistories;
        }

        public void updateAnglishAdministrationHistories(List<AnglishAdministrationHistoryDTO> anglishAdministrationHistoriesDTO, int profileId)
        {
            var currentProfile = context.UserProfiles.Include(a => a.userInformation).ThenInclude(a => a.anglishAdministrationHistories)
                .Where(a => a.Id == profileId).FirstOrDefault();

            List<AnglishAdministrationHistory> anglishAdministrationHistories = new List<AnglishAdministrationHistory>();
            foreach (var entry in anglishAdministrationHistoriesDTO)
            {
                anglishAdministrationHistories.Add(_mapper.Map<AnglishAdministrationHistory>(entry));
            }
            foreach (var deleteEntry in currentProfile.userInformation.anglishAdministrationHistories)
            {
                context.Remove(deleteEntry);
            }

            currentProfile.userInformation.anglishAdministrationHistories = anglishAdministrationHistories;

            context.SaveChanges();
        }

        public ClubInformationDTO getClubInformation(int profileId)
        {
            var currentProfile = context.UserProfiles.Include(a => a.userInformation).ThenInclude(a => a.clubInformation)
                .Where(a => a.Id == profileId).FirstOrDefault();
            var currentUser = context.Users.Include(a => a.profiles).Where(a => a.profiles.Contains(currentProfile)).FirstOrDefault();
            if (currentProfile.userInformation == null)
            {
                currentProfile.userInformation = userService.getDefaultUserInformation();
                context.SaveChanges();

                // Refetching
                currentProfile = context.UserProfiles.Include(a => a.userInformation).ThenInclude(a => a.medicalInformation)
                .Where(a => a.Id == profileId).FirstOrDefault();
            }

            var currentClubInformation = context.ClubInformation
                .Include(a => a.PriorPeriods).Include(a => a.ComitteeMembers)
                .Where(a => a.Id == currentProfile.userInformation.clubInformation.Id).FirstOrDefault();

            List<ClubInformationComitteeMembersDTO> comitteeMembers = new List<ClubInformationComitteeMembersDTO>();
            foreach (var entry in currentClubInformation.ComitteeMembers)
            {
                comitteeMembers.Add(_mapper.Map<ClubInformationComitteeMembersDTO>(entry));
            }

            List<ClubInformationPriorPeriodsDTO> priorPeriods = new List<ClubInformationPriorPeriodsDTO>();
            foreach (var entry in currentClubInformation.PriorPeriods)
            {
                priorPeriods.Add(_mapper.Map<ClubInformationPriorPeriodsDTO>(entry));
            }

            return new ClubInformationDTO()
            {
                ClubName = currentClubInformation.ClubName,
                ClubPeriod = currentClubInformation.ClubPeriod,
                ClubConstitutionRecieved = currentClubInformation.ClubConstitutionRecieved,
                ClubConstitutionDateAccepted = currentClubInformation.ClubConstitutionDateAccepted,
                ClubCodeOfConductRecieved = currentClubInformation.ClubCodeOfConductRecieved,
                ClubCodeOfConductDateAccepted = currentClubInformation.ClubCodeOfConductDateAccepted,
                ClubDisciplinaryCodeRecieved = currentClubInformation.ClubDisciplinaryCodeRecieved,
                ClubDisciplinaryCodeDateAccepted = currentClubInformation.ClubDisciplinaryCodeDateAccepted,
                ComitteeMembers = comitteeMembers,
                PriorPeriods = priorPeriods
            };
        }

        public void updateClubInformation(ClubInformationDTO clubInformation, int profileId)
        {
            var currentProfile = context.UserProfiles.Include(a => a.userInformation).ThenInclude(a => a.clubInformation)
                .Where(a => a.Id == profileId).FirstOrDefault();
            var currentClubInformation = context.ClubInformation
                .Include(a => a.ComitteeMembers).Include(a => a.PriorPeriods)
                .Where(a => a.Id == currentProfile.userInformation.clubInformation.Id).FirstOrDefault();

            List<ClubInformationComitteeMembers> comitteeMembers = new List<ClubInformationComitteeMembers>();
            foreach (var entry in clubInformation.ComitteeMembers)
            {
                comitteeMembers.Add(_mapper.Map<ClubInformationComitteeMembers>(entry));
            }
            foreach (var deleteEntry in currentClubInformation.ComitteeMembers)
            {
                context.Remove(deleteEntry);
            }

            List<ClubInformationPriorPeriods> priorPeriods = new List<ClubInformationPriorPeriods>();
            foreach (var entry in clubInformation.PriorPeriods)
            {
                priorPeriods.Add(_mapper.Map<ClubInformationPriorPeriods>(entry));
            }
            foreach (var deleteEntry in currentClubInformation.PriorPeriods)
            {
                context.Remove(deleteEntry);
            }

            currentClubInformation.ClubName = clubInformation.ClubName;
            currentClubInformation.ClubPeriod = clubInformation.ClubPeriod;
            currentClubInformation.ClubConstitutionRecieved = clubInformation.ClubConstitutionRecieved;
            currentClubInformation.ClubConstitutionDateAccepted = clubInformation.ClubConstitutionDateAccepted;
            currentClubInformation.ClubCodeOfConductRecieved = clubInformation.ClubCodeOfConductRecieved;
            currentClubInformation.ClubCodeOfConductDateAccepted = currentClubInformation.ClubCodeOfConductDateAccepted;
            currentClubInformation.ClubDisciplinaryCodeRecieved = currentClubInformation.ClubDisciplinaryCodeRecieved;
            currentClubInformation.ClubDisciplinaryCodeDateAccepted = currentClubInformation.ClubDisciplinaryCodeDateAccepted;
            currentClubInformation.ComitteeMembers = comitteeMembers;
            currentClubInformation.PriorPeriods = priorPeriods;

            context.SaveChanges();
        }

        public MedicalInformationDTO getMedicalInformation(int profileId)
        {
            var currentProfile = context.UserProfiles.Include(a => a.userInformation).ThenInclude(a => a.medicalInformation)
                .Where(a => a.Id == profileId).FirstOrDefault();
            var currentUser = context.Users.Include(a => a.profiles).Where(a => a.profiles.Contains(currentProfile)).FirstOrDefault();
            if (currentProfile.userInformation == null)
            {
                currentProfile.userInformation = userService.getDefaultUserInformation();
                context.SaveChanges();

                // Refetching
                currentProfile = context.UserProfiles.Include(a => a.userInformation).ThenInclude(a => a.medicalInformation)
                .Where(a => a.Id == profileId).FirstOrDefault();
            }
            var currentMedicalInformation = context.MedicalInformation
                .Include(a => a.MedicalInformationAllergies).Include(a => a.MedicalInformationEmergencyContacts)
                .Include(a => a.MedicalInformationMedicalConditions).Include(a => a.MedicalInformationPharmacies)
                .Include(a => a.MedicalInformationPhysicians).Where(a => a.Id == currentProfile.userInformation.medicalInformation.Id).FirstOrDefault();

            List<MedicalInformationAllergiesDTO> allergies = new List<MedicalInformationAllergiesDTO>();
            foreach(var entry in currentMedicalInformation.MedicalInformationAllergies)
            {
                allergies.Add(_mapper.Map<MedicalInformationAllergiesDTO>(entry));
            }

            List<MedicalInformationEmergencyContactsDTO> emergencyContacts = new List<MedicalInformationEmergencyContactsDTO>();
            foreach (var entry in currentMedicalInformation.MedicalInformationEmergencyContacts)
            {
                emergencyContacts.Add(_mapper.Map<MedicalInformationEmergencyContactsDTO>(entry));
            }

            List<MedicalInformationMedicalConditionsDTO> medicalConditions = new List<MedicalInformationMedicalConditionsDTO>();
            foreach (var entry in currentMedicalInformation.MedicalInformationMedicalConditions)
            {
                medicalConditions.Add(_mapper.Map<MedicalInformationMedicalConditionsDTO>(entry));
            }

            List<MedicalInformationPharmaciesDTO> pharmacies = new List<MedicalInformationPharmaciesDTO>();
            foreach (var entry in currentMedicalInformation.MedicalInformationPharmacies)
            {
                pharmacies.Add(_mapper.Map<MedicalInformationPharmaciesDTO>(entry));
            }

            List<MedicalInformationPhysiciansDTO> physicians = new List<MedicalInformationPhysiciansDTO>();
            foreach (var entry in currentMedicalInformation.MedicalInformationPhysicians)
            {
                physicians.Add(_mapper.Map<MedicalInformationPhysiciansDTO>(entry));
            }

            return new MedicalInformationDTO()
            {
                Id = currentMedicalInformation.Id,
                MedicalAidName = currentMedicalInformation.MedicalAidName,
                MedicalAidPlan = currentMedicalInformation.MedicalAidPlan,
                MedicalAidNumber = currentMedicalInformation.MedicalAidNumber,
                MedicalAidContactNumber = currentMedicalInformation.MedicalAidContactNumber,
                MedicalInformationAllergies = allergies,
                MedicalInformationEmergencyContacts = emergencyContacts,
                MedicalInformationMedicalConditions = medicalConditions,
                MedicalInformationPharmacies = pharmacies,
                MedicalInformationPhysicians = physicians
            };
        }

        public void updateMedicalAid(MedicalInformationDTO medicalInformation, int profileId)
        {
            var currentProfile = context.UserProfiles.Include(a => a.userInformation).ThenInclude(a => a.medicalInformation)
                .Where(a => a.Id == profileId).FirstOrDefault();
            var currentMedicalInformation = context.MedicalInformation
                .Include(a => a.MedicalInformationAllergies).Include(a => a.MedicalInformationEmergencyContacts)
                .Include(a => a.MedicalInformationMedicalConditions).Include(a => a.MedicalInformationPharmacies)
                .Include(a => a.MedicalInformationPhysicians).Where(a => a.Id == currentProfile.userInformation.medicalInformation.Id).FirstOrDefault();

            List<MedicalInformationAllergies> allergies = new List<MedicalInformationAllergies>();
            foreach (var entry in medicalInformation.MedicalInformationAllergies)
            {
                allergies.Add(_mapper.Map<MedicalInformationAllergies>(entry));
            }
            foreach(var deleteEntry in currentMedicalInformation.MedicalInformationAllergies)
            {
                context.Remove(deleteEntry);
            }

            List<MedicalInformationEmergencyContacts> emergencyContacts = new List<MedicalInformationEmergencyContacts>();
            foreach (var entry in medicalInformation.MedicalInformationEmergencyContacts)
            {
                emergencyContacts.Add(_mapper.Map<MedicalInformationEmergencyContacts>(entry));
            }
            foreach (var deleteEntry in currentMedicalInformation.MedicalInformationEmergencyContacts)
            {
                context.Remove(deleteEntry);
            }

            List<MedicalInformationMedicalConditions> medicalConditions = new List<MedicalInformationMedicalConditions>();
            foreach (var entry in medicalInformation.MedicalInformationMedicalConditions)
            {
                medicalConditions.Add(_mapper.Map<MedicalInformationMedicalConditions>(entry));
            }
            foreach (var deleteEntry in currentMedicalInformation.MedicalInformationMedicalConditions)
            {
                context.Remove(deleteEntry);
            }

            List<MedicalInformationPharmacies> pharmacies = new List<MedicalInformationPharmacies>();
            foreach (var entry in medicalInformation.MedicalInformationPharmacies)
            {
                pharmacies.Add(_mapper.Map<MedicalInformationPharmacies>(entry));
            }
            foreach (var deleteEntry in currentMedicalInformation.MedicalInformationPharmacies)
            {
                context.Remove(deleteEntry);
            }

            List<MedicalInformationPhysicians> physicians = new List<MedicalInformationPhysicians>();
            foreach (var entry in medicalInformation.MedicalInformationPhysicians)
            {
                physicians.Add(_mapper.Map<MedicalInformationPhysicians>(entry));
            }
            foreach (var deleteEntry in currentMedicalInformation.MedicalInformationPhysicians)
            {
                context.Remove(deleteEntry);
            }

            currentMedicalInformation.MedicalAidContactNumber = medicalInformation.MedicalAidContactNumber;
            currentMedicalInformation.MedicalAidName = medicalInformation.MedicalAidName;
            currentMedicalInformation.MedicalAidNumber = medicalInformation.MedicalAidNumber;
            currentMedicalInformation.MedicalAidPlan = medicalInformation.MedicalAidPlan;
            currentMedicalInformation.MedicalInformationAllergies = allergies;
            currentMedicalInformation.MedicalInformationEmergencyContacts = emergencyContacts;
            currentMedicalInformation.MedicalInformationMedicalConditions = medicalConditions;
            currentMedicalInformation.MedicalInformationPharmacies = pharmacies;
            currentMedicalInformation.MedicalInformationPhysicians = physicians;

            context.SaveChanges();
        }

        public PersonalInformationDTO getPersonalInformation(int profileId)
        {
            var currentProfile = context.UserProfiles.Include(a => a.userInformation).ThenInclude(a => a.personalInformation)
                .Where(a => a.Id == profileId).FirstOrDefault();
            var currentUser = context.Users.Include(a => a.profiles).Where(a => a.profiles.Contains(currentProfile)).FirstOrDefault();
            if (currentProfile.userInformation == null)
            {
                currentProfile.userInformation = userService.getDefaultUserInformation();
                context.SaveChanges();

                // Refetching
                currentProfile = context.UserProfiles.Include(a => a.userInformation).ThenInclude(a => a.personalInformation)
                .Where(a => a.Id == profileId).FirstOrDefault();
            }

            var currentPersonalInformation = currentProfile.userInformation.personalInformation;
            return new PersonalInformationDTO() {
                name = currentUser.Name,
                surname = currentUser.Surname,
                nickName = currentPersonalInformation.nickName,
                idNumber = currentPersonalInformation.idNumber,
                dob = currentPersonalInformation.dob,
                nationality = currentPersonalInformation.nationality,
                ethnicGroup = currentPersonalInformation.ethnicGroup,
                gender = currentPersonalInformation.gender,
                passportNumber = currentPersonalInformation.passportNumber,
                passportExpirationDate = currentPersonalInformation.passportExpirationDate,
                homeAddress = currentPersonalInformation.homeAddress,
                postalAddress = currentPersonalInformation.postalAddress,
                phone = currentPersonalInformation.phone,
                cell = currentPersonalInformation.cell,
                skipperLicenseNumber = currentPersonalInformation.skipperLicenseNumber
            };
        }

        public void updatePersonalInformation(PersonalInformationDTO personalInformationDTO, int profileId)
        {
            var currentProfile = context.UserProfiles.Include(a => a.userInformation).ThenInclude(a => a.personalInformation)
                .Where(a => a.Id == profileId).FirstOrDefault();
            var currentUser = context.Users.Include(a => a.profiles).Where(a => a.profiles.Contains(currentProfile)).FirstOrDefault();
            var currentPersonalInformation = currentProfile.userInformation.personalInformation;

            currentUser.Name = personalInformationDTO.name;
            currentUser.Surname = personalInformationDTO.surname;
            currentPersonalInformation.nickName = personalInformationDTO.nickName;
            currentPersonalInformation.idNumber = personalInformationDTO.idNumber;
            currentPersonalInformation.dob = personalInformationDTO.dob;
            currentPersonalInformation.nationality = personalInformationDTO.nationality;
            currentPersonalInformation.ethnicGroup = personalInformationDTO.ethnicGroup;
            currentPersonalInformation.gender = personalInformationDTO.gender;
            currentPersonalInformation.passportNumber = personalInformationDTO.passportNumber;
            currentPersonalInformation.passportExpirationDate = personalInformationDTO.passportExpirationDate;
            currentPersonalInformation.homeAddress = personalInformationDTO.homeAddress;
            currentPersonalInformation.postalAddress = personalInformationDTO.postalAddress;
            currentPersonalInformation.phone = personalInformationDTO.phone;
            currentPersonalInformation.cell = personalInformationDTO.cell;
            currentPersonalInformation.skipperLicenseNumber = personalInformationDTO.skipperLicenseNumber;

            context.SaveChanges();
        }

        public async Task<bool> UploadId(IFormFile ufile, int documentId)
        {
            if (ufile != null && ufile.Length > 0)
            {
                var fileName = Path.GetFileName(documentId.ToString() + ".pdf");
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\idDocuments", fileName);
                if(File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await ufile.CopyToAsync(fileStream);
                }
                return true;
            }
            return false;
        }

        public async Task<bool> UploadPassport(IFormFile ufile, int documentId)
        {
            if (ufile != null && ufile.Length > 0)
            {
                var fileName = Path.GetFileName(documentId.ToString() + ".pdf");
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\passports", fileName);
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await ufile.CopyToAsync(fileStream);
                }
                return true;
            }
            return false;
        }

        public async Task<bool> UploadSkipperLicense(IFormFile ufile, int documentId)
        {
            if (ufile != null && ufile.Length > 0)
            {
                var fileName = Path.GetFileName(documentId.ToString() + ".pdf");
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\skippersLicenses", fileName);
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await ufile.CopyToAsync(fileStream);
                }
                return true;
            }
            return false;
        }

        public async Task<bool> UploadMedicalAid(IFormFile ufile, int documentId)
        {
            if (ufile != null && ufile.Length > 0)
            {
                var fileName = Path.GetFileName(documentId.ToString() + ".pdf");
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\medicalAid", fileName);
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await ufile.CopyToAsync(fileStream);
                }
                return true;
            }
            return false;
        }

        public async Task<bool> UploadCOF(IFormFile ufile, int documentId)
        {
            if (ufile != null && ufile.Length > 0)
            {
                var fileName = Path.GetFileName(documentId.ToString() + ".pdf");
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\cof", fileName);
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await ufile.CopyToAsync(fileStream);
                }
                return true;
            }
            return false;
        }

        public async Task<bool> UploadProfilePicture(IFormFile ufile, int documentId)
        {
            if (ufile != null && ufile.Length > 0)
            {
                var fileName = Path.GetFileName(documentId.ToString() + ".jpg");
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\profilePicture", fileName);
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await ufile.CopyToAsync(fileStream);
                }
                return true;
            }
            return false;
        }

        public List<RoleManagementUsersDTO> getRoleManagementUsers(int profileId)
        {
            var currentProfile = context.UserProfiles.Include(a => a.club).Include(a => a.role)
                .Where(a => a.Id == profileId).FirstOrDefault();

            var lowerUserProfiles = communicationService.getProfilesInLowerRoles(currentProfile);
            var sameRoleProfiles = communicationService.getProfilesInSameRole(currentProfile);
            foreach(var entry in sameRoleProfiles)
            {
                if(lowerUserProfiles.Where(a => a.Id == entry.Id).FirstOrDefault() == null && entry.Id != profileId)
                {
                    lowerUserProfiles.Add(entry);
                }
            }

            List<RoleManagementUsersDTO> result = new List<RoleManagementUsersDTO>();
            foreach(var entry in lowerUserProfiles)
            {
                result.Add(new RoleManagementUsersDTO()
                {
                    Id = entry.Id,
                    FullName = getProfileFullName(entry),
                    Username = context.Users.Include(a => a.profiles).Where(a => a.profiles.Contains(entry)).FirstOrDefault().UserName,
                    Facet = getProfileFacetName(entry),
                    Role = entry.role.Description + " " + entry.role.FullName
                });
            }
            return result;
        }

        private string getProfileFullName(UserProfile userProfile)
        {
            var currentUser = context.Users.Include(a => a.profiles).Where(a => a.profiles.Contains(userProfile)).FirstOrDefault();
            return currentUser.Name + " " + currentUser.Surname;
        }

        private string getProfileFacetName(UserProfile userProfile)
        {
            var userFacet = context.UserProfiles.Include(a => a.club).ThenInclude(a => a.Facet)
                    .Where(a => a.Id == userProfile.Id).FirstOrDefault().club.Facet;
            return userFacet.Name + " " + userFacet.Federation;
        }

        public bool updateUserProfileRole(int profileId, string role)
        {
            var currentProfile = context.UserProfiles.Include(a => a.club).Include(a => a.role)
                .Where(a => a.Id == profileId).FirstOrDefault();

            var newRole = context.Role.Where(a => a.Description == role).FirstOrDefault();
            if(newRole == null)
            {
                return false;
            }
            currentProfile.role = newRole;
            context.SaveChanges();
            return true;
        }
    }
}
