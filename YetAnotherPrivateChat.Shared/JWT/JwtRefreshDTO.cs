using System;

namespace YetAnotherPrivateChat.Shared.HelperShared.JWT
{
    public class JwtRefreshDTO
    {
        public RefreshToken Refresh {get;set;}
        public JwtToken JWT {get;set;}
        public JwtRefreshDTO(RefreshToken refresh, JwtToken jwt)
        {
            Refresh = refresh;
            JWT = jwt;
        }
    }
}