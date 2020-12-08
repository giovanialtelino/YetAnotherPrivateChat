using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using YetAnotherPrivateChat.Chat.Interface;
using System.Linq;
using System.Collections.Generic;
using Dapper;
using System.Data;
using Npgsql;
using Microsoft.Extensions.Configuration;

namespace YetAnotherPrivateChat.Chat.Hubs
{
    public class ChatHub : Hub<IChatClient>
    {
        private readonly IConfiguration _config;
        public ChatHub(IConfiguration config)
        {
            _config = config;
        }

        public IDbConnection Connection
        {
            get
            {
                return new NpgsqlConnection(_config.GetConnectionString("postgre"));
            }
        }

        public async Task SendMessage(string user, string message)
        {
            await Clients.All.ReceiveMessage(user, message);
        }

        public async Task GetMessages(int roomId)
        {
            using(var conn = Connection)
            {
                var q = "SELECT ";
                var result = await conn.QueryAsync<string>(q);
                
            }            
        }
    }
}
