using System;
using System.Collections;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Security.Cryptography;
using JWT;
using JWT.Algorithms;
using JWT.Builder;
using JWT.Exceptions;

namespace YetAnotherPrivateChat.Shared.HelperShared.JWT
{
    public class RefreshToken
    {
        private static string _secret = "another-bad-secret";
        public string Token { get; init; }
        public RefreshToken() { }
        public RefreshToken(string token)
        {
            Token = token;
        }
        public RefreshToken(int experirationMonths, int id)
        {
            var token = new JwtBuilder()
                        .WithAlgorithm(new HMACSHA256Algorithm()) // symmetric
                        .WithSecret(_secret)
                        .AddClaim("exp", DateTimeOffset.UtcNow.AddMonths(experirationMonths).ToUnixTimeSeconds())
                        .AddClaim("id", id)
                        .Encode();

            Token = token;
        }


        public RefreshData DecodeToken()
        {
            var token = this.Token;
            try
            {
                var json = new JwtBuilder()
                    .WithAlgorithm(new HMACSHA256Algorithm()) // symmetric
                    .WithSecret(_secret)
                    .MustVerifySignature()
                    .Decode<RefreshData>(token);

                return json;
            }
            catch (TokenExpiredException)
            {
                throw new Exception("You haven't logged for a long time, you need to type the username and password again");
            }
            catch (InvalidTokenPartsException)
            {
                throw new Exception("Your credentials are invalid, please enter again");
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e); //should send to a logger
                throw new Exception("Try to log in again.");
            }
        }
    }
}