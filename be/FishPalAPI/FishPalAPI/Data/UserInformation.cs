using FishPalAPI.Data.Member_Information.Boat_Information;
using FishPalAPI.Data.Member_Information.Geo_Province_Information;
using FishPalAPI.Data.Member_Information.Provincial_Information;
using FishPalAPI.Data.Member_Information.Training;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FishPalAPI.Data
{
    public class UserInformation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public PersonalInformation personalInformation { get; set; }
        public MedicalInformation medicalInformation { get; set; }
        public ClubInformation clubInformation { get; set; }
        public GeoProvinceInformation geoProvinceInformation { get; set; }
        public BoatInformation boatInformation { get; set; }
        public Training training { get; set; }
        public ProvincialInformation provincialInformation { get; set; }
    }
}
