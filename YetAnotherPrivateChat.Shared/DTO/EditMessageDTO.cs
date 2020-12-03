using System;
using System.Collections;
using YetAnotherPrivateChat.Shared;
using YetAnotherPrivateChat.Shared.HelperShared;
using System.ComponentModel.DataAnnotations.Schema;

namespace YetAnotherPrivateChat.Shared.DTO
{
    public class EditMessageDTO
    {
        public int MessageId { get; set; }
        public string NewMessageText { get; set; }
        public EditMessageDTO(int messageId, string newMessageText)
        {
            MessageId = messageId;
            NewMessageText = newMessageText;
        }
    }
}
