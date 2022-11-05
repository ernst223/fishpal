using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FishPalAPI.Models.UserInformation.MedicalInformation
{
    public class MedicalInformationAllergiesDTO
    {
        public int Id { get; set; }
        public string AllergyName { get; set; }
        public string AllergyReaction { get; set; }
        public string AllergyMedication { get; set; }
    }
}
