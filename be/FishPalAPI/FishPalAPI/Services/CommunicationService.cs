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

        public List<MessageDTO> getAllMessages(int messageTypeToReturn, string userEmail)
        {
            var user = context.Users.FirstOrDefault(x => x.Email == userEmail);

            if (messageTypeToReturn == 0)//inbox
            {
                var _messagesReceivers = context.MessageReceivers.Where(x => x.AssignedUserId.ToString() == user.Id).Include(x => x.Messages).Where(y => y.Messages.Status == 1).ToList();

                List<MessageDTO> inboxResult = new List<MessageDTO>();
                foreach (var entry in _messagesReceivers)
                {
                    inboxResult.Add(new MessageDTO()
                    {
                        Id = entry.Messages.Id,
                        Message = entry.Messages.Message,
                        CreationDate = entry.Messages.CreationDate,
                        Status = entry.Messages.Status,
                        InboxOutbox = entry.Messages.InboxOutbox,
                        CreatoruserId = entry.Messages.CreatoruserId,
                        StatusChangeDate = entry.Messages.StatusChangeDate,
                        ApproverRequired = entry.Messages.ApproverRequired
                    });
                }
                return inboxResult;

            }

            List<Messages> _messages;

            if (messageTypeToReturn == 1)//outbox   
            {
                 _messages = context.Messages.Where(x => x.CreatoruserId.ToString() == user.Id).ToList();

                    List<MessageDTO> outboxResult = new List<MessageDTO>();
                    foreach (var entry in _messages)
                    {
                    outboxResult.Add(new MessageDTO()
                        {
                            Id = entry.Id,
                            Message = entry.Message,
                            CreationDate = entry.CreationDate,
                            Status = entry.Status,
                            InboxOutbox = entry.InboxOutbox,
                            CreatoruserId = entry.CreatoruserId,
                            StatusChangeDate = entry.StatusChangeDate,
                            ApproverRequired = entry.ApproverRequired
                        });
                    }
                return outboxResult;
            }

            if (messageTypeToReturn == 2)//pending/to be approved 
            {
                 _messages = context.Messages.Where(x => x.ApproverRequired.ToString() == user.Id && x.Status == 0).ToList(); 
                    List<MessageDTO> pendingResult = new List<MessageDTO>();
                    foreach (var entry in _messages)
                    {
                    pendingResult.Add(new MessageDTO()
                        {
                            Id = entry.Id,
                            Message = entry.Message,
                            CreationDate = entry.CreationDate,
                            Status = entry.Status,
                            InboxOutbox = entry.InboxOutbox,
                            CreatoruserId = entry.CreatoruserId,
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
                    AssignedUserId = entry.AssignedUserId
                });
            }
            return result;
        }

        public bool approveDeclineMessages(Messages T)
        {
            try
            {
                context.Messages.Update(T);
                context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public List<FederationDTO> getAllFederations(string userEmail)
        {
            //List<Federation> _federations=  null;
            //var user = context.Users.Include(y => y.role).Include(x => x.federations).FirstOrDefault(x => x.Email == userEmail);

            //if (user.role.Id == 1 || user.role.Id == 2) { //if your in a sasacc role the return all the distinct federations that are avaialble
            //    _federations = context.Federation.Distinct().ToList();
            //}

            //if (user.role.Id == 5 || user.role.Id == 6) //if your role is that of a federation such as saalaa then return sasacc and your own federation
            //{
            //    _federations = context.Federation.Where(x => x.Name == "SASACC" || x.Name == user.federations[0].Name).ToList();
            //}

            //if (user.role.Id == 9 || user.role.Id == 10) //if your role is that of a province then return only the federation to which you are afiliated
            //{
            //    _federations = context.Federation.Where(x => x.Name == user.federations[0].Name).ToList();
            //}

            //if (user.role.Id != 1 && user.role.Id != 2 && user.role.Id != 5 && user.role.Id != 6 && user.role.Id != 9 && user.role.Id != 10) //if your role is for a club or a member then return nothing, this will be displayed as "default" on the front end
            //{
            //    _federations = context.Federation.Where(x => x.Name == "NothingGetsReturned").ToList();
            //}

            //List<FederationDTO> result = new List<FederationDTO>();
            //foreach (var entry in _federations)
            //{
            //    result.Add(new FederationDTO()
            //    {
            //        Id = entry.Id,
            //        Name = entry.Name
            //    });
            //}
            //return result;
            return null;
        }

        public List<FederationDTO> getAllProvinces(string userEmail, FederationDTO[] selectedFacets)
        {
            ////get loggedin user credentials
            ////if() logged in user = role a getall provinces contained withing all selected federations
            ////if() logged in user = role b, c, d return own province name
            //List<Province> _provinces = null;

            //var user = context.Users.Include(y => y.role).Include(x => x.federations).FirstOrDefault(x => x.Email == userEmail);

            //if (user.role.Id == 1 || user.role.Id == 2)
            // { //if your in a sasacc role the return all the distinct federations that are avaialble
            //     _provinces = context.Provinces.Include(x => x.Facets).Distinct().ToList();//.Where(x => selectedFacets.Contains(x.Facets))
            //}

            // if (user.role.Id == 5 || user.role.Id == 6) //if your role is that of a federation such as saalaa then return sasacc and your own federation
            // {
            //     //_provinces = context.Federation.Where(x => x.Name == "SASACC" || x.Name == user.federations[0].Name).ToList();
            // }

            // if (user.role.Id == 9 || user.role.Id == 10) //if your role is that of a province then return only the federation to which you are afiliated
            // {
            //    // _provinces = context.Federation.Where(x => x.Name == user.federations[0].Name).ToList();
            // }

            // if (user.role.Id != 1 && user.role.Id != 2 && user.role.Id != 5 && user.role.Id != 6 && user.role.Id != 9 && user.role.Id != 10) //if your role is for a club or a member then return nothing, this will be displayed as "default" on the front end
            // {
            //     //_provinces = context.Federation.Where(x => x.Name == "NothingGetsReturned").ToList();
            // }

            //List<FederationDTO> result = new List<FederationDTO>();
            //foreach (var entry in _provinces)
            //{
            //    result.Add(new FederationDTO()
            //    {
            //        Id = entry.Id,
            //        Name = entry.Name
            //    });
            //}
            //return result;
            return null;
        }

        public List<FederationDTO> getAllClubs(string userEmail)
        {
            //get loggedin user credentials
            //if() logged in user = role a getall clubs contained withing all selected provinces
            //if() logged in user = role b return default
            //if() logged in user = role c return clubs in own province
            //if() logged in user = role d return own club

            List<Federation> _federations = context.Federation.ToList();
            List<FederationDTO> result = new List<FederationDTO>();
            foreach (var entry in _federations)
            {
                result.Add(new FederationDTO()
                {
                    Id = entry.Id,
                    Name = entry.Name
                });
            }
            return result;
        }

        public List<FederationDTO> getAllPeople(string userEmail)
        {
            //get loggedin user credentials
            /*if() logged in user = role a 
             * select facet - getall where federation = all selected federations
             * select province - getall where province = selected provinces || selected federations
             * selected clubs - getall where clubs = selcted clubs || selected provinces || selected federations
             * */

            /*if() logged in user = role b return default
             * select facet - get secretary && chair where facet = sasacc && getall where federation = own federation
             * selected province - getall where province = selected provinces || get secretary && chair where facet = sasacc && getall where federation = own federation
             */

            /*if() logged in user = role c return return default
             * selected facet = get secretary && chair of own facet
             * selected province = getall where province = ownProvince || get secretary && chair of own facet
             * selected club = get secretary && chair from all clubs in own province
             */

            /*if() logged in user = role d return own club
             * selected province = 
             */

            List<Federation> _federations = context.Federation.ToList();
            List<FederationDTO> result = new List<FederationDTO>();
            foreach (var entry in _federations)
            {
                result.Add(new FederationDTO()
                {
                    Id = entry.Id,
                    Name = entry.Name
                });
            }
            return result;
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

            if (new string[] { "D0", "D1", "D2", "D3", "D4" }.Contains(userRole))
            {
                return context.UserProfiles.Include(a => a.club).Include(a => a.role).Where(a => a.club.Name == userClub && (
                a.role.Description == "D0" ||
                a.role.Description == "D1" ||
                a.role.Description == "D2" ||
                a.role.Description == "D3" ||
                a.role.Description == "D4"
                )).ToList();
            }
            else if (new string[] { "C0", "C1", "C2", "C3", "C4" }.Contains(userRole))
            {
                var userProvince = context.UserProfiles.Include(a => a.club).ThenInclude(a => a.Province)
                    .Where(a => a.Id == profile.Id).FirstOrDefault().club.Province;

                return context.UserProfiles.Include(a => a.club).ThenInclude(a => a.Province).Include(a => a.role).Where(a => a.club.Province.Id == userProvince.Id && (
                a.role.Description == "C0" ||
                a.role.Description == "C1" ||
                a.role.Description == "C2" ||
                a.role.Description == "C3" ||
                a.role.Description == "C4"
                )).ToList();
            }
            else if (new string[] { "B0", "B1", "B2", "B3", "B4" }.Contains(userRole))
            {
                var userFacet = context.UserProfiles.Include(a => a.club).ThenInclude(a => a.Facet)
                    .Where(a => a.Id == profile.Id).FirstOrDefault().club.Facet;

                return context.UserProfiles.Include(a => a.club).ThenInclude(a => a.Facet).Include(a => a.role).Where(a => a.club.Facet.Id == userFacet.Id && (
                a.role.Description == "B0" ||
                a.role.Description == "B1" ||
                a.role.Description == "B2" ||
                a.role.Description == "B3" ||
                a.role.Description == "B4"
                )).ToList();
            }
            else if (new string[] { "A0", "A1", "A2", "A3", "A4" }.Contains(userRole))
            {
                return context.UserProfiles.Include(a => a.role).Where(a =>
                a.role.Description == "A0" ||
                a.role.Description == "A1" ||
                a.role.Description == "A2" ||
                a.role.Description == "A3" ||
                a.role.Description == "A4"
                ).ToList();
            }
            return null;
        }

        public List<UserProfile> getProfilesInLowerRoles(UserProfile profile)
        {
            var userClub = profile.club.Name;
            var userRole = profile.role.Description;

            if (new string[] { "D0", "D1", "D2", "D3", "D4" }.Contains(userRole))
            {
                return context.UserProfiles.Include(a => a.role).Where(a => 
                a.role.Description == "E0" ||
                a.role.Description == "E1" ||
                a.role.Description == "E2" ||
                a.role.Description == "E3" ||
                a.role.Description == "E4"
                ).ToList();
            }
            else if (new string[] { "C0", "C1", "C2", "C3", "C4" }.Contains(userRole))
            {
                var userProvince = context.UserProfiles.Include(a => a.club).ThenInclude(a => a.Province)
                    .Where(a => a.Id == profile.Id).FirstOrDefault().club.Province;

                return context.UserProfiles.Include(a => a.club).ThenInclude(a => a.Province).Include(a => a.role).Where(a => a.club.Province.Id == userProvince.Id && (
                a.role.Description != "C0" &&
                a.role.Description != "C1" &&
                a.role.Description != "C2" &&
                a.role.Description != "C3" &&
                a.role.Description != "C4" &&
                a.role.Description != "B0" &&
                a.role.Description != "B1" &&
                a.role.Description != "B2" &&
                a.role.Description != "B3" &&
                a.role.Description != "B4" &&
                a.role.Description != "A0" &&
                a.role.Description != "A1" &&
                a.role.Description != "A2" &&
                a.role.Description != "A3" &&
                a.role.Description != "A4"
                )).ToList();
            }
            else if (new string[] { "B0", "B1", "B2", "B3", "B4" }.Contains(userRole))
            {
                var userFacet = context.UserProfiles.Include(a => a.club).ThenInclude(a => a.Facet)
                    .Where(a => a.Id == profile.Id).FirstOrDefault().club.Facet;

                return context.UserProfiles.Include(a => a.club).ThenInclude(a => a.Province).Include(a => a.role).Where(a => a.club.Facet.Id == userFacet.Id && (
                a.role.Description != "B0" &&
                a.role.Description != "B1" &&
                a.role.Description != "B2" &&
                a.role.Description != "B3" &&
                a.role.Description != "B4" &&
                a.role.Description != "A0" &&
                a.role.Description != "A1" &&
                a.role.Description != "A2" &&
                a.role.Description != "A3" &&
                a.role.Description != "A4"
                )).ToList();
            }
            else if (new string[] { "A0", "A1", "A2", "A3", "A4" }.Contains(userRole))
            {
                return context.UserProfiles.Include(a => a.role).Where(a =>
                a.role.Description != "A0" &&
                a.role.Description != "A1" &&
                a.role.Description != "A2" &&
                a.role.Description != "A3" &&
                a.role.Description != "A4"
                ).ToList();
            }
            return null;
        }
    }
}
