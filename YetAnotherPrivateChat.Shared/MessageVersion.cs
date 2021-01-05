using System;
using System.Collections;
using System.Collections.Generic;
using YetAnotherPrivateChat.Shared.HelperShared;
using System.ComponentModel.DataAnnotations.Schema;
using YetAnotherPrivateChat.Shared.DTO;

namespace YetAnotherPrivateChat.Shared
{
    public class MessageVersion
    {
        public int MessageVersionId { get; set; }
        public string MessageText { get; set; }
        public DateTime MessageTime { get; set; }
        public int MessageId {get;set;}
        public Message Message {get;set;}
        public MessageVersion(int messageId, string messageText)
        {
            MessageText = messageText;
            MessageTime = DateTime.Now;
            MessageId = messageId;
        }
    }
}