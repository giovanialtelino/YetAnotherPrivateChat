using System;
using System.Collections;
using YetAnotherPrivateChat.Shared.MessageClass;

namespace YetAnotherPrivateChat.Shared.ReferenceMessageClass
{
    public class Reply
    {
        private int ReplyId { get; set; }
        private Message Original { get; set; }
        private int OriginalId { get; set; }
        private Message Answer { get; set; }
        private int AnswerId { get; set; }
        public Reply(int answerId, int originalId)
        {
            AnswerId = answerId;
            OriginalId = originalId;
        }
    }
}
