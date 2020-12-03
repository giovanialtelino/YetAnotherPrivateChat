using System;
using System.Collections;
using YetAnotherPrivateChat.Shared;
using YetAnotherPrivateChat.Shared.HelperShared;
using System.ComponentModel.DataAnnotations.Schema;

namespace YetAnotherPrivateChat.Shared.DTO
{
    public class CloseOpenRoomDTO
    {
        public int RoomId { get; set; }
        public CloseOpenRoomDTO(int roomId)
        {
            RoomId = roomId;
        }
    }
}
