using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace YetAnotherPrivateChat.Chat.Interface
{
    public interface IChatClient
    {
        Task ReceiveMessage(string user, string message);
    }
}
