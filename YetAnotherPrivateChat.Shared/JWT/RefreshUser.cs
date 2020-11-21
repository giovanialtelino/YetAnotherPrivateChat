using System;
using System.Collections;

namespace YetAnotherPrivateChat.Shared.HelperShared.JWT
{
    public class RefreshTokenDb
    {
        public int RefreshTokenDbId { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool Valid { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public RefreshTokenDb(){}
        public RefreshTokenDb(int userId, int expirationMonths)
        {
            CreationDate = DateTime.Now;
            ExpirationDate = DateTime.Now.AddMonths(expirationMonths);
            Valid = true;
            UserId = userId;
        }

        public void InvalidateToken()
        {
            this.Valid = false;
        }
    }
}