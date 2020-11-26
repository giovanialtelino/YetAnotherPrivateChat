using System;
using System.Collections;
using YetAnotherPrivateChat.Shared;
using YetAnotherPrivateChat.Shared.HelperShared;
using System.ComponentModel.DataAnnotations.Schema;

namespace YetAnotherPrivateChat.Shared.DTO
{
    public class NewMessageDTO
    {
        public string MessageText { get; set; }
        public int UserId { get; set; }
        public int? ReplyId { get; set; }
        public int  RoomId {get;set;}
        public NewMessageDTO(){}
        public NewMessageDTO(string messageText, int userId, int roomId)
        {
            MessageText = messageText;
            UserId = userId;
            RoomId = roomId;
        }
        public NewMessageDTO(string messageText, int userId, int replyId, int roomId)
        {
            MessageText = messageText;
            UserId = userId;
            ReplyId = replyId;
            RoomId = roomId;
        }
    }
}
