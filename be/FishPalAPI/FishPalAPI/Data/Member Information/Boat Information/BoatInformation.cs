using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FishPalAPI.Data.Member_Information.Boat_Information
{
    public class BoatInformation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string BoatOwner { get; set; }
        public string BoatNumber { get; set; }
        public string HullType { get; set; }
        public string HullColour { get; set; }
        public string MotorMake { get; set; }
        public string HorsePower { get; set; }

        public string TowVehicleRegistrationNumber { get; set; }
        public string TrailerRegistrationNumber { get; set; }
        public string CofNumber { get; set; }
        public DateTime CofExpiryDate { get; set; }
    }
}
