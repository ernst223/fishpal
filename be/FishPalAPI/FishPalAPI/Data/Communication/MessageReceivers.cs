using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FishPalAPI.Data.Communication
{
    public class MessageReceivers
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Guid AssignedUserId { get; set; }

        [ForeignKey("Messages")]
        public int MessagesFKId { get; set; }
        public virtual Messages Messages { get; set; }

    }
}
