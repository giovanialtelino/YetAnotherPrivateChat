using System;
using System.Collections.Generic;
using YetAnotherPrivateChat.Shared.MessageClass;
using YetAnotherPrivateChat.Shared.HelperShared;

namespace YetAnotherPrivateChat.Shared.UserClass
{
    public record UserDTO
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class User
    {
        public int UserId { get; set; }
        private string Username { get; set; }
        private string Email { get; set; }
        private string Password { get; set; }
        private Byte[] Avatar { get; set; }
        private DateTime RegistrationDate { get; set; }
        private List<Message> Messages { get; set; }
        public User(){}

        public User(string username, string email, string password)
        {
            var goodPassword = new Helper().AprrovedPassword(password);

            if (!goodPassword.Item1) throw new Exception(goodPassword.Item2);

            Username = username;
            Email = email;
            Password = password;
            RegistrationDate = DateTime.Now;
        }

         public User(UserDTO dto)
        {
            var goodPassword = new Helper().AprrovedPassword(dto.Password);

            if (!goodPassword.Item1) throw new Exception(goodPassword.Item2);

            Username = dto.Username;
            Email = dto.Email;
            Password = dto.Password;
            RegistrationDate = DateTime.Now;
        }

        public Tuple<bool, string> ChangeUserAvatar(byte[] image)
        {
            //have no idea how to check if the item is really a image, need to check later

            this.Avatar = image;
            return new Tuple<bool, string>(true, "Your new image will be displayed soon");
        }

        public bool ValidPassword(string password)
        {
            if (this.Password == password) return true;
            return false;
        }
        public int GetUserId()
        {
            return this.UserId;
        }
        public string GetUsername()
        {
            return this.Username;
        }
        public string GetEmail()
        {
            return this.Email;
        }
        public Byte[] GetAvatar()
        {
            return this.Avatar;
        }
    }
}
