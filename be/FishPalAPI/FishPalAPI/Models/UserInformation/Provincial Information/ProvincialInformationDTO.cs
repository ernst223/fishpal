using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FishPalAPI.Models.UserInformation.Provincial_Information
{
    public class ProvincialInformationDTO
    {
        public int Id { get; set; }
        public string ProvinceName { get; set; }
        public string ProvincePeriod { get; set; }
        public string ProvinceConstitutionRecieved { get; set; }
        public DateTime ProvinceConstitutionDate { get; set; }
        public string ProvinceCodeOfCoductRecieved { get; set; }
        public DateTime ProvinceCodeOfCoductDate { get; set; }
        public string ProvinceDressCodeRecieved { get; set; }
        public DateTime ProvinceDressCodeDate { get; set; }
        public string ProvinceDisciplinaryCodeRecieved { get; set; }
        public DateTime ProvinceDisciplinaryCodeDate { get; set; }
        public List<ProvincialInformationPriorPeriodsDTO> PriorPeriods { get; set; }
        public List<ProvincialInformationComteeMembersDTO> ComitteeMembers { get; set; }
    }
}
