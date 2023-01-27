using FishPalAPI.Data;
using FishPalAPI.Models;
using FishPalAPI.Models.DocumentMessageModels;
using FishPalAPI.Models.MessagesModels;
using FishPalAPI.Models.RoleManagement;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PostmarkDotNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DocumentMessageDTO = FishPalAPI.Models.DocumentMessageModels.DocumentMessageDTO;

namespace FishPalAPI.Services
{
    public class CommunicationService
    {
        private ApplicationDbContext context;
        private UserManager<User> userMgr;
        public CommunicationService(UserManager<User> userMgr)
        {
            this.userMgr = userMgr;
            context = new ApplicationDbContext();
        }

        public CommunicationService()
        {
            context = new ApplicationDbContext();
        }

        public List<Facet> getAllFederations(string role)
        {
            if (role == "A1" || role == "A0")
            {
                return  context.Facets.ToList();
            }
            else {
                return null;
            }
        }

        public List<Province> getAllProvincesForSelectedFederation(string role, int federationId)
        {
            if (role == "A1" || role == "A0" || role == "B1" || role == "B0" || role == "C1" || role == "C0" || role == "D1" || role == "D0")
            {
                var provinces = context.Clubs.Include(x => x.Province).Include(x => x.Facet).Where(x => x.Facet.Id == federationId).Select(x=>x.Province).Distinct().ToList();
                return provinces;
            }
            else
            {
                return null;
            }
        }
        public List<Club> getAllClubsForSelectedProvinces(ProvinceDTO provinces)
        {
            var clubs = context.Clubs.Include(x => x.Province).Where(x => provinces.SelectedProvinceIds.Contains(x.Province.Id)).ToList();
            return clubs;
        }


        public List<string> RolesCurrentRoleCanSendTo(string role)
        {
            List<string> rolesUserCanSendTo = new List<string>();

            if (role == "A1" || role == "A0")
            {
                return rolesUserCanSendTo = context.Role.Select(x => x.Description).ToList();
            }

            if (new string[] { "B0", "B1" }.Contains(role)) {

                return context.Role
                    .Where(x => x.Description == "A1" ||
                     x.Description == "B3" ||
                     x.Description == "B2" ||
                     x.Description == "B1" ||
                      x.Description == "B0" ||
                     x.Description == "C1")
                    .Select(x => x.Description).ToList();
            }

            if (new string[] { "C0", "C1" }.Contains(role))
            {

                return context.Role
                    .Where(x => x.Description == "B1" ||
                     x.Description == "C3" ||
                     x.Description == "C2" ||
                     x.Description == "C1" ||
                     x.Description == "C0" ||
                     x.Description == "D1")
                    .Select(x => x.Description).ToList();
            }

            if (new string[] { "D0", "D1" }.Contains(role))
            {

                return context.Role
                    .Where(x => x.Description == "C1" ||
                     x.Description == "D3" ||
                     x.Description == "D2" ||
                     x.Description == "D1" ||
                     x.Description == "D0" ||
                     x.Description == "E1" ||
                     x.Description == "E0")
                    .Select(x => x.Description).ToList();
            }

            else
            {
                return null;
            }
        }

        public void updateDocument(UploadDocumentMessageDTO T)
        {
            var document = context.Documents.Where(a => a.Id == T.documentId).FirstOrDefault();
            document.Title = T.title;
            document.Note = T.note;
            context.SaveChanges();
        }

        public void updateEvent(UploadEventDTO T)
        {
            var tempEvent = context.Events.Where(a => a.Id == T.eventId).FirstOrDefault();
            tempEvent.Title = T.title;
            tempEvent.Description = T.description;
            tempEvent.StartDate = T.startDate;
            tempEvent.EndDate = T.endDate;
            tempEvent.TypeOfEvent = T.TypeOfEvent;
            context.SaveChanges();
        }

        public void updateCommunication(UploadDocumentMessageDTO T)
        {
            var communication = context.Communications.Where(a => a.Id == T.documentId).FirstOrDefault();
            communication.Title = T.title;
            communication.Note = T.note;
            context.SaveChanges();
        }

        public List<RoleManagementUsersDTO> getAccessableProfiles(int profileId)
        {
            UserProfile currentProfile = context.UserProfiles.Include(a => a.club).Include(a => a.role).Where(a => a.Id == profileId).FirstOrDefault();
            var higherPresidents = getHigerPresidentProfiles(currentProfile);
            var lowerPresidents = getLowerPresidentProfiles(currentProfile);
            var sameLevelProfiles = getProfilesInSameRole(currentProfile);
            List<UserProfile> tempList = new List<UserProfile>();
            if (higherPresidents != null)
            {
                foreach (var entry in higherPresidents)
                {
                    if (tempList.Where(a => a.Id == entry.Id).FirstOrDefault() == null)
                    {
                        tempList.Add(entry);
                    }
                }
            }

            if(lowerPresidents != null)
            {
                foreach (var entry in lowerPresidents)
                {
                    if (tempList.Where(a => a.Id == entry.Id).FirstOrDefault() == null)
                    {
                        tempList.Add(entry);
                    }
                }
            }

            if (sameLevelProfiles != null)
            {
                foreach (var entry in sameLevelProfiles)
                {
                    if (tempList.Where(a => a.Id == entry.Id).FirstOrDefault() == null)
                    {
                        tempList.Add(entry);
                    }
                }
            }

            List<RoleManagementUsersDTO> result = new List<RoleManagementUsersDTO>();
            foreach (var entry in tempList)
            {
                result.Add(new RoleManagementUsersDTO()
                {
                    Id = entry.Id,
                    FullName = getProfileFullName(entry),
                    Username = context.Users.Include(a => a.profiles).Where(a => a.profiles.Contains(entry)).FirstOrDefault().UserName,
                    Facet = getProfileFacetName(entry),
                    Role = entry.role.Description + " " + entry.role.FullName,
                    Status = false,
                });
            }
            return result;
        }

        private string getProfileFullName(UserProfile userProfile)
        {
            var currentUser = context.Users.Include(a => a.profiles).Where(a => a.profiles.Contains(userProfile)).FirstOrDefault();
            return currentUser.Name + " " + currentUser.Surname;
        }

        private string getProfileFacetName(UserProfile userProfile)
        {
            var userFacet = context.UserProfiles.Include(a => a.club).ThenInclude(a => a.Facet)
                    .Where(a => a.Id == userProfile.Id).FirstOrDefault().club.Facet;
            return userFacet.Name + " " + userFacet.Federation;
        }

        public async Task<int> uploadDocumentAsync(IFormFile document, int profileId, string sendTo)
        {
            // Saving the document meta data
            UserProfile currentProfile = context.UserProfiles.Include(a => a.club).Include(a => a.role).Where(a => a.Id == profileId).FirstOrDefault();
            Document documentToAdd = new Document();
            documentToAdd.Aproved = false;
            documentToAdd.CreatedDate = DateTime.Now;
            documentToAdd.CreatedBy = currentProfile;
            context.Documents.Add(documentToAdd);
            context.SaveChanges();

            // Store file on server
            documentToAdd = context.Documents.OrderByDescending(a => a.Id).FirstOrDefault();
            await UploadFile(document, documentToAdd.Id);

            foreach (var profile in getProfilesToSendTo(sendTo))
            {
                context.DocumentMessages.Add(new DocumentMessage()
                {
                    Document = documentToAdd,
                    Recipient = profile
                });
            }

            context.SaveChanges();
            return documentToAdd.Id;
        }

        public async Task<int> uploadEvent(IFormFile document, int profileId)
        {
            // Saving the document meta data
            UserProfile currentProfile = context.UserProfiles.Include(a => a.club).Include(a => a.role).Where(a => a.Id == profileId).FirstOrDefault();
            Event eventToAdd = new Event();
            eventToAdd.Approved = false;
            eventToAdd.CreatedDate = DateTime.Now;
            eventToAdd.userProfile = currentProfile;
            context.Events.Add(eventToAdd);
            context.SaveChanges();

            // Store file on server
            eventToAdd = context.Events.OrderByDescending(a => a.Id).FirstOrDefault();
            await UploadEventFile(document, eventToAdd.Id);

            context.SaveChanges();
            return eventToAdd.Id;
        }

        public async Task<int> uploadMessageAsync(int profileId, string sendTo)
        {
            // Saving the document meta data
            UserProfile currentProfile = context.UserProfiles.Include(a => a.club).Include(a => a.role).Where(a => a.Id == profileId).FirstOrDefault();
            Communication communicationToAdd = new Communication();
            communicationToAdd.Aproved = false;
            communicationToAdd.CreatedDate = DateTime.Now;
            communicationToAdd.CreatedBy = currentProfile;
            context.Communications.Add(communicationToAdd);
            context.SaveChanges();

            // Store file on server
            communicationToAdd = context.Communications.OrderByDescending(a => a.Id).FirstOrDefault();

            foreach (var profile in getProfilesToSendTo(sendTo))
            {
                context.CommunicationMessages.Add(new CommunicationMessage()
                {
                    Communication = communicationToAdd,
                    Recipient = profile
                });
            }

            context.SaveChanges();
            return communicationToAdd.Id;
        }

        private List<UserProfile> getProfilesToSendTo(string sendTo)
        {
            string[] profiles = sendTo.Split(',');
            List<UserProfile> result = new List<UserProfile>();
            foreach (var entry in profiles)
            {
                result.Add(context.UserProfiles.Where(a => a.Id == int.Parse(entry)).FirstOrDefault());
            }
            return result;
        }

        private async Task<bool> UploadFile(IFormFile ufile, int documentId)
        {
            if (ufile != null && ufile.Length > 0)
            {
                var fileName = Path.GetFileName(documentId.ToString() + ".pdf");
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\documents", fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await ufile.CopyToAsync(fileStream);
                }
                return true;
            }
            return false;
        }

        private async Task<bool> UploadEventFile(IFormFile ufile, int documentId)
        {
            if (ufile != null && ufile.Length > 0)
            {
                var fileName = Path.GetFileName(documentId.ToString() + ".pdf");
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\events", fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await ufile.CopyToAsync(fileStream);
                }
                return true;
            }
            return false;
        }

        public List<DocumentMessageDTO> getInboxDocumentMessages(int profileId)
        {
            List<DocumentMessageDTO> messages = new List<DocumentMessageDTO>();
            var userProfile = context.UserProfiles.Where(a => a.Id == profileId).FirstOrDefault();
            var user = context.Users.Include(a => a.profiles).Where(a => a.profiles.Contains(userProfile)).FirstOrDefault();
            var tempMessages = context.DocumentMessages.Include(a => a.Document).Include(a => a.Recipient)
                .Where(a => a.Recipient.Id == profileId && a.Document.Aproved == true).OrderByDescending(a => a.Id).ToList();

            foreach(var entry in tempMessages)
            {
                messages.Add(new DocumentMessageDTO()
                {
                    id = entry.Document.Id,
                    note = entry.Document.Note,
                    sendFrom = user.UserName,
                    title = entry.Document.Title
                });
            }
            return messages;
        }

        public List<EventDTO> getEvents(int profileId)
        {
            List<EventDTO> result = new List<EventDTO>();
            var tempEvents = context.Events.Where(a => a.Approved == true).ToList();
            var userProfile = context.UserProfiles.Where(a => a.Id == profileId).FirstOrDefault();
            var user = context.Users.Include(a => a.profiles).Where(a => a.profiles.Contains(userProfile)).FirstOrDefault();
            foreach(var entry in tempEvents)
            {
                result.Add(new EventDTO()
                {
                    eventId = entry.Id,
                    endDate = entry.EndDate,
                    TypeOfEvent = entry.TypeOfEvent,
                    description = entry.Description,
                    startDate = entry.StartDate,
                    title = entry.Title,
                    userProfile = user.Name + " " + user.Surname,
                    userEmail = user.Email,
                    memberNumber = getProfileMemberNumber(userProfile)
                });
            }
            return result;
        }

        public List<EventDTO> getEventsOutbox(int profileId)
        {
            List<EventDTO> result = new List<EventDTO>();
            var userProfile = context.UserProfiles.Where(a => a.Id == profileId).FirstOrDefault();
            var user = context.Users.Include(a => a.profiles).Where(a => a.profiles.Contains(userProfile)).FirstOrDefault();
            var tempEvents = context.Events.Include(a => a.userProfile).Where(a => a.Approved == true && a.userProfile.Id == userProfile.Id).ToList();
            foreach (var entry in tempEvents)
            {
                result.Add(new EventDTO()
                {
                    eventId = entry.Id,
                    endDate = entry.EndDate,
                    TypeOfEvent = entry.TypeOfEvent,
                    description = entry.Description,
                    startDate = entry.StartDate,
                    title = entry.Title,
                    userProfile = user.Name + " " + user.Surname,
                    userEmail = user.Email,
                    memberNumber = getProfileMemberNumber(userProfile)
                });
            }
            return result;
        }

        public List<EventDTO> getEventsPending(int profileId)
        {
            List<EventDTO> result = new List<EventDTO>();
            var tempEvents = context.Events.Where(a => a.Approved == false).ToList();
            var userProfile = context.UserProfiles.Where(a => a.Id == profileId).FirstOrDefault();
            var user = context.Users.Include(a => a.profiles).Where(a => a.profiles.Contains(userProfile)).FirstOrDefault();
            foreach (var entry in tempEvents)
            {
                result.Add(new EventDTO()
                {
                    eventId = entry.Id,
                    endDate = entry.EndDate,
                    TypeOfEvent = entry.TypeOfEvent,
                    description = entry.Description,
                    startDate = entry.StartDate,
                    title = entry.Title,
                    userProfile = user.Name + " " + user.Surname,
                    userEmail = user.Email,
                    memberNumber = getProfileMemberNumber(userProfile)
                });
            }
            return result;
        }

        public List<DocumentMessageDTO> getInboxCommunicationMessages(int profileId)
        {
            List<DocumentMessageDTO> messages = new List<DocumentMessageDTO>();
            var userProfile = context.UserProfiles.Where(a => a.Id == profileId).FirstOrDefault();
            var user = context.Users.Include(a => a.profiles).Where(a => a.profiles.Contains(userProfile)).FirstOrDefault();
            var tempMessages = context.CommunicationMessages.Include(a => a.Communication).Include(a => a.Recipient)
                .Where(a => a.Recipient.Id == profileId && a.Communication.Aproved == true).OrderByDescending(a => a.Id).ToList();

            foreach (var entry in tempMessages)
            {
                messages.Add(new DocumentMessageDTO()
                {
                    id = entry.Communication.Id,
                    note = entry.Communication.Note,
                    sendFrom = user.UserName,
                    title = entry.Communication.Title
                });
            }
            return messages;
        }

        public List<DocumentMessageDTO> getOutboxDocumentMessages(int profileId)
        {
            List<DocumentMessageDTO> messages = new List<DocumentMessageDTO>();
            var userProfile = context.UserProfiles.Where(a => a.Id == profileId).FirstOrDefault();
            var user = context.Users.Include(a => a.profiles).Where(a => a.profiles.Contains(userProfile)).FirstOrDefault();
            var tempMessages = context.Documents.Where(a => a.CreatedBy == userProfile).ToList();

            foreach (var entry in tempMessages)
            {
                messages.Add(new DocumentMessageDTO()
                {
                    id = entry.Id,
                    note = entry.Note,
                    sendFrom = user.UserName,
                    title = entry.Title
                });
            }
            return messages;
        }

        public List<DocumentMessageDTO> getOutboxCommunicationMessages(int profileId)
        {
            List<DocumentMessageDTO> messages = new List<DocumentMessageDTO>();
            var userProfile = context.UserProfiles.Where(a => a.Id == profileId).FirstOrDefault();
            var user = context.Users.Include(a => a.profiles).Where(a => a.profiles.Contains(userProfile)).FirstOrDefault();
            var tempMessages = context.Communications.Where(a => a.CreatedBy == userProfile).ToList();

            foreach (var entry in tempMessages)
            {
                messages.Add(new DocumentMessageDTO()
                {
                    id = entry.Id,
                    note = entry.Note,
                    sendFrom = user.UserName,
                    title = entry.Title
                });
            }
            return messages;
        }

        public List<DocumentMessageDTO> getPendingDocumentMessages(int profileId)
        {
            List<DocumentMessageDTO> messages = new List<DocumentMessageDTO>();
            var userProfile = context.UserProfiles.Where(a => a.Id == profileId).FirstOrDefault();
            var user = context.Users.Include(a => a.profiles).Where(a => a.profiles.Contains(userProfile)).FirstOrDefault();
            var tempMessages = context.Documents
                .Where(a => a.Aproved == false).OrderByDescending(a => a.Id).ToList();

            foreach (var entry in tempMessages)
            {
                messages.Add(new DocumentMessageDTO()
                {
                    id = entry.Id,
                    note = entry.Note,
                    sendFrom = user.UserName,
                    title = entry.Title
                });
            }
            return messages;
        }

        public List<DocumentMessageDTO> getPendingCommunicationMessages(int profileId)
        {
            List<DocumentMessageDTO> messages = new List<DocumentMessageDTO>();
            var userProfile = context.UserProfiles.Where(a => a.Id == profileId).FirstOrDefault();
            var user = context.Users.Include(a => a.profiles).Where(a => a.profiles.Contains(userProfile)).FirstOrDefault();
            var tempMessages = context.Communications
                .Where(a => a.Aproved == false).OrderByDescending(a => a.Id).ToList();

            foreach (var entry in tempMessages)
            {
                messages.Add(new DocumentMessageDTO()
                {
                    id = entry.Id,
                    note = entry.Note,
                    sendFrom = user.UserName,
                    title = entry.Title
                });
            }
            return messages;
        }

        public void aproveDocumentMessage(int id)
        {
            var documentMessage = context.DocumentMessages.Include(a => a.Document).Where(a => a.Document.Id == id).FirstOrDefault();
            documentMessage.Document.AprovalDate = DateTime.Now;
            documentMessage.Document.Aproved = true;
            context.SaveChanges();
        }

        public void aproveEvent(int id)
        {
            var tempEvent = context.Events.Where(a => a.Id == id).FirstOrDefault();
            tempEvent.ApprovedDate = DateTime.Now;
            tempEvent.Approved = true;
            context.SaveChanges();
        }

        public void aproveCommunicationMessage(int id)
        {
            var communicationMessage = context.CommunicationMessages.Include(a => a.Communication).Where(a => a.Communication.Id == id).FirstOrDefault();
            communicationMessage.Communication.AprovalDate = DateTime.Now;
            communicationMessage.Communication.Aproved = true;
            context.SaveChanges();
        }

        public void declineDocumentMessage(int id)
        {
            var documentMessage = context.DocumentMessages.Include(a => a.Document).Where(a => a.Document.Id == id).FirstOrDefault();
            var document = documentMessage.Document;
            context.Remove(document);
            context.Remove(documentMessage);
            context.SaveChanges();
        }

        public void declineEvent(int id)
        {
            var tempEvent = context.Events.Where(a => a.Id == id).FirstOrDefault();
            context.Remove(tempEvent);
            context.SaveChanges();
        }

        public void declineCommunicationMessage(int id)
        {
            var communicationMessage = context.CommunicationMessages.Include(a => a.Communication).Where(a => a.Communication.Id == id).FirstOrDefault();
            var communication = communicationMessage.Communication;
            context.Remove(communication);
            context.Remove(communicationMessage);
            context.SaveChanges();
        }

        public List<UserProfile> getLowerPresidentProfiles(UserProfile profile)
        {
            var userClub = profile.club.Name;
            var userRole = profile.role.Description;

            if (new string[] { "D0", "D1", "D2", "D3", "D4", "D5", "D6", "D7", "D8", "D9", "D10", "D11", "D12", "D13" }.Contains(userRole))
            {
                var userProvince = context.UserProfiles.Include(a => a.club).ThenInclude(a => a.Province).ThenInclude(a => a.Facets)
                    .Where(a => a.Id == profile.Id).FirstOrDefault().club.Province;
                var userFacet = context.UserProfiles.Include(a => a.club).ThenInclude(a => a.Facet)
                    .Where(a => a.Id == profile.Id).FirstOrDefault().club.Facet;

                return context.UserProfiles.Include(a => a.club).ThenInclude(a => a.Facet).ThenInclude(a => a.Provinces).Include(a => a.role)
                    .Where(a => a.club.Name == userClub && a.club.Facet.Id == userFacet.Id && a.club.Province.Id == userProvince.Id && (
                a.role.Description == "E1"
                )).ToList();
            }
            else if (new string[] { "C0", "C1", "C2", "C3", "C4", "C5", "C6", "C7", "C8", "C9", "C10", "C11", "C12", "C13" }.Contains(userRole))
            {
                var userProvince = context.UserProfiles.Include(a => a.club).ThenInclude(a => a.Province).ThenInclude(a => a.Facets)
                    .Where(a => a.Id == profile.Id).FirstOrDefault().club.Province;
                var userFacet = context.UserProfiles.Include(a => a.club).ThenInclude(a => a.Facet)
                    .Where(a => a.Id == profile.Id).FirstOrDefault().club.Facet;

                return context.UserProfiles.Include(a => a.club).ThenInclude(a => a.Facet).ThenInclude(a => a.Provinces).Include(a => a.role)
                    .Where(a => a.club.Province == userProvince && a.club.Facet.Id == userFacet.Id && (
                a.role.Description == "D1"
                )).ToList();
            }
            else if (new string[] { "B0", "B1", "B2", "B3", "B4", "B5", "B6", "B7", "B8", "B9", "B10", "B11", "B12", "B13" }.Contains(userRole))
            {
                var userProvince = context.UserProfiles.Include(a => a.club).ThenInclude(a => a.Province).ThenInclude(a => a.Facets)
                    .Where(a => a.Id == profile.Id).FirstOrDefault().club.Province;
                var userFacet = context.UserProfiles.Include(a => a.club).ThenInclude(a => a.Facet)
                    .Where(a => a.Id == profile.Id).FirstOrDefault().club.Facet;

                return context.UserProfiles.Include(a => a.club).ThenInclude(a => a.Facet).ThenInclude(a => a.Provinces).Include(a => a.role)
                    .Where(a => a.club.Facet.Id == userFacet.Id && a.role.Description == "C1").ToList();
            }
            else if (new string[] { "A0", "A1", "A2", "A3", "A4", "A5", "A6", "A7", "A8", "A9", "A10", "A11", "A12", "A13" }.Contains(userRole))
            {
                var userProvince = context.UserProfiles.Include(a => a.club).ThenInclude(a => a.Province).ThenInclude(a => a.Facets)
                    .Where(a => a.Id == profile.Id).FirstOrDefault().club.Province;
                var userFacet = context.UserProfiles.Include(a => a.club).ThenInclude(a => a.Facet)
                    .Where(a => a.Id == profile.Id).FirstOrDefault().club.Facet;

                return context.UserProfiles.Include(a => a.club).ThenInclude(a => a.Facet).ThenInclude(a => a.Provinces).Include(a => a.role)
                    .Where(a => a.role.Description == "B1").ToList();
            }

            return null;
        }

        public List<UserProfile> getHigerPresidentProfiles(UserProfile profile)
        {
            var userClub = profile.club.Name;
            var userRole = profile.role.Description;

            if (new string[] { "D0", "D1", "D2", "D3", "D4", "D5", "D6", "D7", "D8", "D9", "D10", "D11", "D12", "D13" }.Contains(userRole))
            {
                var userProvince = context.UserProfiles.Include(a => a.club).ThenInclude(a => a.Province).ThenInclude(a => a.Facets)
                    .Where(a => a.Id == profile.Id).FirstOrDefault().club.Province;
                var userFacet = context.UserProfiles.Include(a => a.club).ThenInclude(a => a.Facet)
                    .Where(a => a.Id == profile.Id).FirstOrDefault().club.Facet;

                return context.UserProfiles.Include(a => a.club).ThenInclude(a => a.Facet).ThenInclude(a => a.Provinces).Include(a => a.role)
                    .Where(a => a.club.Facet.Id == userFacet.Id && a.club.Province.Id == userProvince.Id && (
                a.role.Description == "C1"
                )).ToList();
            }
            else if (new string[] { "C0", "C1", "C2", "C3", "C4", "C5", "C6", "C7", "C8", "C9", "C10", "C11", "C12", "C13" }.Contains(userRole))
            {
                var userProvince = context.UserProfiles.Include(a => a.club).ThenInclude(a => a.Province).ThenInclude(a => a.Facets)
                    .Where(a => a.Id == profile.Id).FirstOrDefault().club.Province;
                var userFacet = context.UserProfiles.Include(a => a.club).ThenInclude(a => a.Facet)
                    .Where(a => a.Id == profile.Id).FirstOrDefault().club.Facet;

                return context.UserProfiles.Include(a => a.club).ThenInclude(a => a.Facet).ThenInclude(a => a.Provinces).Include(a => a.role)
                    .Where(a => a.club.Facet.Id == userFacet.Id && (
                a.role.Description == "B1"
                )).ToList();
            }
            else if (new string[] { "B0", "B1", "B2", "B3", "B4", "B5", "B6", "B7", "B8", "B9", "B10", "B11", "B12", "B13" }.Contains(userRole))
            {
                var userProvince = context.UserProfiles.Include(a => a.club).ThenInclude(a => a.Province).ThenInclude(a => a.Facets)
                    .Where(a => a.Id == profile.Id).FirstOrDefault().club.Province;
                var userFacet = context.UserProfiles.Include(a => a.club).ThenInclude(a => a.Facet)
                    .Where(a => a.Id == profile.Id).FirstOrDefault().club.Facet;

                return context.UserProfiles.Include(a => a.club).ThenInclude(a => a.Facet).ThenInclude(a => a.Provinces).Include(a => a.role)
                    .Where(a => a.role.Description == "A1").ToList();
            }

            return null;
        }

        public List<UserProfile> getProfilesInSameRole(UserProfile profile)
        {
            var userClub = profile.club.Name;
            var userRole = profile.role.Description;

            if (new string[] { "D0", "D1", "D2", "D3", "D4", "D5", "D6", "D7", "D8", "D9", "D10", "D11", "D12", "D13" }.Contains(userRole))
            {
                var userProvince = context.UserProfiles.Include(a => a.club).ThenInclude(a => a.Province).ThenInclude(a => a.Facets)
                    .Where(a => a.Id == profile.Id).FirstOrDefault().club.Province;
                var userFacet = context.UserProfiles.Include(a => a.club).ThenInclude(a => a.Facet)
                    .Where(a => a.Id == profile.Id).FirstOrDefault().club.Facet;

                return context.UserProfiles.Include(a => a.club).ThenInclude(a => a.Facet).ThenInclude(a => a.Provinces).Include(a => a.role)
                    .Where(a => a.club.Name == userClub && a.club.Facet.Id == userFacet.Id && a.club.Province.Id == userProvince.Id && (
                a.role.Description == "D1" ||
                a.role.Description == "D2" ||
                a.role.Description == "D3" ||
                a.role.Description == "D4" ||
                a.role.Description == "D5" ||
                a.role.Description == "D6" ||
                a.role.Description == "D7" ||
                a.role.Description == "D8" ||
                a.role.Description == "D9" ||
                a.role.Description == "D10" ||
                a.role.Description == "D11" ||
                a.role.Description == "D12" ||
                a.role.Description == "D13"
                )).ToList();
            }
            else if (new string[] { "C0", "C1", "C2", "C3", "C4", "C5", "C6", "C7", "C8", "C9", "C10", "C11", "C12", "C13" }.Contains(userRole))
            {
                var userProvince = context.UserProfiles.Include(a => a.club).ThenInclude(a => a.Province).ThenInclude(a => a.Facets)
                    .Where(a => a.Id == profile.Id).FirstOrDefault().club.Province;
                var userFacet = context.UserProfiles.Include(a => a.club).ThenInclude(a => a.Facet)
                    .Where(a => a.Id == profile.Id).FirstOrDefault().club.Facet;

                return context.UserProfiles.Include(a => a.club).ThenInclude(a => a.Province).Include(a => a.role).Where(a => a.club.Province.Id == userProvince.Id &&
                a.club.Facet.Id == userFacet.Id && (
                a.role.Description == "C1" ||
                a.role.Description == "C2" ||
                a.role.Description == "C3" ||
                a.role.Description == "C4" ||
                a.role.Description == "C5" ||
                a.role.Description == "C6" ||
                a.role.Description == "C7" ||
                a.role.Description == "C8" ||
                a.role.Description == "C9" ||
                a.role.Description == "C10" ||
                a.role.Description == "C11" ||
                a.role.Description == "C12" ||
                a.role.Description == "C13"
                )).ToList();
            }
            else if (new string[] { "B0", "B1", "B2", "B3", "B4", "B5", "B6", "B7", "B8", "B9", "B10", "B11", "B12", "B13" }.Contains(userRole))
            {
                var userFacet = context.UserProfiles.Include(a => a.club).ThenInclude(a => a.Facet)
                    .Where(a => a.Id == profile.Id).FirstOrDefault().club.Facet;

                return context.UserProfiles.Include(a => a.club).ThenInclude(a => a.Facet).Include(a => a.role).Where(a => a.club.Facet.Id == userFacet.Id && (
                a.role.Description == "B1" ||
                a.role.Description == "B2" ||
                a.role.Description == "B3" ||
                a.role.Description == "B4" ||
                a.role.Description == "B5" ||
                a.role.Description == "B6" ||
                a.role.Description == "B7" ||
                a.role.Description == "B8" ||
                a.role.Description == "B9" ||
                a.role.Description == "B10" ||
                a.role.Description == "B11" ||
                a.role.Description == "B12" ||
                a.role.Description == "B13"
                )).ToList();
            }
            else if (new string[] { "A0", "A1", "A2", "A3", "A4", "A5", "A6", "A7", "A8", "A9", "A10", "A11", "A12", "A13" }.Contains(userRole))
            {
                return context.UserProfiles.Include(a => a.role).Where(a =>
                a.role.Description == "A1" ||
                a.role.Description == "A2" ||
                a.role.Description == "A3" ||
                a.role.Description == "A4" ||
                a.role.Description == "A5" ||
                a.role.Description == "A6" ||
                a.role.Description == "A7" ||
                a.role.Description == "A8" ||
                a.role.Description == "A9" ||
                a.role.Description == "A10" ||
                a.role.Description == "A11" ||
                a.role.Description == "A12" ||
                a.role.Description == "A13"
                ).ToList();
            }
            return null;
        }

        public List<UserProfile> getProfilesInLowerRoles(UserProfile profile)
        {
            var userClub = profile.club.Name;
            var userRole = profile.role.Description;

            if (new string[] { "D0", "D1", "D2", "D3", "D4", "D5", "D6", "D7", "D8", "D9", "D10", "D11", "D12", "D13" }.Contains(userRole))
            {
                var userFacet = context.UserProfiles.Include(a => a.club).ThenInclude(a => a.Facet)
                    .Where(a => a.Id == profile.Id).FirstOrDefault().club.Facet;

                return context.UserProfiles.Include(a => a.role).Where(a => a.club.Facet.Id == userFacet.Id && a.club.Name == userClub &&
                (a.role.Description == "E1" ||
                a.role.Description == "E2")
                ).ToList();
            }
            else if (new string[] { "C0", "C1", "C2", "C3", "C4", "C5", "C6", "C7", "C8", "C9", "C10", "C11", "C12", "C13" }.Contains(userRole))
            {
                var userProvince = context.UserProfiles.Include(a => a.club).ThenInclude(a => a.Province)
                    .Where(a => a.Id == profile.Id).FirstOrDefault().club.Province;

                var userFacet = context.UserProfiles.Include(a => a.club).ThenInclude(a => a.Facet)
                    .Where(a => a.Id == profile.Id).FirstOrDefault().club.Facet;

                return context.UserProfiles.Include(a => a.club).ThenInclude(a => a.Province).Include(a => a.role).Where(a => a.club.Province.Id == userProvince.Id &&
                a.club.Facet.Id == userFacet.Id && (
                a.role.Description != "C1" &&
                a.role.Description != "C2" &&
                a.role.Description != "C3" &&
                a.role.Description != "C4" &&
                a.role.Description != "C5" &&
                a.role.Description != "C6" &&
                a.role.Description != "C7" &&
                a.role.Description != "C8" &&
                a.role.Description != "C9" &&
                a.role.Description != "C10" &&
                a.role.Description != "C11" &&
                a.role.Description != "C12" &&
                a.role.Description != "C13" &&
                a.role.Description != "B1" &&
                a.role.Description != "B2" &&
                a.role.Description != "B3" &&
                a.role.Description != "B4" &&
                a.role.Description != "B5" &&
                a.role.Description != "B6" &&
                a.role.Description != "B7" &&
                a.role.Description != "B8" &&
                a.role.Description != "B9" &&
                a.role.Description != "B10" &&
                a.role.Description != "B12" &&
                a.role.Description != "B13" &&
                a.role.Description != "A0" &&
                a.role.Description != "A1" &&
                a.role.Description != "A2" &&
                a.role.Description != "A1" &&
                a.role.Description != "A2" &&
                a.role.Description != "A3" &&
                a.role.Description != "A4" &&
                a.role.Description != "A5" &&
                a.role.Description != "A6" &&
                a.role.Description != "A7" &&
                a.role.Description != "A8" &&
                a.role.Description != "A9" &&
                a.role.Description != "A10" &&
                a.role.Description != "A11" &&
                a.role.Description != "A12" &&
                 a.role.Description != "A13"
                )).ToList();
            }
            else if (new string[] { "B0", "B1", "B2", "B3", "B4", "B5", "B6", "B7", "B8", "B9", "B10", "B11", "B12", "B13" }.Contains(userRole))
            {
                var userFacet = context.UserProfiles.Include(a => a.club).ThenInclude(a => a.Facet)
                    .Where(a => a.Id == profile.Id).FirstOrDefault().club.Facet;

                return context.UserProfiles.Include(a => a.club).ThenInclude(a => a.Province).Include(a => a.role).Where(a => a.club.Facet.Id == userFacet.Id && (
                a.role.Description != "B1" &&
                a.role.Description != "B2" &&
                a.role.Description != "B3" &&
                a.role.Description != "B4" &&
                a.role.Description != "B5" &&
                a.role.Description != "B6" &&
                a.role.Description != "B7" &&
                a.role.Description != "B8" &&
                a.role.Description != "B9" &&
                a.role.Description != "B10" &&
                a.role.Description != "B12" &&
                a.role.Description != "B13" &&
                a.role.Description != "A0" &&
                a.role.Description != "A1" &&
                a.role.Description != "A2" &&
                a.role.Description != "A1" &&
                a.role.Description != "A2" &&
                a.role.Description != "A3" &&
                a.role.Description != "A4" &&
                a.role.Description != "A5" &&
                a.role.Description != "A6" &&
                a.role.Description != "A7" &&
                a.role.Description != "A8" &&
                a.role.Description != "A9" &&
                a.role.Description != "A10" &&
                a.role.Description != "A11" &&
                a.role.Description != "A12" &&
                 a.role.Description != "A13"
                )).ToList();
            }
            else if (new string[] { "A0", "A1", "A2", "A3", "A4", "A5", "A6", "A7", "A8", "A9", "A10", "A11", "A12", "A13" }.Contains(userRole))
            {
                return context.UserProfiles.Include(a => a.role).Where(a =>
                a.role.Description != "A1" &&
                a.role.Description != "A2" &&
                a.role.Description != "A3" &&
                a.role.Description != "A4" &&
                a.role.Description != "A5" &&
                a.role.Description != "A6" &&
                a.role.Description != "A7" &&
                a.role.Description != "A8" &&
                a.role.Description != "A9" &&
                a.role.Description != "A10" &&
                a.role.Description != "A11" &&
                a.role.Description != "A12" &&
                 a.role.Description != "A13"
                ).ToList();
            }
            return null;
        }

        public List<Courses> getApprovedCourses()
        {
            return context.Courses.Where(a => a.Approved == true).ToList();
        }

        public List<Courses> getUnapprovedCourses()
        {
            return context.Courses.Where(a => a.Approved == false).ToList();
        }

        public bool approveCourse(int id)
        {
            var course = context.Courses.Where(a => a.Id == id).FirstOrDefault();
            course.Approved = true;
            course.ApprovedDate = DateTime.Now;
            context.SaveChanges();
            return true;
        }

        public bool declineCourse(int id)
        {
            var course = context.Courses.Where(a => a.Id == id).FirstOrDefault();
            var enrolledCourses = context.UserCourses.Include(a => a.course).Where(a => a.course.Id == id).ToList();
            foreach(var entry in enrolledCourses)
            {
                context.Remove(entry);
                context.SaveChanges();
            }
            context.Remove(course);
            context.SaveChanges();
            return true;
        }

        public bool enrollForCourse(int id, int profileId)
        {
            var userProfile = context.UserProfiles.Where(a => a.Id == profileId).FirstOrDefault();
            var user = context.Users.Include(a => a.profiles).Where(a => a.profiles.Contains(userProfile)).FirstOrDefault();
            var currentCourse = context.Courses.Where(a => a.Id == id).FirstOrDefault();
            var userCourseTest = context.UserCourses.Where(a => a.course == currentCourse && a.user == user).FirstOrDefault();
            if(userCourseTest != null)
            {
                return false;
            }
            context.UserCourses.Add(new UserCourses()
            {
                Approved = false,
                course = context.Courses.Where(a => a.Id == id).FirstOrDefault(),
                timeEnrolled = DateTime.Now,
                user = user
            });
            context.SaveChanges();
            return true;
        }

        public List<UserCoursesDTO> getEnrollmentsPending()
        {
            var userCourses = context.UserCourses.Include(a => a.course).Include(a => a.user).ThenInclude(a => a.profiles)
                .Where(a => a.Approved == false).ToList();
            List<UserCoursesDTO> result = new List<UserCoursesDTO>();
            UserProfile profile;
            foreach(var entry in userCourses)
            {
                profile = entry.user.profiles.FirstOrDefault();
                result.Add(new UserCoursesDTO()
                {
                    Id = entry.Id,
                    profileId = profile.Id,
                    userName = entry.user.Name + " " + entry.user.Surname,
                    userEmail = entry.user.Email,
                    memberNumber = getProfileMemberNumber(profile),
                    courseId = entry.course.Id,
                    courseName = entry.course.Name,
                    courseDescription = entry.course.Description,
                    Approved = entry.Approved
                });
            }
            return result;
        }

        public bool approveEnrolCourse(int id)
        {
            var userCourse = context.UserCourses.Where(a => a.Id == id).FirstOrDefault();
            userCourse.Approved = true;
            userCourse.timeEnrolled = DateTime.Now;
            context.SaveChanges();
            return true;
        }

        public bool declineEnrolCourse(int id)
        {
            var userCourse = context.UserCourses.Where(a => a.Id == id).FirstOrDefault();
            context.Remove(userCourse);
            context.SaveChanges();
            return true;
        }
        public List<Courses> getMyCourses(int profileId)
        {
            var userProfile = context.UserProfiles.Where(a => a.Id == profileId).FirstOrDefault();
            var user = context.Users.Include(a => a.profiles).Where(a => a.profiles.Contains(userProfile)).FirstOrDefault();
            var userCourses = context.UserCourses.Include(a => a.user).Include(a => a.course)
                .Where(a => a.user.Id == user.Id && a.Approved == true).ToList();
            List<Courses> result = new List<Courses>();
            foreach(var entry in userCourses)
            {
                result.Add(entry.course);
            }
            return result;
        }

        public async Task<int> uploadCourseAsync(IFormFile document, int profileId)
        {
            // Saving the document meta data
            UserProfile currentProfile = context.UserProfiles.Include(a => a.club).Include(a => a.role).Where(a => a.Id == profileId).FirstOrDefault();
            var user = context.Users.Include(a => a.profiles).Where(a => a.profiles.Contains(currentProfile)).FirstOrDefault();
            Courses courseToAdd = new Courses();
            courseToAdd.Approved = false;
            courseToAdd.UploadDate = DateTime.Now;
            context.Courses.Add(courseToAdd);
            context.SaveChanges();

            // Store file on server
            courseToAdd = context.Courses.OrderByDescending(a => a.Id).FirstOrDefault();
            await UploadCourseFile(document, courseToAdd.Id);

            context.SaveChanges();
            return courseToAdd.Id;
        }

        private async Task<bool> UploadCourseFile(IFormFile ufile, int documentId)
        {
            if (ufile != null && ufile.Length > 0)
            {
                var fileName = Path.GetFileName(documentId.ToString() + ".pdf");
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\courses", fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await ufile.CopyToAsync(fileStream);
                }
                return true;
            }
            return false;
        }

        public bool updateCourse(UpdateCourse course)
        {
            var currentCourse = context.Courses.Where(a => a.Id == course.Id).FirstOrDefault();
            currentCourse.Name = course.Name;
            currentCourse.Description = course.Description;
            currentCourse.URL = course.URL;
            context.SaveChanges();
            return true;
        }

        private string getProfileMemberNumber(UserProfile userProfile)
        {
            var currentUser = context.Users.Include(a => a.profiles).Where(a => a.profiles.Contains(userProfile)).FirstOrDefault();
            var memberNumber = currentUser.EmployeeId.ToString();
            var amountToAdd = 7 - memberNumber.Length;
            var result = "";
            for (var i = 0; i < amountToAdd; i++)
            {
                result = result + "0";
            }
            result = result + memberNumber;
            return result;
        }
    }
}
