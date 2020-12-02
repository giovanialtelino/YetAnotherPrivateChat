using System;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;
using Xunit.Extensions;
using System.Collections;
namespace YetAnotherPrivateChat.UserService.Test
{
    public class LoginAuthTestData : TheoryData<string, string, string, bool>
    {
        public LoginAuthTestData()
        {
            Add("user1", "email1@email.com", "Valid1!pwd", true);
            Add("user2", "email2@email.com", "Vadasgalid1!pwd", true);
            Add("user3", "email3@email.com", "Valid@@@@1!pwd", true);
            Add("user4", "email4@email.com", "Validfsdfsdfsdfsd1!pwd", true);
        }
    }

    public class LoginAuthErrorTestData : TheoryData<string, string, string, bool>
    {
        public LoginAuthErrorTestData()
        {
            Add("user5", "email4@email.com", "Valid1!pwd", false);
            Add("user6", "email6@email.com", "invalid", false);
            Add("user7", "email3@email.com", "Valid1!pwd", false);
        }
    }

    public class RefreshTokenTestData : TheoryData<string, bool>
    {
        public RefreshTokenTestData()
        {
            Add("not a jwt", false);
            Add("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c", false);
            Add("eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiYWRtaW4iOnRydWUsImlhdCI6MTUxNjIzOTAyMn0.VFb0qJ1LRg_4ujbZoRMXnVkUgiuKq5KxWqNdbKq_G9Vvz-S1zZa9LPxtHWKa64zDl2ofkT8F6jBt_K4riU-fPg", false);
        }
    }
}