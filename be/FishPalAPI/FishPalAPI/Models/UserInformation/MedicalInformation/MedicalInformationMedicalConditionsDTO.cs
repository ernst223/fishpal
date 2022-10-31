using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FishPalAPI.Models.UserInformation.MedicalInformation
{
    public class MedicalInformationMedicalConditionsDTO
    {
        public int Id { get; set; }
        public string ConditionName { get; set; }
        public string MedicationName { get; set; }
        public string MedicationDosage { get; set; }
        public string MedicationFrequency { get; set; }
    }
}
