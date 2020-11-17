using System;

namespace YetAnotherPrivateChat.Shared.HelperShared.JWT
{
    public record RefreshData
    {
        public int id { get; init; }
        public long exp {get; init;}
    }

}