using FishPalAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FishPalAPI.Controllers
{
    [Route("api/general")]
    public class GeneralController: Controller
    {
        private ClubService clubService;
        private UserService userService;

        public GeneralController()
        {
            clubService = new ClubService();
            userService = new UserService();
        }

        [HttpGet("facets")]
        public async Task<IActionResult> getAllFacets()
        {
            return Ok(clubService.getAllFacetsForRegistration());
        }

        [HttpGet("clubs/{facet}/{province}")]
        public async Task<IActionResult> getFacetClubsByProvince(int facet, int province)
        {
            return Ok(clubService.getFacetClubsInProvince(facet, province));
        }

        [HttpGet("allUserInfo/{username}/{federation}")]
        public async Task<IActionResult> allUserInfo(string username, int? federation)
        {
            return Ok(userService.allUserInfo(username, federation));
        }

    }
}
