using System;
using System.Threading;
using System.Threading.Tasks;
using YetAnotherPrivateChat.Shared.UserClass;
using YetAnotherPrivateChat.UserService.Context;

namespace YetAnotherPrivateChat.UserService.Service
{
    public class Register
    {
        public async Task<UserDTO> RegisterUser(UserDTO dto, MyDbContext ctx)
        {
            var user = new User(dto);

            ctx.Users.Add(user);
            await ctx.SaveChangesAsync();

            return dto;
        }
    }
}