using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using YetAnotherPrivateChat.Shared;
using YetAnotherPrivateChat.Shared.HelperShared;
using YetAnotherPrivateChat.Change.Context;
using Microsoft.EntityFrameworkCore;
using YetAnotherPrivateChat.Shared.HelperShared.JWT;
using YetAnotherPrivateChat.Shared.DTO;

namespace YetAnotherPrivateChat.Change.Service
{
    public class EditMessage
    {
        //It's not really a edit, just create a new version, with new time and new text
        public async Task<Message> Edit(EditMessageDTO editMessageDTO, int jwtOwner, MyDbContext _context)
        {
            var message = await _context.Messages.Include(c => c.User).FirstOrDefaultAsync(c => c.MessageId == editMessageDTO.MessageId);

            if (message == null) throw new Exception("Message does not exist, please contact the chat administrator");

            if (message.UserId != jwtOwner) throw new Exception("You don't own this message, you cannot modify it.");

            var newVersion = new Message(message);

            _context.Messages.Add(newVersion);
            await _context.SaveChangesAsync();

            return newVersion;
        }
    }

}