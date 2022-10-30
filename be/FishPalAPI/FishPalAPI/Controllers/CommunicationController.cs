using FishPalAPI.Data;
using FishPalAPI.Data.Communication;
using FishPalAPI.Models;
using FishPalAPI.Models.DocumentMessageModels;
using FishPalAPI.Services;
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
        [HttpGet("getAllMessage/{messageTypeToReturn}/{userEmail}")]
        public async Task<IActionResult> getAllMessage(int messageTypeToReturn, string userEmail)
        {
            try
            {
                return Ok(communicationService.getAllMessages(messageTypeToReturn, userEmail));
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
        [HttpPost("approveDeclineMessage")]
        public async Task<IActionResult> approveDeclineMessage([FromBody] Messages T)
        {
            try
            {
                bool result = communicationService.approveDeclineMessages(T);
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
        [HttpGet("getAll/federations/{userEmail}")]
        public async Task<IActionResult> federations(string userEmail)
        {
            return Ok(communicationService.getAllFederations(userEmail));
        }

        /// <summary>
        /// gets all the provinces for new messages
        /// </summary>
        /// <returns></returns>
        [HttpPost("getAll/provinces/{userEmail}")]
        public async Task<IActionResult> provinces(string userEmail, [FromBody] FederationDTO[] T)
        {
            return Ok(communicationService.getAllProvinces(userEmail, T));
        }

        /// <summary>
        /// gets all the clubs for communication
        /// </summary>
        /// <returns></returns>
        [HttpGet("getAll/clubs/{userEmail}")]
        public async Task<IActionResult> clubs(string userEmail)
        {
            return Ok(communicationService.getAllClubs(userEmail));
        }

        /// <summary>
        /// gets all the people for communication
        /// </summary>
        /// <returns></returns>
        [HttpGet("getAll/people/{userEmail}")]
        public async Task<IActionResult> people(string userEmail)
        {
            return Ok(communicationService.getAllPeople(userEmail));
        }

        [HttpPost("document/send")]
        public async Task<IActionResult> uploadDocumentMessage([FromBody] UploadDocumentMessageDTO documentMessageDTO)
        {
            communicationService.uploadDocument(documentMessageDTO);
            return Ok();
        }

        [HttpGet("document/inbox")]
        public async Task<IActionResult> getDocumentMessageInbox()
        {
            return Ok(communicationService.getInboxDocumentMessages());
        }

        [HttpGet("document/outbox")]
        public async Task<IActionResult> getDocumentMessageOutbox()
        {
            return Ok(communicationService.getOutboxDocumentMessages());
        }

        [HttpGet("document/pending")]
        public async Task<IActionResult> getDocumentMessagePending()
        {
            return Ok(communicationService.getPendingDocumentMessages());
        }

        [HttpGet("document/aprove/{id}")]
        public async Task<IActionResult> aprovePendingDocument(int id)
        {
            return Ok(communicationService.aproveDocumentMessage(id));
        }

        [HttpGet("document/decline/{id}")]
        public async Task<IActionResult> declinePendingDocument(int id)
        {
            return Ok(communicationService.declineDocumentMessage(id));
        }
    }
}
