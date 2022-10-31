using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FishPalAPI.Models.UserInformation.MedicalInformation
{
    public class MedicalInformationEmergencyContactsDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Relationship { get; set; }
        public string ContactNumberCell { get; set; }
        public string ContactNumberHome { get; set; }
    }
}
