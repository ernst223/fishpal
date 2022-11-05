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
        public int CreatorUserProfileId { get; set; }
        public DateTime? StatusChangeDate { get; set; }
        public int? ApproverRequired { get; set; }
        public List<MessageReceivers> AssignedUsers { get; set; }
    }
}
