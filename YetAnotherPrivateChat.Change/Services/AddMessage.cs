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
    public class AddMessage
    {
        public async Task<Message> Add(NewMessageDTO newMessage, int jwtOwner, MyDbContext _context)
        {
            var valid = Helper.ValidMessageText(newMessage.MessageText);
            if (!valid) throw new Exception("Message is invalid");

            var room = await _context.Rooms.FirstAsync(c => c.RoomID == newMessage.RoomId);

            if (!room.Open) throw new Exception("Room is closed, cannot add new messages");

            var message = new Message(newMessage, jwtOwner);

            _context.Messages.Add(message);
            await _context.SaveChangesAsync();

            return message;
        }
    }

}