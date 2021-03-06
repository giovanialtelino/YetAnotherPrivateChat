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
    public class CloseRoom
    {
        //Can only delete IF there are no users in the room
        public async Task<Room> Close(int roomId, int jwtOwner, MyDbContext _context)
        {
            var room = await _context.Rooms.FirstOrDefaultAsync(c => c.RoomID == roomId);

            if (room == null) throw new Exception("Room does not exist, please contact the chat administrator.");

            room.CloseRoom();
            await _context.SaveChangesAsync();

            return room;
        }

       
    }

}