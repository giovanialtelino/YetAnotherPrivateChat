using System;
using YetAnotherPrivateChat.Shared;
using System.IO;
using System.IO.Enumeration;
using System.Threading.Tasks;

namespace YetAnotherPrivateChat.UserService.Context
{
    public static class StartDbContext
    {
        public static void StartDb(MyDbContext ctx)
        {
            ctx.Database.EnsureCreated();
        }
    }
}