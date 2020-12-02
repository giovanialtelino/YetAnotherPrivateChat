using System;
using System.Collections;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using JWT;
using JWT.Builder;
using JWT.Algorithms;
using JWT.Serializers;
using System.Security;
using System.Security.Claims;
using System.Collections.Generic;
using System.Threading.Tasks;
using YetAnotherPrivateChat.Shared.DTO;
using YetAnotherPrivateChat.Shared.HelperShared.JWT;
using Microsoft.AspNetCore.Http;

namespace YetAnotherPrivateChat.Shared.HelperShared.JWT
{
    public static class DecodeHeader
    {
        public static JwtData GetJwtToken(IHeaderDictionary header)
        {
            var jwt = header.FirstOrDefault(c => c.Key == "jwt").Value;
            if (string.IsNullOrWhiteSpace(jwt)) throw new Exception("No valid JWT found");

            var helper = DecodeJWT.DecodeToken(jwt);
            return helper;
        }

         public static Tuple<RefreshData, string> GetRefreshToken(IHeaderDictionary header)
        {
            var refresh = header.FirstOrDefault(c => c.Key == "ref").Value;
            if (string.IsNullOrWhiteSpace(refresh)) throw new Exception("No valid token found");

            var helper = DecodeRefresh.DecodeToken(refresh);
            return new Tuple<RefreshData, string>(helper, refresh);
        }
    }
}