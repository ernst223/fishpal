using AutoMapper;
using FishPalAPI.Data;
using FishPalAPI.Models;
using FishPalAPI.Models.UserInformation.MedicalInformation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FishPalAPI.Services
{
    public class UserInformationService
    {
        private ApplicationDbContext context;
        private UserService userService;
        private readonly IMapper _mapper;

        public UserInformationService(IMapper mapper)
        {
            _mapper = mapper;
            context = new ApplicationDbContext();
            userService = new UserService();
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

            List<MedicalInformationEmergencyContacts> emergencyContacts = new List<MedicalInformationEmergencyContacts>();
            foreach (var entry in medicalInformation.MedicalInformationEmergencyContacts)
            {
                emergencyContacts.Add(_mapper.Map<MedicalInformationEmergencyContacts>(entry));
            }

            List<MedicalInformationMedicalConditions> medicalConditions = new List<MedicalInformationMedicalConditions>();
            foreach (var entry in medicalInformation.MedicalInformationMedicalConditions)
            {
                medicalConditions.Add(_mapper.Map<MedicalInformationMedicalConditions>(entry));
            }

            List<MedicalInformationPharmacies> pharmacies = new List<MedicalInformationPharmacies>();
            foreach (var entry in medicalInformation.MedicalInformationPharmacies)
            {
                pharmacies.Add(_mapper.Map<MedicalInformationPharmacies>(entry));
            }

            List<MedicalInformationPhysicians> physicians = new List<MedicalInformationPhysicians>();
            foreach (var entry in medicalInformation.MedicalInformationPhysicians)
            {
                physicians.Add(_mapper.Map<MedicalInformationPhysicians>(entry));
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
    }
}
