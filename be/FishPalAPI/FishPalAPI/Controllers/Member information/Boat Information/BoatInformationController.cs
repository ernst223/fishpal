using FishPalAPI.Models.UserInformation.Boat_Information;
using FishPalAPI.Services.Member_Information.Boat_Information;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FishPalAPI.Controllers.Member_information.Boat_Information
{
    [Route("api/boatInformation")]
    public class BoatInformationController : Controller
    {
        private BoatInformationService boatService;

        public BoatInformationController()
        {
            boatService = new BoatInformationService();
        }

        [HttpGet("getBoatInformation/{profileId}")]
        public async Task<IActionResult> getBoatInformation(int profileId)
        {
            return Ok(boatService.getBoatInformation(profileId));
        }

        [HttpPut("updateBoatInformation/{profileId}")]
        public async Task<IActionResult> updateBoatInformation([FromBody] BoatInformationDTO boatInformationDTO, int profileId)
        {
            boatService.updateBoatInformation(boatInformationDTO, profileId);
            return Ok();
        }


    }
}
