using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using YetAnotherPrivateChat.Shared;
using YetAnotherPrivateChat.Shared.HelperShared;
using YetAnotherPrivateChat.Chat.Context;
using Microsoft.EntityFrameworkCore;
using YetAnotherPrivateChat.Shared.HelperShared.JWT;

namespace YetAnotherPrivateChat.Chat.Service
{
    public class DeleteMessage
    {
        //It's not really a delete, just remove the text, all the other versions can still be seen
        public async Task<bool> Delete(int messageId, int jwtOwner, MyDbContext _context)
        {
            var message = await _context.Messages.FirstOrDefaultAsync(c => c.MessageId == messageId);

            if (message == null) throw new Exception("Message does not exist, please contact the chat administrator");

            if (message.UserId != jwtOwner) throw new Exception("You don't own this message, you cannot modify it.");

            message.AddMessageVersion(new MessageVersion(message.MessageId, "DELETED"));           
            
            await _context.SaveChangesAsync();

            return true;
        }
    }

}