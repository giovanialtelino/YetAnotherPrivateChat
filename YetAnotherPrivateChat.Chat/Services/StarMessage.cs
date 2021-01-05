using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using YetAnotherPrivateChat.Shared;
using YetAnotherPrivateChat.Shared.HelperShared;
using YetAnotherPrivateChat.Chat.Context;
using Microsoft.EntityFrameworkCore;
using YetAnotherPrivateChat.Shared.HelperShared.JWT;

namespace YetAnotherPrivateChat.Chat.Service
{
    public class StarMessage
    {
        public async Task Star(int messageId, int jwtId, MyDbContext _context)
        {
            var newStar = new Star(jwtId, messageId);

            try
            {
                _context.Stars.Add(newStar);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e);
                throw new Exception("Was not possible the star the message, refresh the page");
            }
        }
    }
}