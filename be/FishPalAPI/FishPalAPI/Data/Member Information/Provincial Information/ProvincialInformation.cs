using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FishPalAPI.Data.Member_Information.Provincial_Information
{
    public class ProvincialInformation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string ProvinceName{ get; set; }
        public string ProvincePeriod { get; set; }
        public string ProvinceConstitutionRecieved { get; set; }
        public DateTime ProvinceConstitutionDate { get; set; }
        public string ProvinceCodeOfCoductRecieved { get; set; }
        public DateTime ProvinceCodeOfCoductDate { get; set; }
        public string ProvinceDressCodeRecieved { get; set; }
        public DateTime ProvinceDressCodeDate { get; set; }
        public string ProvinceDisciplinaryCodeRecieved { get; set; }
        public DateTime ProvinceDisciplinaryCodeDate { get; set; }
        public List<ProvincialInformationPriorPeriods> PriorPeriods { get; set; }
        public List<ProvincialInformationComtteeMembers> ComitteeMembers { get; set; }
    }
}
