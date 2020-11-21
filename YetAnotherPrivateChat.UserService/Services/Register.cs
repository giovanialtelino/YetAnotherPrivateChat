using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using YetAnotherPrivateChat.Shared;
using YetAnotherPrivateChat.UserService.Context;
using Microsoft.EntityFrameworkCore;
using YetAnotherPrivateChat.Shared.HelperShared.JWT;

namespace YetAnotherPrivateChat.UserService.Service
{
    public class Register
    {
        public async Task<JwtRefreshDTO> RegisterUser(UserDTO dto, MyDbContext ctx)
        {
            var helper = new Helper();
            if (!await helper.ValidateEmail(dto.Email)) throw new Exception("Email is invalid.");
            if (!await helper.AllowUsernameAndEmail(dto.Email, dto.Username, ctx)) throw new Exception("Username or email are already registered.");

            //save it as a user, them modify the pwd
            var user = new User(dto);
            ctx.Users.Add(user);
            await ctx.SaveChangesAsync();

            var login = await new Login().AllowLogin(dto.Username, dto.Password, ctx);
            return login;
        }
    }
}