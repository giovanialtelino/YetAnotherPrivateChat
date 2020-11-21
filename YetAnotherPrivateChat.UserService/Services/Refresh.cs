using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using YetAnotherPrivateChat.Shared;
using YetAnotherPrivateChat.Shared.HelperShared;
using YetAnotherPrivateChat.UserService.Context;
using Microsoft.EntityFrameworkCore;
using YetAnotherPrivateChat.Shared.HelperShared.JWT;

namespace YetAnotherPrivateChat.UserService.Service
{
    public class Refresh
    {
        public async Task<JwtRefreshDTO> RefreshJWT(string refreshToken, MyDbContext ctx)
        {
            var refreshHelper = new RefreshToken(refreshToken);
            var decodeRefreshToken = refreshHelper.DecodeToken();

            var refreshTokenDb = await ctx.RefreshDb.Include(c => c.User).FirstOrDefaultAsync(c => c.RefreshTokenDbId == decodeRefreshToken.id);

            if (!refreshTokenDb.Valid) throw new Exception("Credentials are invalid, log again.");

            //If it's still valid, just generate a new JWT
            var newJwt = new JwtToken(refreshTokenDb.User.UserId, refreshTokenDb.User.RegistrationDate);

            if (refreshTokenDb.CreationDate.AddMonths(1) <= DateTime.Now) return new JwtRefreshDTO(await RefreshRefreshToken(refreshTokenDb.User.UserId, refreshToken, ctx), newJwt);
            return new JwtRefreshDTO(refreshHelper, newJwt);
        }

        public async Task<RefreshToken> RefreshRefreshToken(int userId, string token, MyDbContext ctx)
        {
            /*
            If the Refresh token is already older than one month, a new one is created.
            Why? I think is a good idea to rotate those keys.
            Why one month? IDK, probably should be less than one month, just trying to avoid some DB trip.
            Keep in mind that the token is still valid for 6 month, by default, so no problem if the user logs before one month.
            Older Refresh Token became invalid.
            */
            int expiration = 6;

            var refreshTokenDb = new RefreshTokenDb(userId, expiration);

            var result = ctx.RefreshDb.Add(refreshTokenDb);
            await ctx.SaveChangesAsync();

            var refreshToken = new RefreshToken(expiration, result.Entity.RefreshTokenDbId);

            await DisableRefreshToken(token, ctx);

            return refreshToken;
        }

        public async Task DisableRefreshToken(string token, MyDbContext ctx)
        {
            /*
            Set the specific refresh token as invalid, could be called if the user ask for log out, or a new refresh token is created.
            */
            var refresh = new RefreshToken(token);
            var decoded = refresh.DecodeToken();

            var refreshDb = await ctx.RefreshDb.FirstOrDefaultAsync(c => c.RefreshTokenDbId == decoded.id);
            refreshDb.InvalidateToken();

            await ctx.SaveChangesAsync();
        }
        public async Task DisableAllRefreshToken(string token, MyDbContext ctx)
        {
            /*
            Set the specific refresh token as invalid, could be called if the user ask for log out, or a new refresh token is created.
            */
            var refresh = new RefreshToken(token);
            var decoded = refresh.DecodeToken();

            var userId = await ctx.RefreshDb.Include(c => c.User).FirstOrDefaultAsync(c => c.RefreshTokenDbId == decoded.id);

            var refreshTokens = await ctx.RefreshDb.Where(c => c.UserId == userId.UserId).ToListAsync();

            foreach (var t in refreshTokens)
            {
                t.InvalidateToken();
            }

            await ctx.SaveChangesAsync();
        }
    }
}