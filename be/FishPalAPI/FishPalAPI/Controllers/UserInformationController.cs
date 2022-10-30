using FishPalAPI.Models;
using FishPalAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FishPalAPI.Controllers
{
    [Route("api/userinformation")]
    public class UserInformationController: Controller
    {
        private UserInformationService userInformationService;

        public UserInformationController()
        {
            userInformationService = new UserInformationService();
        }

        [HttpGet("personalinformation/{profileId}")]
        public async Task<IActionResult> getPersonalInformation(int profileId)
        {
            return Ok(userInformationService.getPersonalInformation(profileId));
        }

        [HttpPut("personalinformation/{profileId}")]
        public async Task<IActionResult> updatePersonalInformation([FromBody] PersonalInformationDTO personalInformationDTO,int profileId)
        {
            userInformationService.updatePersonalInformation(personalInformationDTO, profileId);
            return Ok();
        }

    }
}
