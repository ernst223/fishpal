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
        public int InboxOutbox { get; set; }
        public Guid CreatoruserId { get; set; }
        public DateTime? StatusChangeDate { get; set; }
        public Guid? ApproverRequired { get; set; }
    }
}
