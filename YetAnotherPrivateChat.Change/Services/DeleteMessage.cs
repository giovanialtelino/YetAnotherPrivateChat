using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using YetAnotherPrivateChat.Shared;
using YetAnotherPrivateChat.Shared.HelperShared;
using YetAnotherPrivateChat.Change.Context;
using Microsoft.EntityFrameworkCore;
using YetAnotherPrivateChat.Shared.HelperShared.JWT;

namespace YetAnotherPrivateChat.Change.Service
{
    public class DeleteMessage
    {
        //It's not really a delete, just remove the text, all the other versions can still be seen
        public async Task<Message> Delete(int messageId, int jwtOwner, MyDbContext _context)
        {
            var message = await _context.Messages.FirstOrDefaultAsync(c => c.MessageId == messageId);

            if (message == null) throw new Exception("Message does not exist, please contact the chat administrator");

            if (message.UserId != jwtOwner) throw new Exception("You don't own this message, you cannot modify it.");

            var newVersion = new Message(message, "DELETED");

            _context.Messages.Add(newVersion);
            await _context.SaveChangesAsync();

            return newVersion;
        }
    }

}