using System;

namespace FishPalAPI.Models.MessagesModels
{
    public class MessageReceiversDTO
    {
        public int Id { get; set; }
        public Guid AssignedUserId { get; set; }
    }
}
