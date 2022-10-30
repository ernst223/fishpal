using System.Collections.Generic;

namespace FishPalAPI.Models.MobileAppModels
{
    public class mobileUserInfoDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public List<FacetDTO> facets { get; set; }
        public List<ClubDTO> clubs { get; set; }
    }
}
