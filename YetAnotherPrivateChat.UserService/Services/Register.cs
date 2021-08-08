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
        private readonly MyDbContext _context;
        private readonly Login _loginService;
        public Register(MyDbContext context, Login loginService)
        {
            _context = context;
            _loginService = loginService;
        }

        public async Task<JwtRefreshDTO> RegisterUser(UserDTO dto)
        {

            var helper = new Helper();
            if (!await helper.ValidateEmail(dto.Email)) throw new Exception("Email is invalid.");
            if (!await helper.AllowUsernameAndEmail(dto.Email, dto.Username, _context)) throw new Exception("Username or email are already registered.");

            //save it as a user, them modify the pwd
            var user = new User(dto);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var login = await _loginService.AllowLogin(dto.Username, dto.Password);
            return login;
        }
    }
}