using FishPalAPI.Models.UserInformation.Training;
using FishPalAPI.Services.Member_Information.Training;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FishPalAPI.Controllers.Member_information.Training
{
    [Route("api/training")]
    public class TrainingController : Controller
    {
        private TrainingService trainingService;

        public TrainingController()
        {
            trainingService = new TrainingService();
        }

        [HttpGet("getTrainingInformation/{profileId}")]
        public async Task<IActionResult> getTrainingInformation(int profileId)
        {
            return Ok(trainingService.getTrainingInformation(profileId));
        }

        [HttpPut("updateTrainingInformation/{profileId}")]
        public async Task<IActionResult> updateTrainingInformation([FromBody] TrainingDTO trainingInformationDTO, int profileId)
        {
            trainingService.updateTrainingInformation(trainingInformationDTO, profileId);
            return Ok();
        }
        

    }
}
