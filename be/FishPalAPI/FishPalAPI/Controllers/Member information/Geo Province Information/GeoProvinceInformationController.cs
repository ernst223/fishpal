
using FishPalAPI.Models.UserInformation.Geo_Province_Information;
using FishPalAPI.Services.Member_Information.Geo_Province_Information;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FishPalAPI.Controllers.Member_information.Geo_Province_Information
{ 
        [Route("api/geoProvince")]
        public class ClothesController : Controller
        {
            private GeoProvinceService geoProvinceService;

            public ClothesController()
            {
                geoProvinceService = new GeoProvinceService();
            }

            [HttpGet("getGeoProvinceInformation/{profileId}")]
            public async Task<IActionResult> getGeoProvinceInformation(int profileId)
            {
                return Ok(geoProvinceService.getGeoProvinceInformation(profileId));
            }

            [HttpPut("updateGeoProvinceInformation/{profileId}")]
            public async Task<IActionResult> updateGeoProvinceInformation([FromBody] GeoProvinceInformationDTO medicalInformationDTO, int profileId)
            {
                geoProvinceService.updateGeoProvinceInformation(medicalInformationDTO, profileId);
                return Ok();
            }


        }  
}
