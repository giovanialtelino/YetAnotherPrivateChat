using System;
using System.Collections;
using YetAnotherPrivateChat.Shared;
using YetAnotherPrivateChat.Shared.HelperShared;
using System.ComponentModel.DataAnnotations.Schema;

namespace YetAnotherPrivateChat.Shared.DTO
{
    public class DeleteRoomDTO
    {
        public int RoomId { get; set; }
        public DeleteRoomDTO(int roomId)
        {
            RoomId = roomId;
        }
    }
}
