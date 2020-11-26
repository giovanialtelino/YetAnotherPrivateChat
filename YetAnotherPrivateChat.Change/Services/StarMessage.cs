using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using YetAnotherPrivateChat.Shared;
using YetAnotherPrivateChat.Shared.HelperShared;
using YetAnotherPrivateChat.Change.Context;
using Microsoft.EntityFrameworkCore;
using YetAnotherPrivateChat.Shared.HelperShared.JWT;

namespace YetAnotherPrivateChat.Change.Service
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
        public async Task UnStar(int messageId, int jwtId, MyDbContext _context)
        {
            var star = await _context.Stars.FirstOrDefaultAsync(s => s.MessageId == messageId && s.UserId == jwtId);
            if (star == null) throw new Exception("No star were found.");

            _context.Stars.Remove(star);
            await _context.SaveChangesAsync();
        }
    }
}