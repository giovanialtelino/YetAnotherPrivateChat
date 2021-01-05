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
    public static class Helper
    {
        public static bool ValidRoomName(string roomName)
        {
            //just avoiding empty string, more rules could be added.
            if(string.IsNullOrWhiteSpace(roomName)) return false;
            return true;
        }

        public static bool ValidMessageText(string message)
        {
            //just avoiding empty string, more rules could be added.
            if(string.IsNullOrWhiteSpace(message)) return false;
            return true;
        }
    }

}