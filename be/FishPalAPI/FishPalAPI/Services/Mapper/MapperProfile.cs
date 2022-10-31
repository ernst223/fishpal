using AutoMapper;
using FishPalAPI.Data;
using FishPalAPI.Models.UserInformation.MedicalInformation;
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

            CreateMap<MedicalInformationAllergiesDTO, MedicalInformationAllergies>();
            CreateMap<MedicalInformationEmergencyContactsDTO, MedicalInformationEmergencyContacts>();
            CreateMap<MedicalInformationMedicalConditionsDTO, MedicalInformationMedicalConditions>();
            CreateMap<MedicalInformationPharmaciesDTO, MedicalInformationPharmacies>();
            CreateMap<MedicalInformationPhysiciansDTO, MedicalInformationPhysicians>();
        }
    }
}
