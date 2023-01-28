using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FishPalAPI.Data.Export
{
    public class exportUserInformationDTO
    {
        public int Id { get; set; }
        public string nickName { get; set; }
        public string idNumber { get; set; }
        public string dob { get; set; }
        public string nationality { get; set; }
        public string ethnicGroup { get; set; }
        public string gender { get; set; }
        public string passportNumber { get; set; }
        public string passportExpirationDate { get; set; }
        public string homeAddress { get; set; }
        public string postalAddress { get; set; }
        public string phone { get; set; }
        public string cell { get; set; }
        public string skipperLicenseNumber { get; set; }
        public string memberNumber { get; set; }
        // Medical information
        public string MedicalAidName { get; set; }
        public string MedicalAidPlan { get; set; }
        public string MedicalAidNumber { get; set; }
        public string MedicalAidContactNumber { get; set; }
        // Club information
        public string ClubName { get; set; }
        public string Province { get; set; }

        // Boat information
        public string BoatOwner { get; set; }
        public string BoatNumber { get; set; }
        public string CofNumber { get; set; }
        // trainging
        public string ManagerYearCompleted { get; set; }
        public string ManagerPointsReceived { get; set; }
        public string CoachLvl1YearCompleted { get; set; }
        public string CoachLvl1PointsReceived { get; set; }
        public string CoachLvl2YearCompleted { get; set; }
        public string CoachLvl2PointsReceived { get; set; }
        public string CaptainYearCompleted { get; set; }
        public string CaptainPointsReceived { get; set; }
        public string AdminYearCompleted { get; set; }
        public string AdminPointsReceived { get; set; }
    }
}
