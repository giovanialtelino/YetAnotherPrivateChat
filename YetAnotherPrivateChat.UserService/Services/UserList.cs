using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using YetAnotherPrivateChat.Shared;
using YetAnotherPrivateChat.UserService.Context;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using YetAnotherPrivateChat.Shared.HelperShared.JWT;

namespace YetAnotherPrivateChat.UserService.Service
{
    public class UserList
    {
        public async Task<List<UserDTO>> GetUsers(MyDbContext ctx)
        {
            var userList = await ctx.Users.ToListAsync();
            var userListDTO = new List<UserDTO>();

            foreach (var user in userList)
            {
                userListDTO.Add(new UserDTO(user));
            }

            return userListDTO;
        }
    }
}