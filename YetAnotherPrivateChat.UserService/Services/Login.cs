using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using YetAnotherPrivateChat.Shared.UserClass;
using YetAnotherPrivateChat.Shared.HelperShared;
using YetAnotherPrivateChat.UserService.Context;
using Microsoft.EntityFrameworkCore;
using YetAnotherPrivateChat.Shared.HelperShared.JWT;

namespace YetAnotherPrivateChat.UserService.Service
{
    public class Login
    {
        public async Task<JwtRefreshDTO> AllowLogin(string username, string password, MyDbContext ctx)
        {
            var helperShared = new YetAnotherPrivateChat.Shared.HelperShared.Helper();

            var user = await ctx.Users.FirstOrDefaultAsync(c => c.Username == username);

            var allowed = helperShared.ComparePassword(user.Password, password);

            if (!allowed) throw new Exception("Denied login, please check your password and username");

            int expiration = 6;

            /*
            What is the rationale of adding the row id to the Refresh token?
            Since we need to check if the Refresh Token is still valid or not we need some identification.
            I could send a blank Refresh token and save its string to the DB, but them I would have all of the Refresh tokens stored
            If a breach happened the attacker would have access of all the users, but now since I'm not storing anything the attacker would also need the secret key
            So I'm pretty much adding another chain to protect the application.
            */

            var refreshTokenDb = new RefreshTokenDb(user.UserId, expiration);

            var result = ctx.RefreshDb.Add(refreshTokenDb);
            await ctx.SaveChangesAsync();

            var refreshToken = new RefreshToken(expiration, result.Entity.RefreshTokenDbId);
            var jwt = new JwtToken(user.UserId, user.RegistrationDate);

            return new JwtRefreshDTO(refreshToken, jwt);
        }
    }
}