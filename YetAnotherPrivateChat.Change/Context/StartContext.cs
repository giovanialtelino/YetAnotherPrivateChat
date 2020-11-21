using System;
using YetAnotherPrivateChat.Shared;
using System.IO;
using System.IO.Enumeration;
using System.Threading.Tasks;

namespace YetAnotherPrivateChat.Change.Context
{
    public static class StartDbContext
    {
        public static void StartDb(MyDbContext ctx)
        {
            ctx.Database.EnsureDeleted();
            ctx.Database.EnsureCreated();
        }
    }
}