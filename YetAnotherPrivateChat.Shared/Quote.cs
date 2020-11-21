using System;
using System.Collections;
using System.ComponentModel.DataAnnotations.Schema;

namespace YetAnotherPrivateChat.Shared
{
    public class Quote
    {
        public int QuoteId { get; set; }
        [NotMapped]
        public Message Message { get; set; }
        public int MessageId { get; set; }
        public User User {get;set;}
        public int UserId {get;set;}
        public Quote() { }
        public Quote(int messageId, int userId)
        {
            MessageId = messageId;
            UserId = userId;
        }
    }
}
