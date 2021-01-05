using System;
using System.Collections;
using System.Collections.Generic;
using YetAnotherPrivateChat.Shared.HelperShared;
using System.ComponentModel.DataAnnotations.Schema;
using YetAnotherPrivateChat.Shared.DTO;
using System.Linq;

namespace YetAnotherPrivateChat.Shared
{
    public class Message
    {
        public int MessageId { get; set; }
        public DateTime OriginalTime { get; set; }
        public int UserId { get; set; }
        public List<Star> Stars { get; set; }
        public List<MessageVersion> MessagesVersions { get; set; }
        public Room Room { get; set; }
        public int RoomId { get; set; }
        public Message(int userId)
        {
            OriginalTime = DateTime.Now;
            UserId = userId;
            MessagesVersions = new List<MessageVersion>();
        }

        public Message(NewMessageDTO dto, int jwtOwner)
        {
            OriginalTime = DateTime.Now;
            var msg = new Helper().AprrovedMessage(dto.MessageText);
            if (!msg.Item1) throw new Exception(msg.Item2);
            UserId = jwtOwner;
            RoomId = dto.RoomId;
            MessagesVersions = new List<MessageVersion>();
        }

        public void AddMessageVersion(MessageVersion msgV)
        {
            this.MessagesVersions.Add(msgV);
        }
    }
}
