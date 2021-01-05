using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using YetAnotherPrivateChat.Shared;
using YetAnotherPrivateChat.Shared.HelperShared;
using YetAnotherPrivateChat.Chat.Context;
using Microsoft.EntityFrameworkCore;
using YetAnotherPrivateChat.Shared.HelperShared.JWT;
using YetAnotherPrivateChat.Shared.DTO;

namespace YetAnotherPrivateChat.Chat.Service
{
    public class EditMessage
    {
        //It's not really a edit, just create a new version, with new time and new text
        public async Task<bool> Edit(EditMessageDTO editMessageDTO, int jwtOwner, MyDbContext _context)
        {
            var valid = Helper.ValidMessageText(editMessageDTO.NewMessageText);
            if (!valid) throw new Exception("Message is invalid");

            var message = await _context.Messages.FirstOrDefaultAsync(c => c.MessageId == editMessageDTO.MessageId);

            if (message == null) throw new Exception("Message does not exist, please contact the chat administrator");

            if (message.UserId != jwtOwner) throw new Exception("You don't own this message, you cannot modify it.");

            message.AddMessageVersion(new MessageVersion(message.MessageId, editMessageDTO.NewMessageText));
            
            await _context.SaveChangesAsync();

            return true;
        }
    }

}