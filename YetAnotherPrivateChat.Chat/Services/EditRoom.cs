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
    public class EditRoom
    {
        //It's not really a edit, just create a new version, with new time and new text
        public async Task<Room> Edit(EditRoomDTO editRoom, int jwtOwner, MyDbContext _context)
        {

var helper = Helper.ValidRoomName(editRoom.NewName);
if(!helper) throw new Exception("Room name is invalid");

            var room = await _context.Rooms.FirstOrDefaultAsync(c => c.RoomID == editRoom.RoomId);

            if (room == null) throw new Exception("Room does not exist, please contact the chat administrator");

            room.RoomName = editRoom.NewName;
            await _context.SaveChangesAsync();

            return room;
        }
    }

}