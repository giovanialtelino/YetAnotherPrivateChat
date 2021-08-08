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
        private readonly MyDbContext _context;
        public Refresh(MyDbContext context)
        {
            _context = context;
        }
        public async Task<JwtRefreshDTO> RefreshJWT(int tokenId, string refresh)
        {
            var refreshHelper = new RefreshToken(refresh);

            var refreshTokenDb = await _context.RefreshDb.Include(c => c.User).FirstOrDefaultAsync(c => c.RefreshTokenDbId == tokenId);

            if (!refreshTokenDb.Valid) throw new Exception("Credentials are invalid, log again.");

            //If it's still valid, just generate a new JWT
            var newJwt = new JwtToken(refreshTokenDb.User.UserId, refreshTokenDb.User.RegistrationDate, refreshTokenDb.User.Admin);

            if (refreshTokenDb.CreationDate.AddMonths(1) <= DateTime.Now) return new JwtRefreshDTO(await RefreshRefreshToken(refreshTokenDb.User.UserId, tokenId), newJwt);

            return new JwtRefreshDTO(refreshHelper, newJwt);
        }

        public async Task<RefreshToken> RefreshRefreshToken(int userId, int tokenId)
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

            var result = _context.RefreshDb.Add(refreshTokenDb);
            await _context.SaveChangesAsync();

            var refreshToken = new RefreshToken(expiration, result.Entity.RefreshTokenDbId);

            await DisableRefreshToken(tokenId);

            return refreshToken;
        }

        public async Task DisableRefreshToken(int tokenId)
        {
            /*
            Set the specific refresh token as invalid, could be called if the user ask for log out, or a new refresh token is created.
            */
            var refreshDb = await _context.RefreshDb.FirstOrDefaultAsync(c => c.RefreshTokenDbId == tokenId);
            refreshDb.InvalidateToken();

            await _context.SaveChangesAsync();
        }
        public async Task DisableAllRefreshToken(int tokenId)
        {
            /*
            Set the specific refresh token as invalid, could be called if the user ask for log out, or a new refresh token is created.
            */
            var userId = await _context.RefreshDb.Include(c => c.User).FirstOrDefaultAsync(c => c.RefreshTokenDbId == tokenId);

            var refreshTokens = await _context.RefreshDb.Where(c => c.UserId == userId.UserId).ToListAsync();

            foreach (var t in refreshTokens)
            {
                t.InvalidateToken();
            }

            await _context.SaveChangesAsync();
        }
    }
}