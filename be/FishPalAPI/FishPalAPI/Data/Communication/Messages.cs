using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FishPalAPI.Data.Communication
{
    public class Messages
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime? CreationDate { get; set; }

        public int Status { get; set; }
        public int InboxOutbox { get; set; }
        public Guid CreatoruserId { get; set; }
        public DateTime? StatusChangeDate { get; set; }
        public Guid? ApproverRequired { get; set; }
        public List<MessageReceivers> AssignedUsers { get; set; }
    }
}
