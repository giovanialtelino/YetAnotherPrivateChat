using System;
using System.Collections.Generic;
using YetAnotherPrivateChat.Shared.HelperShared;
using YetAnotherPrivateChat.Shared.HelperShared.JWT;

namespace YetAnotherPrivateChat.Shared
{
    public class UserDTO
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Byte[] Avatar { get; private set; }
        public DateTime RegistrationDate { get; private set; }
        public UserDTO() { }
        public UserDTO(string username, string email, string password)
        {
            Username = username;
            Email = email;
            Password = password;
        }
        public UserDTO(string username, string password)
        {
            Username = username;
            Password = password;
        }
        public UserDTO(User user)
        {
            Username = user.Username;
            Email = user.Email;
            Password = "";
            Avatar = user.Avatar;
            RegistrationDate = user.RegistrationDate;
        }
    }

    public class User
    {
        public int UserId { get; set; }
        public string Username { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public int Admin { get; private set; }
        public Byte[] Avatar { get; private set; }
        public DateTime RegistrationDate { get; private set; }
        public List<RefreshTokenDb> RefreshTokens { get; }

        public User() { }

        public User(string username, string email, string password)
        {
            var goodPassword = new Helper().AprrovedPassword(password);

            if (!goodPassword.Item1) throw new Exception(goodPassword.Item2);

            Username = username;
            Email = SetEmail(Email);
            Password = SetPassWord(password);
            RegistrationDate = DateTime.Now;
            Avatar = new Byte[0];
            Admin = 0;
        }

        public User(UserDTO dto)
        {
            var goodPassword = new Helper().AprrovedPassword(dto.Password);

            if (!goodPassword.Item1) throw new Exception(goodPassword.Item2);

            Username = dto.Username;
            Email = SetEmail(dto.Email);
            Password = SetPassWord(dto.Password);
            RegistrationDate = DateTime.Now;
            Avatar = new Byte[0];
            Admin = 0;
        }

        private string SetPassWord(string password)
        {
            return new Helper().HashPassword(password);
        }

        private string SetEmail(string email)
        {
            return new Helper().HashEmail(email);
        }

        public Tuple<bool, string> ChangeUserAvatar(byte[] image)
        {
            //have no idea how to check if the item is really a image, need to check later

            this.Avatar = image;
            return new Tuple<bool, string>(true, "Your new image will be displayed soon");
        }

        public bool ValidPassword(string hash, string password)
        {
            var comparePassword = new Helper().ComparePassword(hash, password);
            return comparePassword;
        }

        public void TurnAdmin()
        {
            this.Admin = 1;
        }

        public void RemoveAdmin()
        {
            this.Admin = 0;
        }
    }
}
