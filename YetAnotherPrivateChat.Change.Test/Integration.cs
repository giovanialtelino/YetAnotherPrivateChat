using System;
using Xunit;
using System.Threading.Tasks;
using System.Collections;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using YetAnotherPrivateChat.Shared;
using YetAnotherPrivateChat.Shared.DTO;
using YetAnotherPrivateChat.Shared.HelperShared.JWT;
using YetAnotherPrivateChat.Change;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft;
using Newtonsoft.Json.Serialization;
using Xunit.Priority;
using System.Collections.Generic;
using System.Linq;

namespace YetAnotherPrivateChat.Change.Test
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class Integration : IClassFixture<CustomWebApplicationFactory<YetAnotherPrivateChat.Change.Startup>>
    {
        private readonly CustomWebApplicationFactory<YetAnotherPrivateChat.Change.Startup> _factory;

        public Integration(CustomWebApplicationFactory<YetAnotherPrivateChat.Change.Startup> factory)
        {
            _factory = factory;
        }

        [Theory, Priority(0)]
        [ClassData(typeof(AddRoomTestData))]
        public async Task AddRoom(string roomName, JwtToken jwt, bool valid)
        {
            var client = _factory.CreateClient();
            var newRoom = new NewRoomDTO(roomName);

            var addMessageJson = new StringContent(
              JsonConvert.SerializeObject(newRoom),
              Encoding.UTF8,
              "application/json");

            client.DefaultRequestHeaders.Add("jwt", jwt.Token);
            var response = await client.PostAsync("/addroom", addMessageJson);
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

        [Theory, Priority(1)]
        [ClassData(typeof(EditRoomTestData))]
        public async Task EditRoom(string newRoomName, int roomId, JwtToken jwt, bool valid)
        {
            var client = _factory.CreateClient();
            var newRoom = new EditRoomDTO(roomId, newRoomName);

            var addMessageJson = new StringContent(
              JsonConvert.SerializeObject(newRoom),
              Encoding.UTF8,
              "application/json");

            client.DefaultRequestHeaders.Add("jwt", jwt.Token);
            var response = await client.PostAsync("/editroom", addMessageJson);
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

        [Theory, Priority(2)]
        [ClassData(typeof(CloseOpenRoomTestData))]
        public async Task CloseRoom(int roomId, JwtToken jwt, bool valid)
        {
            var client = _factory.CreateClient();
            var newRoom = new CloseOpenRoomDTO(roomId);

            var addMessageJson = new StringContent(
              JsonConvert.SerializeObject(newRoom),
              Encoding.UTF8,
              "application/json");

            client.DefaultRequestHeaders.Add("jwt", jwt.Token);
            var response = await client.PostAsync("/closeroom", addMessageJson);
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
        [ClassData(typeof(CloseOpenRoomTestData))]
        public async Task OpenRoom(int roomId, JwtToken jwt, bool valid)
        {
            var client = _factory.CreateClient();
            var newRoom = new CloseOpenRoomDTO(roomId);

            var addMessageJson = new StringContent(
              JsonConvert.SerializeObject(newRoom),
              Encoding.UTF8,
              "application/json");

            client.DefaultRequestHeaders.Add("jwt", jwt.Token);
            var response = await client.PostAsync("/openroom", addMessageJson);
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
        [ClassData(typeof(AddMessageTestData))]
        public async Task AddMessage(string messageText, int roomId, JwtToken jwt, bool valid)
        {
            var client = _factory.CreateClient();
            var newMessage = new NewMessageDTO(messageText, roomId);

            var addMessageJson = new StringContent(
              JsonConvert.SerializeObject(newMessage),
              Encoding.UTF8,
              "application/json");

            client.DefaultRequestHeaders.Add("jwt", jwt.Token);
            var response = await client.PostAsync("/addmessage", addMessageJson);
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

        [Theory, Priority(5)]
        [ClassData(typeof(StarUnstarMessageTestData))]
        public async Task StarMessage(int messageId, JwtToken jwt, bool valid)
        {
            var client = _factory.CreateClient();
            var newMessage = new StarMessageDTO(messageId);

            var addMessageJson = new StringContent(
              JsonConvert.SerializeObject(newMessage),
              Encoding.UTF8,
              "application/json");

            client.DefaultRequestHeaders.Add("jwt", jwt.Token);
            var response = await client.PostAsync("/star", addMessageJson);
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

        [Theory, Priority(6)]
        [ClassData(typeof(StarUnstarMessageTestData))]
        public async Task UnStarMessage(int messageId, JwtToken jwt, bool valid)
        {
            var client = _factory.CreateClient();
            var newMessage = new StarMessageDTO(messageId);

            var addMessageJson = new StringContent(
              JsonConvert.SerializeObject(newMessage),
              Encoding.UTF8,
              "application/json");

            client.DefaultRequestHeaders.Add("jwt", jwt.Token);
            var response = await client.PostAsync("/unstar", addMessageJson);
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

        [Theory, Priority(7)]
        [ClassData(typeof(EditMessageTestData))]
        public async Task EditMessage(int messageId, string messageText, JwtToken jwt, bool valid)
        {
            var client = _factory.CreateClient();
            var newMessage = new EditMessageDTO(messageId, messageText);

            var addMessageJson = new StringContent(
              JsonConvert.SerializeObject(newMessage),
              Encoding.UTF8,
              "application/json");

            client.DefaultRequestHeaders.Add("jwt", jwt.Token);
            var response = await client.PostAsync("/editmessage", addMessageJson);
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

        [Theory, Priority(8)]
        [ClassData(typeof(DeleteMessageTestData))]
        public async Task DeleteMessage(int messageId, JwtToken jwt, bool valid)
        {
            var client = _factory.CreateClient();
            var newMessage = new DeleteMessageDTO(messageId);

            var addMessageJson = new StringContent(
              JsonConvert.SerializeObject(newMessage),
              Encoding.UTF8,
              "application/json");

            client.DefaultRequestHeaders.Add("jwt", jwt.Token);
            var response = await client.PostAsync("/deletemessage", addMessageJson);
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
    }
}
