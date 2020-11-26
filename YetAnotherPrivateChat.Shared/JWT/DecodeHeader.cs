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

            var helper = Decoder.DecodeToken(jwt);
            return helper;
        }
    }
}