using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FishPalAPI.Models.UserInformation.ClubInformation
{
    public class ClubInformationDTO
    {
        public int Id { get; set; }
        public string ClubName { get; set; }
        public string ClubPeriod { get; set; }
        public string ClubConstitutionRecieved { get; set; }
        public DateTime ClubConstitutionDateAccepted { get; set; }
        public string ClubCodeOfConductRecieved { get; set; }
        public DateTime ClubCodeOfConductDateAccepted { get; set; }
        public string ClubDisciplinaryCodeRecieved { get; set; }
        public DateTime ClubDisciplinaryCodeDateAccepted { get; set; }
        public List<ClubInformationComitteeMembersDTO> ComitteeMembers { get; set; }
        public List<ClubInformationPriorPeriodsDTO> PriorPeriods { get; set; }
    }
}
