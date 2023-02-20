using FishPalAPI.Data;
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

        [HttpGet("document/acknoledge/{documentId}/{profileId}")]
        public async Task<IActionResult> setDocumentAcknoledge(int documentId, int profileId)
        {
            return Ok(communicationService.setAcknowledged(documentId, profileId));
        }

        [HttpGet("getacknoledgedusers/{documentId}")]
        public async Task<IActionResult> getAcknoledgedUsers(int documentId)
        {
            return Ok(communicationService.getDocumentsAcknowledgedUsers(documentId));
        }

        [HttpPost("document/send/{profileId}/{sendTo}")]
        public async Task<IActionResult> uploadDocumentMessage(IFormFile file, int profileId, string sendTo)
        {
            return Ok(await communicationService.uploadDocumentAsync(file, profileId, sendTo));
        }

        [HttpPost("event/send/{profileId}")]
        public async Task<IActionResult> uploadEvent(IFormFile file, int profileId)
        {
            return Ok(await communicationService.uploadEvent(file, profileId));
        }

        [HttpPut("document/update")]
        public async Task<IActionResult> updateDocumentMessage([FromBody] UploadDocumentMessageDTO uploadDocumentMessageDTO)
        {
            communicationService.updateDocument(uploadDocumentMessageDTO);
            return Ok();
        }

        [HttpPut("event/update")]
        public async Task<IActionResult> updateEventMessage([FromBody] UploadEventDTO uploadEventDTO)
        {
            communicationService.updateEvent(uploadEventDTO);
            return Ok();
        }

        [HttpGet("document/inbox/{profileId}")]
        public async Task<IActionResult> getDocumentMessageInbox(int profileId)
        {
            return Ok(communicationService.getInboxDocumentMessages(profileId));
        }

        [HttpGet("event/inbox/{profileId}")]
        public async Task<IActionResult> getEventInbox(int profileId)
        {
            return Ok(communicationService.getEvents(profileId));
        }

        [HttpGet("document/outbox/{profileId}")]
        public async Task<IActionResult> getDocumentMessageOutbox(int profileId)
        {
            return Ok(communicationService.getOutboxDocumentMessages(profileId));
        }

        [HttpGet("event/outbox/{profileId}")]
        public async Task<IActionResult> getEventOutbox(int profileId)
        {
            return Ok(communicationService.getEventsOutbox(profileId));
        }

        [HttpGet("event/delete/{documentId}")]
        public async Task<IActionResult> deleteEvent(int documentId)
        {
            return Ok(communicationService.deleteEvent(documentId));
        }

        [HttpGet("document/pending/{profileId}")]
        public async Task<IActionResult> getDocumentMessagePending(int profileId)
        {
            return Ok(communicationService.getPendingDocumentMessages(profileId));
        }

        [HttpGet("event/pending/{profileId}")]
        public async Task<IActionResult> getEventPending(int profileId)
        {
            return Ok(communicationService.getEventsPending(profileId));
        }

        [HttpGet("document/aprove/{id}")]
        public async Task<IActionResult> aprovePendingDocument(int id)
        {
            communicationService.aproveDocumentMessage(id);
            return Ok();
        }

        [HttpGet("event/aprove/{id}")]
        public async Task<IActionResult> aprovePendingEvent(int id)
        {
            communicationService.aproveEvent(id);
            return Ok();
        }

        [HttpGet("document/decline/{id}")]
        public async Task<IActionResult> declinePendingDocument(int id)
        {
            communicationService.declineDocumentMessage(id);
            return Ok();
        }

        [HttpGet("event/decline/{id}")]
        public async Task<IActionResult> declinePendingEvent(int id)
        {
            communicationService.declineEvent(id);
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

        [HttpGet("courses/myCourses/{profileId}")]
        public async Task<IActionResult> getMyCourses(int profileId)
        {
            return Ok(communicationService.getMyCourses(profileId));
        }

        [HttpGet("courses")]
        public async Task<IActionResult> getCourses()
        {
            return Ok(communicationService.getApprovedCourses());
        }

        [HttpGet("courses/unaproved")]
        public async Task<IActionResult> getAllUnaprovedCourses()
        {
            return Ok(communicationService.getUnapprovedCourses());
        }

        [HttpGet("courses/approve/{id}")]
        public async Task<IActionResult> approveCourse(int id)
        {
            return Ok(communicationService.approveCourse(id));
        }

        [HttpGet("courses/decline/{id}")]
        public async Task<IActionResult> declineCourse(int id)
        {
            return Ok(communicationService.declineCourse(id));
        }

        [HttpGet("courses/enroll/{id}/{profileId}")]
        public async Task<IActionResult> enrollForCourse(int id, int profileId)
        {
            return Ok(communicationService.enrollForCourse(id, profileId));
        }

        [HttpGet("courses/enrollments/pending")]
        public async Task<IActionResult> getPendingEnrolments()
        {
            return Ok(communicationService.getEnrollmentsPending());
        }

        [HttpGet("courses/enrolment/approve/{id}")]
        public async Task<IActionResult> approveEnrolment(int id)
        {
            return Ok(communicationService.approveEnrolCourse(id));
        }

        [HttpGet("courses/enrolment/decline/{id}")]
        public async Task<IActionResult> declineEnrolment(int id)
        {
            return Ok(communicationService.declineEnrolCourse(id));
        }

        [HttpPost("courses/create/{profileId}")]
        public async Task<IActionResult> createCourse(IFormFile file, int profileId)
        {
            var result = await communicationService.uploadCourseAsync(file, profileId);
            return Ok(result);
        }

        [HttpPut("courses/update")]
        public async Task<IActionResult> updateCourse([FromBody] UpdateCourse course)
        {
            return Ok(communicationService.updateCourse(course));
        }

        [HttpGet("export/userinformation/{currentProfileID}/{facetId}/{provinceId}/{clubId}")]
        public async Task<IActionResult> exportUserInformation(int currentProfileID, int facetId, int provinceId, int clubId)
        {
            return Ok(communicationService.exportUserInformation(currentProfileID, facetId, provinceId, clubId));
        }
    }
}
