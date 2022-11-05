using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FishPalAPI.Data
{
    public class PersonalInformation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string nickName { get; set; }
        public string idNumber { get; set; }
        public DateTime dob { get; set; }
        public string nationality { get; set; }
        public string ethnicGroup { get; set; }
        public string gender { get; set; }
        public string passportNumber { get; set; }
        public DateTime passportExpirationDate { get; set; }
        public string homeAddress { get; set; }
        public string postalAddress { get; set; }
        public string phone { get; set; }
        public string cell { get; set; }
        public string skipperLicenseNumber { get; set; }
    }
}
