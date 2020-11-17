using System;
using System.Collections;
using YetAnotherPrivateChat.Shared.MessageClass;
using System.ComponentModel.DataAnnotations.Schema;

namespace YetAnotherPrivateChat.Shared.ReferenceMessageClass
{
    public class Reply
    {
        public int ReplyId { get; set; }
        [NotMapped]
        public Message Original { get; set; }
        public int? OriginalId { get; set; }
        [NotMapped]
        public Message Answer { get; set; }
        public int? AnswerId { get; set; }
        public Reply() { }
        public Reply(int answerId, int originalId)
        {
            AnswerId = answerId;
            OriginalId = originalId;
        }
    }
}
