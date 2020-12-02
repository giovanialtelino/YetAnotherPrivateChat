using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using YetAnotherPrivateChat.Shared;
using YetAnotherPrivateChat.UserService.Context;
using YetAnotherPrivateChat.Shared.HelperShared;



namespace YetAnotherPrivateChat.UserService.Service
{
    public class Helper
    {
        public async Task<bool> AllowUsernameAndEmail(string email, string username, MyDbContext ctx)
        {
            var usr = await ctx.Users.AnyAsync(c => c.Username == username);

            if (usr) return false;

            //due to the idea of hashing the emails, I them need to query all of them to compare....
            var hashList = await ctx.Users.Select(c => c.Email).ToListAsync();
            var helperShared = new YetAnotherPrivateChat.Shared.HelperShared.Helper();

            foreach (var hash in hashList)
            {
                if (helperShared.CompareEmail(hash, email)) return false;
            }

            return true;
        }

        public async Task<bool> ValidateEmail(string email)
        {
            //should query an external api to validate if the email exist or not, only using a regex is pretty much useless.
            await Task.Delay(100);
            return true;
        }
    }
}