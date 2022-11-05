using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FishPalAPI.Data
{
    public class ClubInformation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string ClubName{ get; set; }
        public string ClubPeriod { get; set; }
        public string ClubConstitutionRecieved { get; set; }
        public DateTime ClubConstitutionDateAccepted { get; set; }
        public string ClubCodeOfConductRecieved { get; set; }
        public DateTime ClubCodeOfConductDateAccepted { get; set; }
        public string ClubDisciplinaryCodeRecieved { get; set; }
        public DateTime ClubDisciplinaryCodeDateAccepted { get; set; }
        public List<ClubInformationComitteeMembers> ComitteeMembers { get; set; }
        public List<ClubInformationPriorPeriods> PriorPeriods { get; set; }
    }
}
