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
        /// gets the order items
        /// </summary>
        /// <param name="T"></param>
        /// <returns></returns>
        [HttpGet("getOrderItems")]
        public async Task<IActionResult> getOrderItems()
        {
            try
            {
                return Ok(dashboardService.getOrderItems());
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
    }
}
