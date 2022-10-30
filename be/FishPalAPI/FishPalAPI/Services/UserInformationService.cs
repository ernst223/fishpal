using FishPalAPI.Data;
using FishPalAPI.Models;
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

        public UserInformationService()
        {
            context = new ApplicationDbContext();
            userService = new UserService();
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
