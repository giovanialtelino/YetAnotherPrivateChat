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
        public int? ReplyId { get; set; }
        public int  RoomId {get;set;}
        public NewMessageDTO(){}
        public NewMessageDTO(string messageText,  int roomId)
        {
            MessageText = messageText;
            RoomId = roomId;
        }
        public NewMessageDTO(string messageText, int userId, int replyId, int roomId)
        {
            MessageText = messageText;
            ReplyId = replyId;
            RoomId = roomId;
        }
    }
}
