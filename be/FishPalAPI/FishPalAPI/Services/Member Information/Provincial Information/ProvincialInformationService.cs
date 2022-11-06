using AutoMapper;
using FishPalAPI.Data;
using FishPalAPI.Data.Member_Information.Provincial_Information;
using FishPalAPI.Models.UserInformation.Provincial_Information;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FishPalAPI.Services.Member_Information.Provincial_Information
{
    public class ProvincialInformationService
    {
        private ApplicationDbContext context;
        private UserService userService;
        private readonly IMapper _mapper;
        public ProvincialInformationService(IMapper mapper)
        {
            _mapper = mapper;
            context = new ApplicationDbContext();
            userService = new UserService();
        }

        public ProvincialInformationDTO getProvincialInformation(int profileId)
        {
            var currentProfile = context.UserProfiles.Include(a => a.userInformation).ThenInclude(a => a.provincialInformation)
                .Where(a => a.Id == profileId).FirstOrDefault();
            var currentUser = context.Users.Include(a => a.profiles).Where(a => a.profiles.Contains(currentProfile)).FirstOrDefault();
            if (currentProfile.userInformation == null)
            {
                currentProfile.userInformation = userService.getDefaultUserInformation();
                context.SaveChanges();

                // Refetching
                currentProfile = context.UserProfiles.Include(a => a.userInformation).ThenInclude(a => a.provincialInformation)
                .Where(a => a.Id == profileId).FirstOrDefault();
            }

            var currentProvincialInformation = context.ProvincialInformation
                .Include(a => a.PriorPeriods).Include(a => a.ComitteeMembers)
                .Where(a => a.Id == currentProfile.userInformation.provincialInformation.Id).FirstOrDefault();

            List<ProvincialInformationComteeMembersDTO> comitteeMembers = new List<ProvincialInformationComteeMembersDTO>();
            foreach (var entry in currentProvincialInformation.ComitteeMembers)
            {
                comitteeMembers.Add(_mapper.Map<ProvincialInformationComteeMembersDTO>(entry));
            }

            List<ProvincialInformationPriorPeriodsDTO> priorPeriods = new List<ProvincialInformationPriorPeriodsDTO>();
            foreach (var entry in currentProvincialInformation.PriorPeriods)
            {
                priorPeriods.Add(_mapper.Map<ProvincialInformationPriorPeriodsDTO>(entry));
            }

            return new ProvincialInformationDTO()
            {
                Id = currentProvincialInformation.Id,
                ProvinceName = currentProvincialInformation.ProvinceName,
                ProvincePeriod = currentProvincialInformation.ProvincePeriod,
                ProvinceCodeOfCoductDate = currentProvincialInformation.ProvinceCodeOfCoductDate,
                ProvinceCodeOfCoductRecieved = currentProvincialInformation.ProvinceCodeOfCoductRecieved,
                ProvinceConstitutionDate = currentProvincialInformation.ProvinceConstitutionDate,
                ProvinceConstitutionRecieved = currentProvincialInformation.ProvinceConstitutionRecieved,
                ProvinceDisciplinaryCodeDate = currentProvincialInformation.ProvinceDisciplinaryCodeDate,
                ProvinceDisciplinaryCodeRecieved = currentProvincialInformation.ProvinceDisciplinaryCodeRecieved,
                ProvinceDressCodeDate = currentProvincialInformation.ProvinceDressCodeDate,
                ProvinceDressCodeRecieved = currentProvincialInformation.ProvinceDressCodeRecieved,
                ComitteeMembers = comitteeMembers,
                PriorPeriods = priorPeriods
            };
        }

        public void updateProvincialInformation(ProvincialInformationDTO provincialInformation, int profileId)
        {
            var currentProfile = context.UserProfiles.Include(a => a.userInformation).ThenInclude(a => a.provincialInformation)
                .Where(a => a.Id == profileId).FirstOrDefault();
            var currentProvincialInformation = context.ProvincialInformation
                .Include(a => a.ComitteeMembers).Include(a => a.PriorPeriods)
                .Where(a => a.Id == currentProfile.userInformation.provincialInformation.Id).FirstOrDefault();

            List<ProvincialInformationComtteeMembers> comitteeMembers = new List<ProvincialInformationComtteeMembers>();
            foreach (var entry in provincialInformation.ComitteeMembers)
            {
                comitteeMembers.Add(_mapper.Map<ProvincialInformationComtteeMembers>(entry));
            }
            foreach (var deleteEntry in currentProvincialInformation.ComitteeMembers)
            {
                context.Remove(deleteEntry);
            }

            List<ProvincialInformationPriorPeriods> priorPeriods = new List<ProvincialInformationPriorPeriods>();
            foreach (var entry in provincialInformation.PriorPeriods)
            {
                priorPeriods.Add(_mapper.Map<ProvincialInformationPriorPeriods>(entry));
            }
            foreach (var deleteEntry in currentProvincialInformation.PriorPeriods)
            {
                context.Remove(deleteEntry);
            }

            currentProvincialInformation.ProvinceName = provincialInformation.ProvinceName;
            currentProvincialInformation.ProvincePeriod = provincialInformation.ProvincePeriod;
            currentProvincialInformation.ProvinceCodeOfCoductDate = provincialInformation.ProvinceCodeOfCoductDate;
            currentProvincialInformation.ProvinceCodeOfCoductRecieved = provincialInformation.ProvinceCodeOfCoductRecieved;
            currentProvincialInformation.ProvinceConstitutionDate = provincialInformation.ProvinceConstitutionDate;
            currentProvincialInformation.ProvinceConstitutionRecieved = provincialInformation.ProvinceConstitutionRecieved;
            currentProvincialInformation.ProvinceDisciplinaryCodeDate = provincialInformation.ProvinceDisciplinaryCodeDate;
            currentProvincialInformation.ProvinceDisciplinaryCodeRecieved = provincialInformation.ProvinceDisciplinaryCodeRecieved;
            currentProvincialInformation.ProvinceDressCodeDate = provincialInformation.ProvinceDressCodeDate;
            currentProvincialInformation.ProvinceDressCodeRecieved = provincialInformation.ProvinceDressCodeRecieved;
            currentProvincialInformation.ComitteeMembers = comitteeMembers;
            currentProvincialInformation.PriorPeriods = priorPeriods;

            context.SaveChanges();
        }
    }
}
