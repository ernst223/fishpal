using FishPalAPI.Data;
using FishPalAPI.Data.Communication;
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
                    var recipients = context.MessageReceivers.Include(a => a.Messages).Where(a => a.MessagesFKId == recordToApproveOrDecline.Id).ToList();
                    foreach(var entity in recipients)
                    {
                        if(entity.Messages.SendEmail == true)
                        {
                            sendEmailMessageToRecipientAsync(entity.Messages, 
                                context.UserProfiles.Where(a => a.Id == entity.AssignedUserProfileId).FirstOrDefault());
                        }
                    }
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

        public void sendMessages(MessageDTO message, int federationId, int profileId, bool sendEmail)
        {

            UserProfile currentProfile = context.UserProfiles.Include(a => a.club).ThenInclude(a => a.Province).Include(a => a.role).Where(a => a.Id == profileId).FirstOrDefault();

            var userClub = currentProfile.club.Name;
            var userRole = currentProfile.role.Description;


            List<UserProfile> recipients = new List<UserProfile>();

            if (new string[] { "D0", "D1" }.Contains(userRole)) //club
            {
                var userProvince = context.UserProfiles.Include(a => a.club).ThenInclude(a => a.Province).ThenInclude(a => a.Facets)
                    .Where(a => a.Id == currentProfile.Id).FirstOrDefault().club.Province;

                if ((message.selectedClubs != null))
                {
                    recipients = context.UserProfiles.Include(a => a.club).ThenInclude(a => a.Facet).ThenInclude(a => a.Provinces).Include(a => a.role)
                    .Where(a => a.club.Name == userClub && a.club.Facet.Id == federationId && a.club.Province.Id == userProvince.Id && (message.rolesToSendTo.Contains(a.role.Description)) 
                    && (message.selectedClubs.Contains(a.club.Id))).ToList();
                }
                else {
                    recipients = context.UserProfiles.Include(a => a.club).ThenInclude(a => a.Facet).ThenInclude(a => a.Provinces).Include(a => a.role)
                       .Where(a => a.club.Name == userClub && a.club.Facet.Id == federationId && a.club.Province.Id == userProvince.Id && (message.rolesToSendTo.Contains(a.role.Description))).ToList();
                }
                    
            }
            else if (new string[] { "C0", "C1" }.Contains(userRole)) //province
            {
                var userProvince = context.UserProfiles.Include(a => a.club).ThenInclude(a => a.Province).ThenInclude(a => a.Facets)
                    .Where(a => a.Id == currentProfile.Id).FirstOrDefault().club.Province;

                if (message.selectedClubs != null) //newly added test
                {
                    recipients = context.UserProfiles.Include(a => a.club).ThenInclude(a => a.Province).Include(a => a.role).Where(a => a.club.Province.Id == userProvince.Id &&
                    a.club.Facet.Id == federationId && (message.rolesToSendTo.Contains(a.role.Description)) 
                    && (message.selectedClubs.Contains(a.club.Id))).ToList();
                } else {
                    recipients = context.UserProfiles.Include(a => a.club).ThenInclude(a => a.Province).Include(a => a.role).Where(a => a.club.Province.Id == userProvince.Id &&
                    a.club.Facet.Id == federationId && (message.rolesToSendTo.Contains(a.role.Description))).ToList();
                }
                
            }
            else if (new string[] { "B0", "B1" }.Contains(userRole)) //federation/facet
            {
                if ((message.selectedClubs != null) && (message.selectedProvince != null))
                {
                    recipients = context.UserProfiles.Include(a => a.club).ThenInclude(a => a.Facet).Include(a => a.role).Where(a => a.club.Facet.Id == federationId && (message.rolesToSendTo.Contains(a.role.Description))
                       && (message.selectedProvince.Contains(a.club.Province.Id)) 
                       && (message.selectedClubs.Contains(a.club.Id))).ToList();
                }
                else if ((message.selectedClubs != null) && (message.selectedProvince == null))
                {
                    recipients = context.UserProfiles.Include(a => a.club).ThenInclude(a => a.Facet).Include(a => a.role).Where(a => a.club.Facet.Id == federationId && (message.rolesToSendTo.Contains(a.role.Description))
                       && (message.selectedClubs.Contains(a.club.Id))).ToList();
                }
                else if ((message.selectedClubs == null) && (message.selectedProvince != null))
                {
                    recipients = context.UserProfiles.Include(a => a.club).ThenInclude(a => a.Facet).Include(a => a.role).Where(a => a.club.Facet.Id == federationId && (message.rolesToSendTo.Contains(a.role.Description))
                       && (message.selectedProvince.Contains(a.club.Province.Id))).ToList();
                }
                else
                {
                    recipients = context.UserProfiles.Include(a => a.club).ThenInclude(a => a.Facet).Include(a => a.role).Where(a => a.club.Facet.Id == federationId && (message.rolesToSendTo.Contains(a.role.Description))).ToList();
                }
                    
            }
            else if (new string[] { "A0", "A1" }.Contains(userRole)) //sasacc
            {
                if ((message.selectedClubs != null) && (message.selectedProvince != null))
                {
                    recipients = context.UserProfiles.Include(a => a.role).Include(a => a.club).ThenInclude(a => a.Facet).Where(a => message.rolesToSendTo.Contains(a.role.Description) && a.club.Facet.Id == federationId 
                    && message.selectedProvince.Contains(a.club.Province.Id) && message.selectedClubs.Contains(a.club.Id)).ToList();
                }
                else if ((message.selectedClubs != null) && (message.selectedProvince == null))
                {
                    recipients = context.UserProfiles.Include(a => a.role).Include(a => a.club).ThenInclude(a => a.Facet).Where(a => message.rolesToSendTo.Contains(a.role.Description) && a.club.Facet.Id == federationId 
                    && message.selectedClubs.Contains(a.club.Id)).ToList();
                }
                else if ((message.selectedClubs == null) && (message.selectedProvince != null))
                {
                    recipients = context.UserProfiles.Include(a => a.role).Include(a => a.club).ThenInclude(a => a.Facet).Where(a => message.rolesToSendTo.Contains(a.role.Description) && a.club.Facet.Id == federationId 
                    && message.selectedProvince.Contains(a.club.Province.Id)).ToList();
                }
                else {
                    recipients = context.UserProfiles.Include(a => a.role).Include(a => a.club).ThenInclude(a => a.Facet).Where(a => message.rolesToSendTo.Contains(a.role.Description) && a.club.Facet.Id == federationId).ToList();
                }              
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
                newMessage.SendEmail = sendEmail;

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
                    if (message.Status == 2 && newMessage.SendEmail == true)
                    {
                        sendEmailMessageToRecipientAsync(newMessage, item);
                    }
                }
                context.SaveChanges();
            }
        }

        public async Task sendEmailMessageToRecipientAsync(Messages messageToSend, UserProfile userProfile)
        {
            var currentUser = context.Users.Include(a => a.profiles).Where(a => a.profiles.Contains(userProfile)).FirstOrDefault();
            if(currentUser != null)
            {
                var message = new TemplatedPostmarkMessage
                {
                    From = "admin@fishpal.co.za",
                    To = currentUser.UserName,
                    TemplateAlias = "NewMessage",
                    TemplateModel = new Dictionary<string, object> {
                    { "name", currentUser.Name },
                    { "message", messageToSend.Message },
                  },
                };

                var client = new PostmarkClient("fc1530d0-ed32-488f-8f40-33fb54be4801");

                var response = await client.SendMessageAsync(message);

                if (response.Status != PostmarkStatus.Success)
                {
                    Console.WriteLine("Response was: " + response.Message);
                }
            }
        }

        public void updateDocument(UploadDocumentMessageDTO T)
        {
            var document = context.Documents.Where(a => a.Id == T.documentId).FirstOrDefault();
            document.Title = T.title;
            document.Note = T.note;
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
    }
}
