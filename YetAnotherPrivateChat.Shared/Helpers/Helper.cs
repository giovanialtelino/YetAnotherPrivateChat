using System;
using System.Collections;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Security.Cryptography;
using BCrypt;

namespace YetAnotherPrivateChat.Shared.HelperShared
{
    public class Helper
    {
        public Tuple<bool, string> AprrovedMessage(string msg)
        {
            if (msg.Length <= 0) return new Tuple<bool, string>(false, "Message must have at least one character");
            return new Tuple<bool, string>(true, "");
        }

        public Tuple<bool, string> AprrovedPassword(string pwd)
        {
            var error = "";

            var specialChar = false;
            var lowerCase = false;
            var upperCase = false;
            var numeric = false;
            //would be cool to have another method to check for common passwords, but sem tempo irmão.

            foreach (var letter in pwd)
            {
                if (!Char.IsLetterOrDigit(letter))
                {
                    specialChar = true;
                }
                if (!Char.IsLower(letter))
                {
                    lowerCase = true;
                }
                if (!Char.IsUpper(letter))
                {
                    upperCase = true;
                }
                if (!Char.IsNumber(letter))
                {
                    numeric = true;
                }
            }

            if (pwd.Length < 6) error += "Password is not long enough. \n";
            if (!specialChar) error += "Password has only text or digits, must add a special character. \n";
            if (!lowerCase) error += "Password has no lower case text. \n";
            if (!upperCase) error += "Password has no upper case text. \n";
            if (!numeric) error += "Password has no number. \n";

            if (error.Length is 0) return new Tuple<bool, string>(true, error);
            return new Tuple<bool, string>(false, error);
        }

        public string HashPassword(string pwd)
        {
            //Doing it like dropbox, besides the AES at the end;
            //Leaving the bcrypt at the default 11
            string hash = BCrypt.Net.BCrypt.EnhancedHashPassword(pwd, hashType: BCrypt.Net.HashType.SHA512);
            return hash;
        }

        public string HashEmail(string email)
        {
            string hash = BCrypt.Net.BCrypt.EnhancedHashPassword(email, hashType: BCrypt.Net.HashType.SHA512);
            return hash;
        }

        public bool ComparePassword(string hash, string pwd)
        {
            bool equal = BCrypt.Net.BCrypt.EnhancedVerify(pwd, hash, hashType: BCrypt.Net.HashType.SHA512);
            return equal;
        }

        public bool CompareEmail(string hash, string email)
        {
            bool equal = BCrypt.Net.BCrypt.EnhancedVerify(email, hash, hashType: BCrypt.Net.HashType.SHA512);
            return equal;
        }
    }
}