﻿using FishPalAPI.Services;
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

        [HttpGet("allUserInfo/{profileId}/{returnAll}")]
        public async Task<IActionResult> allUserInfo(int profileId, bool returnAll)
        {
            return Ok(userService.allUserInfo(profileId, returnAll));
        }

        [HttpGet("seed")]
        public async Task<IActionResult> seedDatabase()
        {
            return Ok(userService.seed());
        }

        [HttpGet("events/public")]
        public async Task<IActionResult> getPublicEvents()
        {
            return Ok(userService.getPublicEvents());
        }
    }
}
