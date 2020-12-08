using System;
using System.Collections;
using System.Collections.Generic;
using YetAnotherPrivateChat.Shared.HelperShared;
using System.ComponentModel.DataAnnotations.Schema;
using YetAnotherPrivateChat.Shared.DTO;

namespace YetAnotherPrivateChat.Shared
{
    public class Message
    {
        public int MessageId { get; set; }
        public string MessageText { get; set; }
        public DateTime OriginalTime { get; set; }
        public DateTime MessageTime { get; set; }
        public int UserId { get; set; }
        [NotMapped]
        public Reply Reply { get; set; }
        public int? ReplyId { get; set; }
        public int Version { get; set; }
        public List<Quote> Quotes { get; set; }
        public List<Star> Stars { get; set; }
        public Room Room { get; set; }
        public int RoomId { get; set; }
        public Message(string messageText, int userId)
        {
            var msg = new Helper().AprrovedMessage(messageText);
            if (!msg.Item1) throw new Exception(msg.Item2);
            MessageText = messageText;
            MessageTime = DateTime.Now;
            OriginalTime = DateTime.Now;
            UserId = userId;
            Version = 0;
        }

        public Message(string messageText, int userId, int replyId)
        {
            var msg = new Helper().AprrovedMessage(messageText);
            if (!msg.Item1) throw new Exception(msg.Item2);

            MessageText = messageText;
            MessageTime = DateTime.Now;
            OriginalTime = DateTime.Now;
            UserId = userId;
            ReplyId = replyId;
            Reply = new Reply(this.MessageId, replyId);
            Version = 0;
        }
        public Message(NewMessageDTO dto, int jwtOwner)
        {
            var msg = new Helper().AprrovedMessage(dto.MessageText);
            if (!msg.Item1) throw new Exception(msg.Item2);

            MessageText = dto.MessageText;
            MessageTime = DateTime.Now;
            UserId = jwtOwner;
            ReplyId = dto.ReplyId;
            RoomId = dto.RoomId;
            Version = 0;
        }

        public Message(Message msg)
        {
            MessageText = "----";
            MessageTime = DateTime.Now;
            OriginalTime = msg.OriginalTime;
            UserId = msg.UserId;
            ReplyId = msg.ReplyId;
            Version = msg.Version++;
            RoomId = msg.RoomId;
        }

        public Message(Message msg, string text)
        {
            MessageText = text;
            OriginalTime = msg.OriginalTime;
            MessageTime = DateTime.Now;
            UserId = msg.UserId;
            ReplyId = msg.ReplyId;
            Version = msg.Version++;
            RoomId = msg.RoomId;
        }
    }
}
