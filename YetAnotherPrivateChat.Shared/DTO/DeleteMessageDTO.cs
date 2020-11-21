using System;
using System.Collections;
using YetAnotherPrivateChat.Shared;
using YetAnotherPrivateChat.Shared.HelperShared;
using System.ComponentModel.DataAnnotations.Schema;

namespace YetAnotherPrivateChat.Shared.DTO
{
    public class DeleteMessageDTO
    {
        public int MessageId { get; set; }
        public DeleteMessageDTO(int messageId)
        {
            MessageId = messageId;
        }
    }
}
