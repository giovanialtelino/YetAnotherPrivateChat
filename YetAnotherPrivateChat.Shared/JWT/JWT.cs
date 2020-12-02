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

namespace YetAnotherPrivateChat.Shared.HelperShared.JWT
{

    public class JwtToken
    {
        private static string _secret = "bad-secret";

        public string Token { get; init; }
        public JwtToken(int userId, DateTime userDate)
        {
            var token = new JwtBuilder()
                        .WithAlgorithm(new HMACSHA256Algorithm()) // symmetric
                        .WithSecret(_secret)
                        .AddClaim("exp", DateTimeOffset.UtcNow.AddMinutes(10).ToUnixTimeSeconds())
                        .AddClaim("id", userId)
                        .AddClaim("date", userDate)
                        .Encode();

            Token = token;
        }
    }

    public static class DecodeJWT
    {
        private static string _secret = "bad-secret";
        public static JwtData DecodeToken(string token)
        {
            var json = new JwtBuilder()
                    .WithAlgorithm(new HMACSHA256Algorithm()) // symmetric
                    .WithSecret(_secret)
                    .MustVerifySignature()
                    .Decode<JwtData>(token);

            Console.WriteLine(json.date);
            Console.WriteLine(json.id);

            return json;
        }
    }
}