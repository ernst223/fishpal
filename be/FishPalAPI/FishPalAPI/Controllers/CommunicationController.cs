using FishPalAPI.Data;
using FishPalAPI.Data.Communication;
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
        /// Insert a new Item that can be ordered
        /// </summary>
        /// <param name="T"></param>
        /// <returns></returns>
        [HttpPost("insertMessage")]
        public async Task<IActionResult> insertMessage([FromBody] Messages T)
        {
            try
            {
                bool result = communicationService.insertMessages(T);
                if (result)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Get all inbox messages
        /// </summary>
        /// <param name="T"></param>
        /// <returns></returns>
        [HttpGet("getAllMessage/{messageTypeToReturn}/{profileId}")]
        public async Task<IActionResult> getAllMessage(int messageTypeToReturn, int profileId)
        {
            try
            {
                return Ok(communicationService.getAllMessages(messageTypeToReturn, profileId));
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Message Approval/declining
        /// </summary>
        /// <param name="T"></param>
        /// <returns></returns>
        [HttpPost("approveDeclineMessage/{approveDecline}/{messageId}")]
        public async Task<IActionResult> approveDeclineMessage(int approveDecline, int messageId)
        {
            try
            {
                bool result = communicationService.approveDeclineMessages(approveDecline, messageId);
                if (result)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception e)
            {
                return BadRequest();
            }
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

        /// <summary>
        /// sends the message to all the selected roles inside your/selected federation
        /// </summary>
        /// <returns></returns>
        [HttpPost("getAll/sendMessages/{federationId}/{profileId}/{sendEmail}")]
        public async Task<IActionResult> sendMessages([FromBody] MessageDTO message, int federationId, int profileId, bool sendEmail)
        {
            communicationService.sendMessages(message, federationId, profileId, sendEmail);
            return Ok();
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

        /// <summary>
        /// Delete a message
        /// </summary>
        /// <param name="T"></param>
        /// <returns></returns>
        [HttpDelete("deleteMessage/{messageId}")]
        public async Task<IActionResult> deleteMessage(int messageId)
        {
            try
            {
                bool result = communicationService.deleteMessage(messageId);
                if (result)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
    }
}
