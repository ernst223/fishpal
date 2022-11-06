using AutoMapper;
using FishPalAPI.Data;
using FishPalAPI.Data.Member_Information.Provincial_Information;
using FishPalAPI.Models.UserInformation.ClubInformation;
using FishPalAPI.Models.UserInformation.MedicalInformation;
using FishPalAPI.Models.UserInformation.Provincial_Information;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FishPalAPI.Services.Mapper
{
    public class MapperProfile: Profile
    {
        public MapperProfile()
        {
            CreateMap<MedicalInformationAllergies, MedicalInformationAllergiesDTO>();
            CreateMap<MedicalInformationEmergencyContacts, MedicalInformationEmergencyContactsDTO>();
            CreateMap<MedicalInformationMedicalConditions, MedicalInformationMedicalConditionsDTO>();
            CreateMap<MedicalInformationPharmacies, MedicalInformationPharmaciesDTO>();
            CreateMap<MedicalInformationPhysicians, MedicalInformationPhysiciansDTO>();

            CreateMap<MedicalInformationAllergiesDTO, MedicalInformationAllergies>().ForMember(a => a.Id, a => a.Ignore());
            CreateMap<MedicalInformationEmergencyContactsDTO, MedicalInformationEmergencyContacts>().ForMember(a => a.Id, a => a.Ignore());
            CreateMap<MedicalInformationMedicalConditionsDTO, MedicalInformationMedicalConditions>().ForMember(a => a.Id, a => a.Ignore());
            CreateMap<MedicalInformationPharmaciesDTO, MedicalInformationPharmacies>().ForMember(a => a.Id, a => a.Ignore());
            CreateMap<MedicalInformationPhysiciansDTO, MedicalInformationPhysicians>().ForMember(a => a.Id, a => a.Ignore());

            CreateMap<ClubInformation, ClubInformationDTO>();
            CreateMap<ClubInformationComitteeMembers, ClubInformationComitteeMembersDTO>();
            CreateMap<ClubInformationPriorPeriods, ClubInformationPriorPeriodsDTO>();

            CreateMap<ClubInformationDTO, ClubInformation>().ForMember(a => a.Id, a => a.Ignore());
            CreateMap<ClubInformationComitteeMembersDTO, ClubInformationComitteeMembers>().ForMember(a => a.Id, a => a.Ignore());
            CreateMap<ClubInformationPriorPeriodsDTO, ClubInformationPriorPeriods>().ForMember(a => a.Id, a => a.Ignore());

            CreateMap<ProvincialInformation, ProvincialInformationDTO>();
            CreateMap<ProvincialInformationComtteeMembers, ProvincialInformationComteeMembersDTO>();
            CreateMap<ProvincialInformationPriorPeriods, ProvincialInformationPriorPeriodsDTO>();

            CreateMap<ProvincialInformationDTO, ProvincialInformation>().ForMember(a => a.Id, a => a.Ignore());
            CreateMap<ProvincialInformationComteeMembersDTO, ProvincialInformationComtteeMembers>().ForMember(a => a.Id, a => a.Ignore());
            CreateMap<ProvincialInformationPriorPeriodsDTO, ProvincialInformationPriorPeriods>().ForMember(a => a.Id, a => a.Ignore());
        }
    }
}
