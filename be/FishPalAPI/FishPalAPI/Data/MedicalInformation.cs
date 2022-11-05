using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FishPalAPI.Data
{
    public class MedicalInformation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string MedicalAidName { get; set; }
        public string MedicalAidPlan { get; set; }
        public string MedicalAidNumber { get; set; }
        public string MedicalAidContactNumber { get; set; }
        public List<MedicalInformationPhysicians> MedicalInformationPhysicians { get; set; }
        public List<MedicalInformationPharmacies> MedicalInformationPharmacies { get; set; }
        public List<MedicalInformationEmergencyContacts> MedicalInformationEmergencyContacts{ get; set; }
        public List<MedicalInformationMedicalConditions> MedicalInformationMedicalConditions { get; set; }
        public List<MedicalInformationAllergies> MedicalInformationAllergies { get; set; }
    }
}
