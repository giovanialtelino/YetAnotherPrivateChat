using System;
using System.Collections;
using System.Collections.Generic;
using YetAnotherPrivateChat.Shared.HelperShared;
using System.ComponentModel.DataAnnotations.Schema;

namespace YetAnotherPrivateChat.Shared
{
    public class Star
    {
        public int StarID { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public Message Message { get; set; }
        public int MessageId { get; set; }
        public Star(int userId, int messageId)
        {
            UserId = userId;
            MessageId = messageId;
        }
    }
}
