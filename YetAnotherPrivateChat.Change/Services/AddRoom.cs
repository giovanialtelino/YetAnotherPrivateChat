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
    public class AddRoom
    {
        public async Task<Room> Add(NewRoomDTO newRoom, int jwtOwner, MyDbContext _context)
        {
            var helper = Helper.ValidRoomName(newRoom.RoomName);

            if(!helper) throw new Exception("Room name is invalid");

            var room = new Room(newRoom);

            _context.Rooms.Add(room);
            await _context.SaveChangesAsync();

            return room;
        }
    }

}