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
    public class AddMessage
    {
        public async Task<bool> Add(NewMessageDTO newMessage, int jwtOwner, MyDbContext _context)
        {
            var valid = Helper.ValidMessageText(newMessage.MessageText);
            if (!valid) throw new Exception("Message is invalid");

            var room = await _context.Rooms.FirstAsync(c => c.RoomID == newMessage.RoomId);

            if (!room.Open) throw new Exception("Room is closed, cannot add new messages");

            var message = new Message(newMessage, jwtOwner);
            message.AddMessageVersion(new MessageVersion(message.MessageId, newMessage.MessageText));

            _context.Messages.Add(message);
            await _context.SaveChangesAsync();

            return true;
        }
    }

}