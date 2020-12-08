using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace YetAnotherPrivateChat.Chat.Interface
{
    public interface IRoomClient
    {
        Task ReceiveRoom(string user, string room);
    }
}
