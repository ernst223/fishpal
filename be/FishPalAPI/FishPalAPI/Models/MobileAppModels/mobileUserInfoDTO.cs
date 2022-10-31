using System;
using System.Collections.Generic;

namespace FishPalAPI.Models.MobileAppModels
{
    public class mobileUserInfoDTO
    {
        public int ProfileId { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string FacetName { get; set; }
        public string TypeName { get; set; }
        public int FacetId { get; set; }
        public DateTime ProfileCreationDate { get; set; }
        public string ClubName { get; set; }
        public int ClubId { get; set; }
        public string Province { get; set; }
        public int ProvinceId { get; set; }

        public string FacetLogoBase64 { get; set; }
    }
}
