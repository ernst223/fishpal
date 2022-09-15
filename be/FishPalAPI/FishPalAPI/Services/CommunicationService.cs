using FishPalAPI.Data;
using FishPalAPI.Data.Communication;
using FishPalAPI.Models;
using FishPalAPI.Models.MessagesModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            List<Federation> _federations=  null;
            var user = context.Users.Include(y => y.role).Include(x => x.federations).FirstOrDefault(x => x.Email == userEmail);
        
            if (user.role.Id == 1 || user.role.Id == 2) { //if your in a sasacc role the return all the distinct federations that are avaialble
                _federations = context.Federation.Distinct().ToList();
            }

            if (user.role.Id == 5 || user.role.Id == 6) //if your role is that of a federation such as saalaa then return sasacc and your own federation
            {
                _federations = context.Federation.Where(x => x.Name == "SASACC" || x.Name == user.federations[0].Name).ToList();
            }

            if (user.role.Id == 9 || user.role.Id == 10) //if your role is that of a province then return only the federation to which you are afiliated
            {
                _federations = context.Federation.Where(x => x.Name == user.federations[0].Name).ToList();
            }

            if (user.role.Id != 1 && user.role.Id != 2 && user.role.Id != 5 && user.role.Id != 6 && user.role.Id != 9 && user.role.Id != 10) //if your role is for a club or a member then return nothing, this will be displayed as "default" on the front end
            {
                _federations = context.Federation.Where(x => x.Name == "NothingGetsReturned").ToList();
            }

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

        public List<FederationDTO> getAllProvinces(string userEmail, FederationDTO[] selectedFacets)
        {
            //get loggedin user credentials
            //if() logged in user = role a getall provinces contained withing all selected federations
            //if() logged in user = role b, c, d return own province name
            List<Province> _provinces = null;

            var user = context.Users.Include(y => y.role).Include(x => x.federations).FirstOrDefault(x => x.Email == userEmail);

            if (user.role.Id == 1 || user.role.Id == 2)
             { //if your in a sasacc role the return all the distinct federations that are avaialble
                 _provinces = context.Provinces.Include(x => x.Facets).Distinct().ToList();//.Where(x => selectedFacets.Contains(x.Facets))
            }

             if (user.role.Id == 5 || user.role.Id == 6) //if your role is that of a federation such as saalaa then return sasacc and your own federation
             {
                 //_provinces = context.Federation.Where(x => x.Name == "SASACC" || x.Name == user.federations[0].Name).ToList();
             }

             if (user.role.Id == 9 || user.role.Id == 10) //if your role is that of a province then return only the federation to which you are afiliated
             {
                // _provinces = context.Federation.Where(x => x.Name == user.federations[0].Name).ToList();
             }

             if (user.role.Id != 1 && user.role.Id != 2 && user.role.Id != 5 && user.role.Id != 6 && user.role.Id != 9 && user.role.Id != 10) //if your role is for a club or a member then return nothing, this will be displayed as "default" on the front end
             {
                 //_provinces = context.Federation.Where(x => x.Name == "NothingGetsReturned").ToList();
             }

            List<FederationDTO> result = new List<FederationDTO>();
            foreach (var entry in _provinces)
            {
                result.Add(new FederationDTO()
                {
                    Id = entry.Id,
                    Name = entry.Name
                });
            }
            return result;
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

    }
}
