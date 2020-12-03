using System;
using System.Collections;
using System.Collections.Generic;
using YetAnotherPrivateChat.Shared.HelperShared;
using System.ComponentModel.DataAnnotations.Schema;
using YetAnotherPrivateChat.Shared.DTO;

namespace YetAnotherPrivateChat.Shared
{
    public class Room
    {
        public int RoomID { get; set; }
        public List<Message> Messages { get; set; }
        public List<User> Users { get; set; }
        public DateTime CreatedAt { get; set; }
        public string RoomName { get; set; }
        public bool Open { get; set; }
        public Room() { }
        public Room(string roomName)
        {
            RoomName = roomName;
            CreatedAt = DateTime.Now;
            Open = false;
        }
        public Room(NewRoomDTO dto)
        {
            RoomName = dto.RoomName;
            CreatedAt = DateTime.Now;
            Open = false;
        }
        public void CloseRoom()
        {
            this.Open = false;
        }
         public void OpenRoom()
        {
            this.Open = false;
        }
    }
}
