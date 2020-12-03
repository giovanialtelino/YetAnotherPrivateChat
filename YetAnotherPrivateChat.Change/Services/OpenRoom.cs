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
    public class OpenRoom
    {
        public async Task<Room> Open(int roomId, int jwtOwner, MyDbContext _context)
        {
            var room = await _context.Rooms.FirstOrDefaultAsync(c => c.RoomID == roomId);

            if (room == null) throw new Exception("Room does not exist, please contact the chat administrator.");

            room.OpenRoom();
            await _context.SaveChangesAsync();

            return room;
        }
    }
}