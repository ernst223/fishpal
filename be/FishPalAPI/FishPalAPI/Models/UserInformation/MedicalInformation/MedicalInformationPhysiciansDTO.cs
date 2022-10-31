using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FishPalAPI.Models.UserInformation.MedicalInformation
{
    public class MedicalInformationPhysiciansDTO
    {
        public int Id { get; set; }
        public string PhysicianName { get; set; }
        public string PhysicianContactNumber { get; set; }
    }
}
