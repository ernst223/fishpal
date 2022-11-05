using System;
using System.Collections.Generic;

namespace FishPalAPI.Models.MessagesModels
{
    public class MessageDTO
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime? CreationDate { get; set; }

        public int Status { get; set; }

        public string[] rolesToSendTo { get; set; }
        public int CreatorUserProfileId { get; set; }
        public DateTime? StatusChangeDate { get; set; }
        public int? ApproverRequired { get; set; }
    }
}
