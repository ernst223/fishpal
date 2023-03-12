using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FishPalAPI.Models
{
    public class PersonalInformationDTO
    {
        public string name { get; set; }
        public string nickName { get; set; }
        public string surname { get; set; }
        public string idNumber { get; set; }
        public DateTime dob { get; set; }
        public string nationality { get; set; }
        public string ethnicGroup { get; set; }
        public string gender { get; set; }
        public string passportNumber { get; set; }
        public DateTime passportExpirationDate { get; set; }
        public string homeAddress { get; set; }
        public string postalAddress { get; set; }
        public string postalCode { get; set; }
        public string phone { get; set; }
        public string cell { get; set; }
        public string skipperLicenseNumber { get; set; }
    }
}
