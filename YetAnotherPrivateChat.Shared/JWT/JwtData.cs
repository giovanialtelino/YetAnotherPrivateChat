using System;

namespace YetAnotherPrivateChat.Shared.HelperShared.JWT
{
    public record JwtData
    {
        public int id { get; init; }
        public int admin { get; init; }
        public DateTime date { get; init; }
        public long exp { get; init; }
    }

}