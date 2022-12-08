using AutoMapper;
using FishPalAPI.Models;
using FishPalAPI.Models.UserInformation.ClubInformation;
using FishPalAPI.Models.UserInformation.MedicalInformation;
using FishPalAPI.Models.UserInformation.Other;
using FishPalAPI.Models.UserInformation.Provincial_Information;
using FishPalAPI.Services;
using FishPalAPI.Services.Member_Information.Provincial_Information;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FishPalAPI.Controllers
{
    [Route("api/userinformation")]
    public class UserInformationController: Controller
    {
        private UserInformationService userInformationService;
        private ProvincialInformationService provincialInformationService;
        private readonly IMapper _mapper;

        public UserInformationController(IMapper mapper)
        {
            _mapper = mapper;
            userInformationService = new UserInformationService(_mapper);
            provincialInformationService = new ProvincialInformationService(_mapper);
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

        [HttpGet("provincialinformation/{profileId}")]
        public async Task<IActionResult> getProvincialInformation(int profileId)
        {
            return Ok(provincialInformationService.getProvincialInformation(profileId));
        }

        [HttpPut("provincialinformation/{profileId}")]
        public async Task<IActionResult> updateProvincialInformation([FromBody] ProvincialInformationDTO provincialInformationDTO, int profileId)
        {
            provincialInformationService.updateProvincialInformation(provincialInformationDTO, profileId);
            return Ok();
        }

        [HttpGet("otheranglingachievements/{profileId}")]
        public async Task<IActionResult> getOtherAnglingAchievements(int profileId)
        {
            return Ok(userInformationService.getOtherAnglingAchievements(profileId));
        }

        [HttpPut("otheranglingachievements/{profileId}")]
        public async Task<IActionResult> updateOtherAnglingAchievements([FromBody] List<OtherAnglingAchievementsDTO> otherAnglingAchievementsDTO, int profileId)
        {
            userInformationService.updateOtherAnglingAchievements(otherAnglingAchievementsDTO, profileId);
            return Ok();
        }

        [HttpGet("anglishadministrationaistory/{profileId}")]
        public async Task<IActionResult> getAnglishAdministrationHistory(int profileId)
        {
            return Ok(userInformationService.getAnglishAdministrationHistory(profileId));
        }

        [HttpPut("anglishadministrationaistory/{profileId}")]
        public async Task<IActionResult> updateAnglishAdministrationHistory([FromBody] List<AnglishAdministrationHistoryDTO> anglishAdministrationHistoryDTO, int profileId)
        {
            userInformationService.updateAnglishAdministrationHistories(anglishAdministrationHistoryDTO, profileId);
            return Ok();
        }

        [HttpPost("upload/id/{profileId}")]
        public async Task<IActionResult> uploadIdDocument(IFormFile file, int profileId)
        {
            return Ok(await userInformationService.UploadId(file, profileId));
        }

        [HttpPost("upload/passport/{profileId}")]
        public async Task<IActionResult> uploadPassport(IFormFile file, int profileId)
        {
            return Ok(await userInformationService.UploadPassport(file, profileId));
        }

        [HttpPost("upload/skippers/{profileId}")]
        public async Task<IActionResult> uploadSkippers(IFormFile file, int profileId)
        {
            return Ok(await userInformationService.UploadSkipperLicense(file, profileId));
        }

        [HttpPost("upload/medicalaid/{profileId}")]
        public async Task<IActionResult> uploadMedicalAid(IFormFile file, int profileId)
        {
            return Ok(await userInformationService.UploadMedicalAid(file, profileId));
        }

        [HttpPost("upload/cof/{profileId}")]
        public async Task<IActionResult> uploadCOF(IFormFile file, int profileId)
        {
            return Ok(await userInformationService.UploadCOF(file, profileId));
        }

        [HttpPost("upload/profilePicture/{profileId}")]
        public async Task<IActionResult> uploadProfilePicture(IFormFile file, string profileId)
        {
            return Ok(await userInformationService.UploadProfilePicture(file, profileId));
        }

        [HttpGet("rolemanagement/{profileId}")]
        public async Task<IActionResult> getUsersForRoleManagement(int profileId)
        {
            return Ok(userInformationService.getRoleManagementUsers(profileId));
        }

        [HttpGet("updateuserrole/{profileId}/{role}")]
        public async Task<IActionResult> updateUserRole(int profileId, string role)
        {
            return Ok(userInformationService.updateUserProfileRole(profileId, role));
        }
    }
}
