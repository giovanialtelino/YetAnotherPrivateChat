using System;
using Xunit;
using System.Threading.Tasks;
using System.Collections;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using YetAnotherPrivateChat.Shared;
using YetAnotherPrivateChat.Shared.DTO;
using YetAnotherPrivateChat.Shared.HelperShared.JWT;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft;
using Newtonsoft.Json.Serialization;
using Xunit.Priority;
using System.Collections.Generic;
using System.Linq;

namespace YetAnotherPrivateChat.UserService.Test
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class Integration : IClassFixture<CustomWebApplicationFactory<YetAnotherPrivateChat.UserService.Startup>>
    {
        private readonly CustomWebApplicationFactory<YetAnotherPrivateChat.UserService.Startup> _factory;

        public Integration(CustomWebApplicationFactory<YetAnotherPrivateChat.UserService.Startup> factory)
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

        [Theory, Priority(0)]
        [AssemblyTrait: CollectionBehavior(DisableTestParallelization = true)]
        [ClassData(typeof(LoginAuthTestData))]
        [ClassData(typeof(LoginAuthErrorTestData))]
        public async Task Register(string username, string email, string pwd, bool valid)
        {
            var client = _factory.CreateClient();

            var userDto = new UserDTO(username, email, pwd);

            var registerItemJson = new StringContent(
              JsonConvert.SerializeObject(userDto),
              Encoding.UTF8,
              "application/json");

            var response = await client.PostAsync("/register", registerItemJson);
            var result = await response.Content.ReadAsStringAsync();

            if (valid)
            {
                var jsonResult = JsonConvert.DeserializeObject<JwtRefreshDTO>(result);
                Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
            }
            else
            {
                Assert.Equal(System.Net.HttpStatusCode.InternalServerError, response.StatusCode);
            }
        }

        [Theory, Priority(1)]
        [ClassData(typeof(LoginAuthTestData))]
        [ClassData(typeof(LoginAuthErrorTestData))]
        public async Task Login(string username, string email, string pwd, bool valid)
        {
            var client = _factory.CreateClient();

            var userDto = new UserDTO(username, pwd);

            var registerItemJson = new StringContent(
                         JsonConvert.SerializeObject(userDto),
                         Encoding.UTF8,
                         "application/json");

            var response = await client.PostAsync("/login", registerItemJson);
            var result = await response.Content.ReadAsStringAsync();

            if (valid)
            {
                var jsonResult = JsonConvert.DeserializeObject<JwtRefreshDTO>(result);
                Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);

                //Try to refresh the token
                await Refresh(jsonResult.Refresh.Token, true);
                await UserList(jsonResult.Refresh.Token, false); //since I'm sending the refresh instead of the JWT it should break
                await UserList(jsonResult.JWT.Token, true);
            }
            else
            {
                Assert.Equal(System.Net.HttpStatusCode.InternalServerError, response.StatusCode);
            }
        }

        [Theory, Priority(2)]
        [ClassData(typeof(RefreshTokenTestData))]
        public async Task Refresh(string refreshToken, bool valid)
        {
            var client = _factory.CreateClient();
            client.DefaultRequestHeaders.Add("ref", refreshToken); //for some reason refresh doesn't work....

            var response = await client.GetAsync("/refresh");
            var result = await response.Content.ReadAsStringAsync();

            if (valid)
            {
                Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
            }
            else
            {
                Assert.Equal(System.Net.HttpStatusCode.InternalServerError, response.StatusCode);
            }
        }

        [Theory, Priority(3)]
        [ClassData(typeof(RefreshTokenTestData))]
        public async Task UserList(string token, bool valid)
        {
            var client = _factory.CreateClient();
            client.DefaultRequestHeaders.Add("jwt", token);

            var response = await client.GetAsync("/userlist");
            var result = await response.Content.ReadAsStringAsync();

            if (valid)
            {
                Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
            }
            else
            {
                Assert.Equal(System.Net.HttpStatusCode.InternalServerError, response.StatusCode);
            }
        }

        [Theory, Priority(4)]
        [ClassData(typeof(LoginAuthTestData))]
        public async Task Logoff(string username, string email, string pwd, bool valid)
        {
            //logoff get the refresh token id, and removes it
            var client = _factory.CreateClient();

            var userDto = new UserDTO(username, pwd);

            var loginJSON = new StringContent(
                         JsonConvert.SerializeObject(userDto),
                         Encoding.UTF8,
                         "application/json");

            //We need to test the whole pipeline here, first we login
            var response = await client.PostAsync("/login", loginJSON);
            var result = await response.Content.ReadAsStringAsync();
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);

            //then we get the refresh token
            var jsonResult = JsonConvert.DeserializeObject<JwtRefreshDTO>(result);
            var refreshToken = jsonResult.Refresh.Token;

            //we add the refresh token the to header
            client.DefaultRequestHeaders.Add("ref", refreshToken);

            //if all is fine we should refresh the JWT
            var refreshResponse = await client.GetAsync("/refresh");
            Assert.Equal(System.Net.HttpStatusCode.OK, refreshResponse.StatusCode);

            //then we remove the refresh token
            var disableRefreshResponse = await client.DeleteAsync("/logoff");
            Assert.Equal(System.Net.HttpStatusCode.OK, disableRefreshResponse.StatusCode);

            //now we try to refresh again, but since we invalidated the refresh token, a error should return instead of a OK
            var refreshResponseError = await client.GetAsync("/refresh");
            Assert.Equal(System.Net.HttpStatusCode.InternalServerError, refreshResponseError.StatusCode);
        }

        [Theory, Priority(5)]
        [ClassData(typeof(LoginAuthTestData))]
        public async Task LogoffAll(string username, string email, string pwd, bool valid)
        {
            //logoff get the refresh token id, and removes it
            var refreshTokensList = new List<string>();
            var client = _factory.CreateClient();

            var userDto = new UserDTO(username, pwd);

            var loginJSON = new StringContent(
                         JsonConvert.SerializeObject(userDto),
                         Encoding.UTF8,
                         "application/json");

            //need to login a lot of times, to collect multiple refresh tokens, we will do five times.

            for (int i = 0; i < 5; i++)
            {
                //We need to test the whole pipeline here, first we login
                var response = await client.PostAsync("/login", loginJSON);
                var result = await response.Content.ReadAsStringAsync();
                Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);

                //then we get the refresh token
                var jsonResult = JsonConvert.DeserializeObject<JwtRefreshDTO>(result);
                var refreshToken = jsonResult.Refresh.Token;
                refreshTokensList.Add(refreshToken);

                //we add the refresh token the to header
                client.DefaultRequestHeaders.Add("ref", refreshToken);

                //if all is fine we should refresh the JWT
                var refreshResponse = await client.GetAsync("/refresh");
                Assert.Equal(System.Net.HttpStatusCode.OK, refreshResponse.StatusCode);

                client.DefaultRequestHeaders.Remove("ref");
            }

            //must logoff only once and try all the other refresh tokens from before
            //then we remove the refresh token
            var random = new Random();
            var item = refreshTokensList.OrderBy(s => random.NextDouble()).First(); //should be a better method of avoid to use the same position key
            client.DefaultRequestHeaders.Add("ref", item);
            var disableRefreshResponse = await client.DeleteAsync("/logoffall");
            Assert.Equal(System.Net.HttpStatusCode.OK, disableRefreshResponse.StatusCode);

            //now we try to refresh again, but since we invalidated the refresh token, a error should return instead of a OK
            foreach (var token in refreshTokensList)
            {
                client.DefaultRequestHeaders.Add("ref", token);
                var refreshResponseError = await client.GetAsync("/refresh");
                Assert.Equal(System.Net.HttpStatusCode.InternalServerError, refreshResponseError.StatusCode);
                client.DefaultRequestHeaders.Remove("ref");
            }
        }
    }
}
