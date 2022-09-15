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

        public GeneralController()
        {
            clubService = new ClubService();
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

    }
}
