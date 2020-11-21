using System;
using System.Collections;
using System.Collections.Generic;
using YetAnotherPrivateChat.Shared.HelperShared;
using System.ComponentModel.DataAnnotations.Schema;

namespace YetAnotherPrivateChat.Shared
{
    public class Room
    {
        public int RoomID { get; set; }
        public List<Message> Messages { get; set; }
        public List<User> Users { get; set; }
        public DateTime CreatedAt { get; set; }
        public string RoomName { get; set; }
    }
}
