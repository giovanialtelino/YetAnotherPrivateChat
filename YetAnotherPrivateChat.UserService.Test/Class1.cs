using System;
using Xunit;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using YetAnotherPrivateChat.Shared;
using YetAnotherPrivateChat.Shared.DTO;
using YetAnotherPrivateChat.Shared.HelperShared.JWT;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft;
using Newtonsoft.Json.Serialization;



namespace YetAnotherPrivateChat.UserService.Test
{
    public class Class1 : IClassFixture<CustomWebApplicationFactory<YetAnotherPrivateChat.UserService.Startup>>
    {
        [Fact]
        public void PassingTest()
        {
            Assert.Equal(4, Add(2, 2));
        }

        int Add(int x, int y)
        {
            return x + y;
        }

        private readonly CustomWebApplicationFactory<YetAnotherPrivateChat.UserService.Startup> _factory;

        public Class1(CustomWebApplicationFactory<YetAnotherPrivateChat.UserService.Startup> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData("/")]
        public async Task GetEndPoints(string url)
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync(url);
            var result = await response.Content.ReadAsStringAsync();

            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(result, "I'm alive!");
        }

        [Fact]
        public async Task Signup()
        {
            //Given
            var signupUser = new UserDTO("test", "test@test", "Test123!");

            var client = _factory.CreateClient();

            var todoItemJson = new StringContent(
               JsonConvert.SerializeObject(signupUser),
               Encoding.UTF8,
               "application/json");

            var response = await client.PostAsync("/register", todoItemJson);
            var result = await response.Content.ReadAsStringAsync();
            var jsonResult = JsonConvert.DeserializeObject<JwtRefreshDTO>(result);

            System.Console.WriteLine(jsonResult.JWT.Token);
            System.Console.WriteLine(jsonResult.Refresh.Token);

            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
            //Then
        }
    }
}
