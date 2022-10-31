using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FishPalAPI.Models.UserInformation.MedicalInformation
{
    public class MedicalInformationDTO
    {
        public int Id { get; set; }
        public string MedicalAidName { get; set; }
        public string MedicalAidPlan { get; set; }
        public string MedicalAidNumber { get; set; }
        public string MedicalAidContactNumber { get; set; }
        public List<MedicalInformationPhysiciansDTO> MedicalInformationPhysicians { get; set; }
        public List<MedicalInformationPharmaciesDTO> MedicalInformationPharmacies { get; set; }
        public List<MedicalInformationEmergencyContactsDTO> MedicalInformationEmergencyContacts { get; set; }
        public List<MedicalInformationMedicalConditionsDTO> MedicalInformationMedicalConditions { get; set; }
        public List<MedicalInformationAllergiesDTO> MedicalInformationAllergies { get; set; }
    }
}
