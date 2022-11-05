using FishPalAPI.Data;
using FishPalAPI.Data.Communication;
using FishPalAPI.Models;
using FishPalAPI.Models.DocumentMessageModels;
using FishPalAPI.Models.MessagesModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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

        public bool insertMessages(Messages T)
        {
            try
            {
                context.Messages.Add(T);
                context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public List<MessageDTO> getAllMessages(int messageTypeToReturn, int profileId)
        {          
            if (messageTypeToReturn == 0)//inbox
            {
                var _messagesReceivers = context.MessageReceivers.Where(x => x.AssignedUserProfileId == profileId).Include(x => x.Messages).Where(y => y.Messages.Status == 2).ToList();

                List<MessageDTO> inboxResult = new List<MessageDTO>();
                foreach (var entry in _messagesReceivers)
                {
                    inboxResult.Add(new MessageDTO()
                    {
                        Id = entry.Messages.Id,
                        Message = entry.Messages.Message,
                        CreationDate = entry.Messages.CreationDate,
                        Status = entry.Messages.Status,
                        CreatorUserProfileId = entry.Messages.CreatorUserProfileId,
                        StatusChangeDate = entry.Messages.StatusChangeDate,
                        ApproverRequired = entry.Messages.ApproverRequired
                    });
                }
                return inboxResult;

            }

            List<Messages> _messages;

            if (messageTypeToReturn == 1)//outbox   
            {
                 _messages = context.Messages.Where(x => x.CreatorUserProfileId == profileId).ToList();

                    List<MessageDTO> outboxResult = new List<MessageDTO>();
                    foreach (var entry in _messages)
                    {
                    outboxResult.Add(new MessageDTO()
                        {
                            Id = entry.Id,
                            Message = entry.Message,
                            CreationDate = entry.CreationDate,
                            Status = entry.Status,
                            CreatorUserProfileId = entry.CreatorUserProfileId,
                            StatusChangeDate = entry.StatusChangeDate,
                            ApproverRequired = entry.ApproverRequired
                        });
                    }
                return outboxResult;
            }

            if (messageTypeToReturn == 2)//pending/to be approved 
            {
                 _messages = context.Messages.Where(x => x.ApproverRequired == profileId && x.Status == 1).ToList(); 
                    List<MessageDTO> pendingResult = new List<MessageDTO>();
                    foreach (var entry in _messages)
                    {
                    pendingResult.Add(new MessageDTO()
                        {
                            Id = entry.Id,
                            Message = entry.Message,
                            CreationDate = entry.CreationDate,
                            Status = entry.Status,
                            CreatorUserProfileId = entry.CreatorUserProfileId,
                            StatusChangeDate = entry.StatusChangeDate,
                            ApproverRequired = entry.ApproverRequired
                        });
                    }
                return pendingResult;
            }

            return null;
        }

        public List<MessageReceiversDTO> getMessageAssignedToDTO(List<MessageReceivers> messageRecievers)
        {
            List<MessageReceiversDTO> result = new List<MessageReceiversDTO>();
            foreach (var entry in messageRecievers)
            {
                result.Add(new MessageReceiversDTO()
                {
                    Id = entry.Id,
                    AssignedUserProfileId = entry.AssignedUserProfileId
                });
            }
            return result;
        }

        public bool deleteMessage(int messageId)
        {
            try
            {
                var recordToDelete = context.Messages.Where(y => y.Id == messageId).FirstOrDefault();
                context.Messages.Remove(recordToDelete);
                context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool approveDeclineMessages(int approveDecline, int messageId)
        {
            try
            {
                var recordToApproveOrDecline = context.Messages.Where(y => y.Id == messageId).FirstOrDefault();

                if (approveDecline == 0) {
                    recordToApproveOrDecline.Status = 0;
                }

                if (approveDecline == 2)
                {
                    recordToApproveOrDecline.Status = 2;
                }

                context.Messages.Update(recordToApproveOrDecline);
                context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public List<Facet> getAllFederations(string role)
        {
            List<Facet> federations = new List<Facet>();

            if (role == "A1" || role == "A0")
            {
                return  context.Facets.ToList();
            }
            else {
                return null;
            }
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

        public void sendMessages(MessageDTO message, int federationId, int profileId)
        {

            UserProfile currentProfile = context.UserProfiles.Include(a => a.club).ThenInclude(a => a.Province).Include(a => a.role).Where(a => a.Id == profileId).FirstOrDefault();

            var userClub = currentProfile.club.Name;
            var userRole = currentProfile.role.Description;


            List<UserProfile> recipients = new List<UserProfile>();

            if (new string[] { "D0", "D1" }.Contains(userRole)) //club
            {
                var userProvince = context.UserProfiles.Include(a => a.club).ThenInclude(a => a.Province).ThenInclude(a => a.Facets)
                    .Where(a => a.Id == currentProfile.Id).FirstOrDefault().club.Province;
              
                recipients = context.UserProfiles.Include(a => a.club).ThenInclude(a => a.Facet).ThenInclude(a => a.Provinces).Include(a => a.role)
                    .Where(a => a.club.Name == userClub && a.club.Facet.Id == federationId && a.club.Province.Id == userProvince.Id && (message.rolesToSendTo.Contains(a.role.Description))).ToList();
            }
            else if (new string[] { "C0", "C1" }.Contains(userRole)) //province
            {
                var userProvince = context.UserProfiles.Include(a => a.club).ThenInclude(a => a.Province).ThenInclude(a => a.Facets)
                    .Where(a => a.Id == currentProfile.Id).FirstOrDefault().club.Province;
                
                recipients = context.UserProfiles.Include(a => a.club).ThenInclude(a => a.Province).Include(a => a.role).Where(a => a.club.Province.Id == userProvince.Id &&
                a.club.Facet.Id == federationId && (message.rolesToSendTo.Contains(a.role.Description))).ToList();
            }
            else if (new string[] { "B0", "B1" }.Contains(userRole)) //federation/facet
            {              
                recipients = context.UserProfiles.Include(a => a.club).ThenInclude(a => a.Facet).Include(a => a.role).Where(a => a.club.Facet.Id == federationId && (message.rolesToSendTo.Contains(a.role.Description))).ToList();
            }
            else if (new string[] { "A0", "A1" }.Contains(userRole)) //sasacc
            {
                recipients = context.UserProfiles.Include(a => a.role).Include(a => a.club).ThenInclude(a => a.Facet).Where(a => message.rolesToSendTo.Contains(a.role.Description) && a.club.Facet.Id == federationId).ToList();
            }

            if (recipients != null) {

                Messages newMessage = new Messages();

                if (new string[] { "A0", "B0", "C0", "D0" }.Contains(currentProfile.role.Description)) {
                    message.Status = 1; //pending
                } else if (new string[] { "A1", "B1", "C1", "D1" }.Contains(currentProfile.role.Description)) {
                    message.Status = 2; //Allready approved
                }

                int approverId = 0;

                if (currentProfile.role.Description == "A0") {
                    approverId = context.UserProfiles.Include(a => a.role)
                       .Where(a => a.role.Description == "A1").Select(a=>a.Id).FirstOrDefault();

                }                 
                else if (currentProfile.role.Description == "B0") {
                    approverId = context.UserProfiles.Include(a => a.role)
                        .Include(a => a.club)
                        .ThenInclude(a => a.Facet)
                        .Include(a => a.role)
                        .Where(a => a.club.Facet.Id == federationId && a.role.Description == "B1").Select(a => a.Id).FirstOrDefault();
                }
                else if (currentProfile.role.Description == "C0")
                {
                    var userProvince = context.UserProfiles.Include(a => a.club).ThenInclude(a => a.Province).ThenInclude(a => a.Facets)
                    .Where(a => a.Id == currentProfile.Id).FirstOrDefault().club.Province;

                    approverId = context.UserProfiles.Include(a => a.role)
                       .Include(a => a.club)
                        .ThenInclude(a => a.Facet)
                        .ThenInclude(a=>a.Provinces)
                        .Include(a => a.role)
                        .Where(a => a.club.Province.Id == userProvince.Id && a.club.Facet.Id == federationId && a.role.Description == "C1").Select(a => a.Id).FirstOrDefault();
                }
                else if (currentProfile.role.Description == "D0")
                {
                    var userProvince = context.UserProfiles.Include(a => a.club).ThenInclude(a => a.Province).ThenInclude(a => a.Facets)
                    .Where(a => a.Id == currentProfile.Id).FirstOrDefault().club.Province;

                    approverId = context.UserProfiles.Include(a => a.role)
                       .Include(a => a.club)
                        .ThenInclude(a => a.Facet)
                        .Include(a => a.role)
                        .Where(a => a.club.Name == userClub && a.club.Facet.Id == federationId && a.club.Province.Id == userProvince.Id && a.role.Description == "D1").Select(a => a.Id).FirstOrDefault();
                }


                newMessage.Message = message.Message;
                newMessage.CreationDate = DateTime.Now;
                newMessage.Status = message.Status;
                newMessage.CreatorUserProfileId = profileId;
                newMessage.ApproverRequired = approverId;

                context.Messages.Add(newMessage);
                context.SaveChanges();

                //Get Newly created message
                newMessage = context.Messages.Where(a => a.Message == newMessage.Message).OrderByDescending(a=>a.Id).FirstOrDefault();

                //Add emssages to receipients
                foreach (var item in recipients) {
                    context.MessageReceivers.Add(new MessageReceivers{
                     AssignedUserProfileId = item.Id,
                     MessagesFKId = newMessage.Id
                    });
                }
                context.SaveChanges();
            }
        }

        public void updateDocument(UploadDocumentMessageDTO T)
        {
            var document = context.Documents.Where(a => a.Id == T.documentId).FirstOrDefault();
            document.Title = T.title;
            document.Note = T.note;
            context.SaveChanges();
        }

        public async Task<int> uploadDocumentAsync(IFormFile document, int profileId, int sendTo)
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

            // Send to all in same role
            if (sendTo == 0)
            {
                foreach (var profile in getProfilesInSameRole(currentProfile))
                {
                    context.DocumentMessages.Add(new DocumentMessage()
                    {
                        Document = documentToAdd,
                        Recipient = profile
                    });
                }
            }
            // Send to all in lower roles
            else if (sendTo == 1)
            {
                foreach (var profile in getProfilesInLowerRoles(currentProfile))
                {
                    context.DocumentMessages.Add(new DocumentMessage()
                    {
                        Document = documentToAdd,
                        Recipient = profile
                    });
                }
            }
            context.SaveChanges();
            return documentToAdd.Id;
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

        public void aproveDocumentMessage(int id)
        {
            var documentMessage = context.DocumentMessages.Include(a => a.Document).Where(a => a.Document.Id == id).FirstOrDefault();
            documentMessage.Document.AprovalDate = DateTime.Now;
            documentMessage.Document.Aproved = true;
            context.SaveChanges();
        }

        public void declineDocumentMessage(int id)
        {
            var documentMessage = context.DocumentMessages.Include(a => a.Document).Where(a => a.Document.Id == id).FirstOrDefault();
            var document = documentMessage.Document;
            context.Remove(document);
            context.Remove(documentMessage);
        }

        public List<UserProfile> getProfilesInSameRole(UserProfile profile)
        {
            var userClub = profile.club.Name;
            var userRole = profile.role.Description;

            if (new string[] { "D0", "D1", "D2", "D3" }.Contains(userRole))
            {
                var userProvince = context.UserProfiles.Include(a => a.club).ThenInclude(a => a.Province).ThenInclude(a => a.Facets)
                    .Where(a => a.Id == profile.Id).FirstOrDefault().club.Province;
                var userFacet = context.UserProfiles.Include(a => a.club).ThenInclude(a => a.Facet)
                    .Where(a => a.Id == profile.Id).FirstOrDefault().club.Facet;

                return context.UserProfiles.Include(a => a.club).ThenInclude(a => a.Facet).ThenInclude(a => a.Provinces).Include(a => a.role)
                    .Where(a => a.club.Name == userClub && a.club.Facet.Id == userFacet.Id && a.club.Province.Id == userProvince.Id && (
                a.role.Description == "D0" ||
                a.role.Description == "D1" ||
                a.role.Description == "D2" ||
                a.role.Description == "D3"
                )).ToList();
            }
            else if (new string[] { "C0", "C1", "C2", "C3" }.Contains(userRole))
            {
                var userProvince = context.UserProfiles.Include(a => a.club).ThenInclude(a => a.Province).ThenInclude(a => a.Facets)
                    .Where(a => a.Id == profile.Id).FirstOrDefault().club.Province;
                var userFacet = context.UserProfiles.Include(a => a.club).ThenInclude(a => a.Facet)
                    .Where(a => a.Id == profile.Id).FirstOrDefault().club.Facet;

                return context.UserProfiles.Include(a => a.club).ThenInclude(a => a.Province).Include(a => a.role).Where(a => a.club.Province.Id == userProvince.Id &&
                a.club.Facet.Id == userFacet.Id && (
                a.role.Description == "C0" ||
                a.role.Description == "C1" ||
                a.role.Description == "C2" ||
                a.role.Description == "C3"
                )).ToList();
            }
            else if (new string[] { "B0", "B1", "B2", "B3" }.Contains(userRole))
            {
                var userFacet = context.UserProfiles.Include(a => a.club).ThenInclude(a => a.Facet)
                    .Where(a => a.Id == profile.Id).FirstOrDefault().club.Facet;

                return context.UserProfiles.Include(a => a.club).ThenInclude(a => a.Facet).Include(a => a.role).Where(a => a.club.Facet.Id == userFacet.Id && (
                a.role.Description == "B0" ||
                a.role.Description == "B1" ||
                a.role.Description == "B2" ||
                a.role.Description == "B3"
                )).ToList();
            }
            else if (new string[] { "A0", "A1", "A2", "A3" }.Contains(userRole))
            {
                return context.UserProfiles.Include(a => a.role).Where(a =>
                a.role.Description == "A0" ||
                a.role.Description == "A1" ||
                a.role.Description == "A2" ||
                a.role.Description == "A3"
                ).ToList();
            }
            return null;
        }

        public List<UserProfile> getProfilesInLowerRoles(UserProfile profile)
        {
            var userClub = profile.club.Name;
            var userRole = profile.role.Description;

            if (new string[] { "D0", "D1", "D2", "D3" }.Contains(userRole))
            {
                var userFacet = context.UserProfiles.Include(a => a.club).ThenInclude(a => a.Facet)
                    .Where(a => a.Id == profile.Id).FirstOrDefault().club.Facet;

                return context.UserProfiles.Include(a => a.role).Where(a => a.club.Facet.Id == userFacet.Id && a.club.Name == userClub &&
                (a.role.Description == "E0" ||
                a.role.Description == "E1")
                ).ToList();
            }
            else if (new string[] { "C0", "C1", "C2", "C3" }.Contains(userRole))
            {
                var userProvince = context.UserProfiles.Include(a => a.club).ThenInclude(a => a.Province)
                    .Where(a => a.Id == profile.Id).FirstOrDefault().club.Province;

                var userFacet = context.UserProfiles.Include(a => a.club).ThenInclude(a => a.Facet)
                    .Where(a => a.Id == profile.Id).FirstOrDefault().club.Facet;

                return context.UserProfiles.Include(a => a.club).ThenInclude(a => a.Province).Include(a => a.role).Where(a => a.club.Province.Id == userProvince.Id &&
                a.club.Facet.Id == userFacet.Id && (
                a.role.Description != "C0" &&
                a.role.Description != "C1" &&
                a.role.Description != "C2" &&
                a.role.Description != "C3" &&
                a.role.Description != "B0" &&
                a.role.Description != "B1" &&
                a.role.Description != "B2" &&
                a.role.Description != "B3" &&
                a.role.Description != "A0" &&
                a.role.Description != "A1" &&
                a.role.Description != "A2" &&
                a.role.Description != "A3"
                )).ToList();
            }
            else if (new string[] { "B0", "B1", "B2", "B3" }.Contains(userRole))
            {
                var userFacet = context.UserProfiles.Include(a => a.club).ThenInclude(a => a.Facet)
                    .Where(a => a.Id == profile.Id).FirstOrDefault().club.Facet;

                return context.UserProfiles.Include(a => a.club).ThenInclude(a => a.Province).Include(a => a.role).Where(a => a.club.Facet.Id == userFacet.Id && (
                a.role.Description != "B0" &&
                a.role.Description != "B1" &&
                a.role.Description != "B2" &&
                a.role.Description != "B3" &&
                a.role.Description != "A0" &&
                a.role.Description != "A1" &&
                a.role.Description != "A2" &&
                a.role.Description != "A3"
                )).ToList();
            }
            else if (new string[] { "A0", "A1", "A2", "A3" }.Contains(userRole))
            {
                return context.UserProfiles.Include(a => a.role).Where(a =>
                a.role.Description != "A0" &&
                a.role.Description != "A1" &&
                a.role.Description != "A2" &&
                a.role.Description != "A3"
                ).ToList();
            }
            return null;
        }

        public List<UserProfile> getProfilesInLowerRolesInSameFacet(UserProfile profile)
        {
            var userFacet = context.UserProfiles.Include(a => a.club).ThenInclude(a => a.Facet)
                    .Where(a => a.Id == profile.Id).FirstOrDefault().club.Facet;
            var userRole = profile.role.Description;
            if (new string[] { "A0", "A1", "A2", "A3", "A4" }.Contains(userRole))
            {
                return context.UserProfiles.Include(a => a.role).Where(a => a.club.Facet.Id == userFacet.Id &&
                (a.role.Description != "A0" &&
                a.role.Description != "A1" &&
                a.role.Description != "A2" &&
                a.role.Description != "A3" &&
                a.role.Description != "A4")
                ).ToList();
            }
            return null;
        }
    }
}
