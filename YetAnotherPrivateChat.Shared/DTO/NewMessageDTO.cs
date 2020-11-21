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
        public NewMessageDTO(string messageText, int userId)
        {
            MessageText = messageText;
            UserId = userId;
        }
        public NewMessageDTO(string messageText, int userId, int replyId)
        {
            MessageText = messageText;
            UserId = userId;
            ReplyId = replyId;
        }
    }
}
