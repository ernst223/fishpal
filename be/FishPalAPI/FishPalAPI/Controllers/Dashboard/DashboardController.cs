using FishPalAPI.Services.Dashboard;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace FishPalAPI.Data.Dashboard
{
    [Route("api/dashboard")]
    public class DashboardController : Controller
    {
        private DashboardService dashboardService;

        public DashboardController()
        {
            dashboardService = new DashboardService();
        }

        /// <summary>
        /// gets the document aknowledgements that are true
        /// </summary>
        /// <param name="T"></param>
        /// <returns></returns>
        [HttpGet("getDocumentAknowledgementsTrueCount/{profileId}")]
        public async Task<IActionResult> getDocumentAknowledgementsTrueCount(int profileId)
        {
            try
            {
                return Ok(dashboardService.getDocumentAknowledgementsTrueCount(profileId));
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// gets the document aknowledgements that are not aknowledged yet
        /// </summary>
        /// <param name="T"></param>
        /// <returns></returns>
        [HttpGet("getDocumentAknowledgementsFalseCount/{profileId}")]
        public async Task<IActionResult> getDocumentAknowledgementsFalseCount(int profileId)
        {
            try
            {
                return Ok(dashboardService.getDocumentAknowledgementsFalseCount(profileId));
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// gets the enrolled courses
        /// </summary>
        /// <param name="T"></param>
        /// <returns></returns>
        [HttpGet("getEnrolledCoursesCount/{profileId}")]
        public async Task<IActionResult> getEnrolledCoursesCount(int profileId)
        {
            try
            {
                return Ok(dashboardService.getEnrolledCoursesCount(profileId));
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// gets the enrolled courses approved
        /// </summary>
        /// <param name="T"></param>
        /// <returns></returns>
        [HttpGet("getEnrolledCoursesApprovedCount/{profileId}")]
        public async Task<IActionResult> getEnrolledCoursesApprovedCount(int profileId)
        {
            try
            {
                return Ok(dashboardService.getEnrolledCoursesApprovedCount(profileId));
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
    }
}
