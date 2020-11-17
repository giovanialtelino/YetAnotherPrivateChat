using System;
using System.Collections;
using YetAnotherPrivateChat.Shared.UserClass;
using YetAnotherPrivateChat.Shared.ReferenceMessageClass;
using YetAnotherPrivateChat.Shared.HelperShared;
using System.ComponentModel.DataAnnotations.Schema;

namespace YetAnotherPrivateChat.Shared.MessageClass
{
    public class Message
    {
        public int MessageId { get; set; }
        public string MessageText { get; set; }
        public DateTime MessageTime { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        [NotMapped]
        public Reply Reply { get; set; }
        public int? ReplyId { get; set; }

        public Message(string messageText, int userId)
        {
            var msg = new Helper().AprrovedMessage(messageText);
            if (!msg.Item1) throw new Exception(msg.Item2);
            MessageText = messageText;
            MessageTime = DateTime.Now;
            UserId = userId;
        }

        public Message(string messageText, int userId, int replyId)
        {
            var msg = new Helper().AprrovedMessage(messageText);
            if (!msg.Item1) throw new Exception(msg.Item2);

            MessageText = messageText;
            MessageTime = DateTime.Now;
            UserId = userId;
            ReplyId = replyId;
            Reply = new Reply(this.MessageId, replyId);
        }
    }
}
