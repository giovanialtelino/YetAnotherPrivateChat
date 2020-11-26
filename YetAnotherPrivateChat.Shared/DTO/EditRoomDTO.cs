using System;
using System.Collections;
using YetAnotherPrivateChat.Shared;
using YetAnotherPrivateChat.Shared.HelperShared;
using System.ComponentModel.DataAnnotations.Schema;

namespace YetAnotherPrivateChat.Shared.DTO
{
    public class EditRoomDTO
    {
        public int RoomId { get; set; }
        public string NewName {get;set;}
        public EditRoomDTO(int roomId, string newName)
        {
            RoomId = roomId;
            NewName = newName;
        }
    }
}
