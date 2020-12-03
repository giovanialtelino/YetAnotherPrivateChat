using System;
using System.Collections;
using YetAnotherPrivateChat.Shared;
using YetAnotherPrivateChat.Shared.HelperShared;
using System.ComponentModel.DataAnnotations.Schema;

namespace YetAnotherPrivateChat.Shared.DTO
{
    public class StarMessageDTO
    {
        public int MessageId {get;set;}
        public StarMessageDTO(int messageId)
        {
            MessageId = messageId;
        }
    }
}
