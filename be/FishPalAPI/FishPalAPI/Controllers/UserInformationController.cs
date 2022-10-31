using AutoMapper;
using FishPalAPI.Models;
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

    }
}
