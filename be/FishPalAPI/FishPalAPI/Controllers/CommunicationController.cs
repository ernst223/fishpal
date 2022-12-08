﻿using FishPalAPI.Data;
using FishPalAPI.Models;
using FishPalAPI.Models.DocumentMessageModels;
using FishPalAPI.Models.MessagesModels;
using FishPalAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FishPalAPI.Controllers
{
    [Route("api/communication")]
    public class CommunicationController : Controller
    {
        private CommunicationService communicationService;
        private UserManager<User> userMgr;
        public CommunicationController(UserManager<User> userMgr)
        {
            this.userMgr = userMgr;
            communicationService = new CommunicationService(this.userMgr);
        }

        /// <summary>
        /// gets all the Federations including sasacc for communication
        /// </summary>
        /// <returns></returns>
        [HttpGet("getAll/federations/{role}")]
        public async Task<IActionResult> federations(string role)
        {
            return Ok(communicationService.getAllFederations(role));
        }

        /// <summary>
        /// gets all the provinces for a specific federation
        /// </summary>
        /// <returns></returns>
        [HttpGet("getAll/getAllProvincesForSelectedFederation/{role}/{federationId}")]
        public async Task<IActionResult> getAllProvincesForSelectedFederation(string role, int federationId)
        {
            return Ok(communicationService.getAllProvincesForSelectedFederation(role, federationId));
        }

        /// <summary>
        /// gets all the clubs for a specific province
        /// </summary>
        /// <returns></returns>
        [HttpPost("getAll/getAllClubsForSelectedProvinces")]
        public async Task<IActionResult> getAllClubsForSelectedProvinces([FromBody] ProvinceDTO provinces)
        {
            return Ok(communicationService.getAllClubsForSelectedProvinces(provinces));
        }
        
        /// <summary>
        /// gets all the Federations including sasacc for communication
        /// </summary>
        /// <returns></returns>
        [HttpGet("getAll/RolesCurrentRoleCanSendTo/{role}")]
        public async Task<IActionResult> RolesCurrentRoleCanSendTo(string role)
        {
            return Ok(communicationService.RolesCurrentRoleCanSendTo(role));
        }

        [HttpGet("accessableprofiles/{profileId}")]
        public async Task<IActionResult> getAccessableProfiles(int profileId)
        {
            return Ok(communicationService.getAccessableProfiles(profileId));
        }

        [HttpPost("document/send/{profileId}/{sendTo}")]
        public async Task<IActionResult> uploadDocumentMessage(IFormFile file, int profileId, string sendTo)
        {
            return Ok(await communicationService.uploadDocumentAsync(file, profileId, sendTo));
        }

        [HttpPut("document/update")]
        public async Task<IActionResult> updateDocumentMessage([FromBody] UploadDocumentMessageDTO uploadDocumentMessageDTO)
        {
            communicationService.updateDocument(uploadDocumentMessageDTO);
            return Ok();
        }

        [HttpGet("document/inbox/{profileId}")]
        public async Task<IActionResult> getDocumentMessageInbox(int profileId)
        {
            return Ok(communicationService.getInboxDocumentMessages(profileId));
        }

        [HttpGet("document/outbox/{profileId}")]
        public async Task<IActionResult> getDocumentMessageOutbox(int profileId)
        {
            return Ok(communicationService.getOutboxDocumentMessages(profileId));
        }

        [HttpGet("document/pending/{profileId}")]
        public async Task<IActionResult> getDocumentMessagePending(int profileId)
        {
            return Ok(communicationService.getPendingDocumentMessages(profileId));
        }

        [HttpGet("document/aprove/{id}")]
        public async Task<IActionResult> aprovePendingDocument(int id)
        {
            communicationService.aproveDocumentMessage(id);
            return Ok();
        }

        [HttpGet("document/decline/{id}")]
        public async Task<IActionResult> declinePendingDocument(int id)
        {
            communicationService.declineDocumentMessage(id);
            return Ok();
        }

        [HttpGet("message/send/{profileId}/{sendTo}")]
        public async Task<IActionResult> uploadCommunicationMessage(int profileId, string sendTo)
        {
            return Ok(await communicationService.uploadMessageAsync(profileId, sendTo));
        }

        [HttpPut("message/update")]
        public async Task<IActionResult> updateCommunicationMessage([FromBody] UploadDocumentMessageDTO uploadDocumentMessageDTO)
        {
            communicationService.updateCommunication(uploadDocumentMessageDTO);
            return Ok();
        }

        [HttpGet("message/inbox/{profileId}")]
        public async Task<IActionResult> getCommunicationMessageInbox(int profileId)
        {
            return Ok(communicationService.getInboxCommunicationMessages(profileId));
        }

        [HttpGet("message/outbox/{profileId}")]
        public async Task<IActionResult> getCommunicationMessageOutbox(int profileId)
        {
            return Ok(communicationService.getOutboxCommunicationMessages(profileId));
        }

        [HttpGet("message/pending/{profileId}")]
        public async Task<IActionResult> getCommunicationMessagePending(int profileId)
        {
            return Ok(communicationService.getPendingCommunicationMessages(profileId));
        }

        [HttpGet("message/aprove/{id}")]
        public async Task<IActionResult> aprovePendingCommunication(int id)
        {
            communicationService.aproveCommunicationMessage(id);
            return Ok();
        }

        [HttpGet("message/decline/{id}")]
        public async Task<IActionResult> declinePendingCommunication(int id)
        {
            communicationService.declineCommunicationMessage(id);
            return Ok();
        }
    }
}
