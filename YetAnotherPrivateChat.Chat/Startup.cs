using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using YetAnotherPrivateChat.Chat.Context;
using YetAnotherPrivateChat.Chat.Service;
using YetAnotherPrivateChat.Shared.DTO;
using YetAnotherPrivateChat.Shared.HelperShared.JWT;

namespace YetAnotherPrivateChat.Chat
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<MyDbContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("I'm alive");
                });
                endpoints.MapPost("/addmessage", async context =>
                {
                    MyDbContext _context = new MyDbContext();
                    var add = new AddMessage();

                    var dto = await context.Request.ReadFromJsonAsync<NewMessageDTO>();
                    var jwt = DecodeHeader.GetJwtToken(context.Request.Headers);

                    var result = await add.Add(dto, jwt.id, _context);
                    await context.Response.WriteAsJsonAsync(result);
                });
                endpoints.MapPost("/addroom", async context =>
                {
                    MyDbContext _context = new MyDbContext();
                    var add = new AddRoom();

                    var dto = await context.Request.ReadFromJsonAsync<NewRoomDTO>();
                    var jwt = DecodeHeader.GetJwtToken(context.Request.Headers);

                    if (jwt.admin == 0) throw new Exception("You are not allowed to add a new room");

                    var result = await add.Add(dto, jwt.id, _context);
                });
                endpoints.MapPost("/editmessage", async context =>
                {
                    MyDbContext _context = new MyDbContext();
                    var edit = new EditMessage();

                    var dto = await context.Request.ReadFromJsonAsync<EditMessageDTO>();
                    var jwt = DecodeHeader.GetJwtToken(context.Request.Headers);

                    var result = await edit.Edit(dto, jwt.id, _context);
                    await context.Response.WriteAsJsonAsync(result);
                });
                endpoints.MapPost("/editroom", async context =>
                {
                    MyDbContext _context = new MyDbContext();
                    var edit = new EditRoom();

                    var dto = await context.Request.ReadFromJsonAsync<EditRoomDTO>();
                    var jwt = DecodeHeader.GetJwtToken(context.Request.Headers);

                    if (jwt.admin == 0) throw new Exception("You are not allowed to edit room details");

                    var result = await edit.Edit(dto, jwt.id, _context);
                    await context.Response.WriteAsJsonAsync(result);

                });
                endpoints.MapPost("/deletemessage", async context =>
                {
                    MyDbContext _context = new MyDbContext();
                    var delete = new DeleteMessage();

                    var dto = await context.Request.ReadFromJsonAsync<DeleteMessageDTO>();
                    var jwt = DecodeHeader.GetJwtToken(context.Request.Headers);

                    var result = await delete.Delete(dto.MessageId, jwt.id, _context);
                    await context.Response.WriteAsJsonAsync(result);
                });
                endpoints.MapPost("/closeroom", async context =>
                {
                    MyDbContext _context = new MyDbContext();
                    var close = new CloseRoom();

                    var dto = await context.Request.ReadFromJsonAsync<CloseOpenRoomDTO>();
                    var jwt = DecodeHeader.GetJwtToken(context.Request.Headers);

                    if (jwt.admin == 0) throw new Exception("You are not allowed to close a room");

                    var result = await close.Close(dto.RoomId, jwt.id, _context);
                    await context.Response.WriteAsJsonAsync(result);
                });
                endpoints.MapPost("/openroom", async context =>
               {
                   MyDbContext _context = new MyDbContext();
                   var open = new OpenRoom();

                   var dto = await context.Request.ReadFromJsonAsync<CloseOpenRoomDTO>();
                   var jwt = DecodeHeader.GetJwtToken(context.Request.Headers);

                   if (jwt.admin == 0) throw new Exception("You are not allowed to open a room");

                   var result = await open.Open(dto.RoomId, jwt.id, _context);
                   await context.Response.WriteAsJsonAsync(result);
               });
                endpoints.MapPost("/star", async context =>
              {
                  MyDbContext _context = new MyDbContext();
                  var star = new StarMessage();

                  var dto = await context.Request.ReadFromJsonAsync<StarMessageDTO>();
                  var jwt = DecodeHeader.GetJwtToken(context.Request.Headers);

                  await star.Star(dto.MessageId, jwt.id, _context);
                  await context.Response.WriteAsJsonAsync(StatusCodes.Status200OK);
              });
                endpoints.MapPost("/unstar", async context =>
              {
                  MyDbContext _context = new MyDbContext();
                  var star = new UnStarMessage();

                  var dto = await context.Request.ReadFromJsonAsync<StarMessageDTO>();
                  var jwt = DecodeHeader.GetJwtToken(context.Request.Headers);

                  await star.UnStar(dto.MessageId, jwt.id, _context);
                  await context.Response.WriteAsJsonAsync(StatusCodes.Status200OK);
              });
            });
        }
    }
}