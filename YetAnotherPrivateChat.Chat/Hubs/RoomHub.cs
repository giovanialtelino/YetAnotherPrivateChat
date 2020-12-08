using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using  YetAnotherPrivateChat.Chat.Interface;

namespace YetAnotherPrivateChat.Chat.Hubs
{
    public class RoomHub: Hub<IRoomClient>
    {
        public async Task SendRoom(string user, string room)
        {
            await Clients.All.ReceiveRoom(user, room);
        }
    }
}
