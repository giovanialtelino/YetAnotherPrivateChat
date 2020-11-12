using System;
using System.Collections;
using YetAnotherPrivateChat.Shared.UserClass;
using YetAnotherPrivateChat.Shared.ReferenceMessageClass;
using YetAnotherPrivateChat.Shared.HelperShared;

namespace YetAnotherPrivateChat.Shared.MessageClass
{
    public class Message
    {
        private int MessageId { get; set; }
        private string MessageText { get; set; }
        private DateTime MessageTime { get; set; }
        private int UserId { get; set; }
        private User User { get; set; }
        private Reply Reply { get; set; }
        private int? ReplyId { get; set; }

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
