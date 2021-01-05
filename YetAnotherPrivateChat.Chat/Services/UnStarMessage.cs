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
    public class UnStarMessage
    {
        public async Task UnStar(int messageId, int jwtId, MyDbContext _context)
        {
            var star = await _context.Stars.FirstOrDefaultAsync(s => s.MessageId == messageId && s.UserId == jwtId);
            if (star == null) throw new Exception("No star were found.");

            _context.Stars.Remove(star);
            await _context.SaveChangesAsync();
        }
    }
}