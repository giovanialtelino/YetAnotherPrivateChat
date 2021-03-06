using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using YetAnotherPrivateChat.Chat.Context;

namespace YetAnotherPrivateChat.Chat
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var ctx = new MyDbContext();
            StartDbContext.StartDb(ctx);
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseUrls("http://localhost:5003");
                });
    }
}
