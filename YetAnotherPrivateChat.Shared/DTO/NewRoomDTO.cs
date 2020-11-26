using System;
using System.Collections;
using YetAnotherPrivateChat.Shared;
using YetAnotherPrivateChat.Shared.HelperShared;
using System.ComponentModel.DataAnnotations.Schema;

namespace YetAnotherPrivateChat.Shared.DTO
{
    public class NewRoomDTO
    {
        public string RoomName { get; set; }
        public NewRoomDTO() { }
        public NewRoomDTO(string roomName)
        {
            RoomName = roomName;
        }

    }
}
