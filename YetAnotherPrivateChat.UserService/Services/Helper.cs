using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using YetAnotherPrivateChat.Shared;
using YetAnotherPrivateChat.UserService.Context;


namespace YetAnotherPrivateChat.UserService.Service
{
    public class Helper
    {
        public async Task<bool> AllowUsernameAndEmail(string email, string username, MyDbContext ctx)
        {
            var emailUsername = await ctx.Users.AnyAsync(c => c.Username == username || c.Email == email);

            if (!emailUsername) return true;
            return false;
        }

        public async Task<bool> ValidateEmail(string email)
        {
            //should query an external api to validate if the email exist or not, only using a regex is pretty much useless.
            await Task.Delay(100);
            return true;
        }
    }
}