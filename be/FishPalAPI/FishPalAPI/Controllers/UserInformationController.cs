using AutoMapper;
using FishPalAPI.Models;
using FishPalAPI.Models.UserInformation.ClubInformation;
using FishPalAPI.Models.UserInformation.MedicalInformation;
using FishPalAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FishPalAPI.Controllers
{
    [Route("api/userinformation")]
    public class UserInformationController: Controller
    {
        private UserInformationService userInformationService;
        private readonly IMapper _mapper;

        public UserInformationController(IMapper mapper)
        {
            _mapper = mapper;
            userInformationService = new UserInformationService(_mapper);
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

        [HttpGet("medicalinformation/{profileId}")]
        public async Task<IActionResult> getMedicalInformation(int profileId)
        {
            return Ok(userInformationService.getMedicalInformation(profileId));
        }

        [HttpPut("medicalinformation/{profileId}")]
        public async Task<IActionResult> updateMedicalInformation([FromBody] MedicalInformationDTO medicalInformationDTO, int profileId)
        {
            userInformationService.updateMedicalAid(medicalInformationDTO, profileId);
            return Ok();
        }

        [HttpGet("clubinformation/{profileId}")]
        public async Task<IActionResult> getClubInformation(int profileId)
        {
            return Ok(userInformationService.getClubInformation(profileId));
        }

        [HttpPut("clubinformation/{profileId}")]
        public async Task<IActionResult> updateClubInformation([FromBody] ClubInformationDTO clubInformationDTO, int profileId)
        {
            userInformationService.updateClubInformation(clubInformationDTO, profileId);
            return Ok();
        }

    }
}
