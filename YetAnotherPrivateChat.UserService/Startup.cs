using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Net.Http.Json;
using YetAnotherPrivateChat.Shared;
using YetAnotherPrivateChat.Shared.HelperShared.JWT;
using YetAnotherPrivateChat.UserService.Context;
using YetAnotherPrivateChat.UserService.Service;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore;
using Npgsql;
using Npgsql.EntityFrameworkCore.PostgreSQL;

namespace YetAnotherPrivateChat.UserService
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
                    await context.Response.WriteAsync("I'm alive!");
                });
                endpoints.MapPost("/login", async context =>
                {
                    var _context = new MyDbContext();
                    var log = new Login();
                    var dto = await context.Request.ReadFromJsonAsync<UserDTO>();

                    var result = await log.AllowLogin(dto.Username, dto.Password, _context);
                    await context.Response.WriteAsJsonAsync(result);
                });
                endpoints.MapGet("/refresh", async context =>
                {
                    var _context = new MyDbContext();
                    var refresh = new Refresh();

                    var refreshToken = DecodeHeader.GetRefreshToken(context.Request.Headers);

                    var result = await refresh.RefreshJWT(refreshToken.Item1.id, refreshToken.Item2, _context);
                    await context.Response.WriteAsJsonAsync(result);
                });
                endpoints.MapPost("/register", async context =>
                {
                    var _context = new MyDbContext();
                    var reg = new Register();

                    var dto = await context.Request.ReadFromJsonAsync<UserDTO>();

                    var result = await reg.RegisterUser(dto, _context);
                    await context.Response.WriteAsJsonAsync(result);

                });
                endpoints.MapGet("/userlist", async context =>
                {
                    var _context = new MyDbContext();
                    var us = new UserList();

                    //check if the JWT is valid, if there is none or invalid it will send a error back
                    var jwt = DecodeHeader.GetJwtToken(context.Request.Headers);

                    var result = await us.GetUsers(_context);
                    await context.Response.WriteAsJsonAsync(result);
                });
                endpoints.MapDelete("/logoff", async context =>
                {
                    var _context = new MyDbContext();
                    var off = new Refresh();

                    var dto = await context.Request.ReadFromJsonAsync<RefreshToken>();

                    await off.DisableRefreshToken(dto.Token, _context);
                    await context.Response.WriteAsync("Logged off");

                });
                endpoints.MapDelete("/logoffall", async context =>
                {
                    var _context = new MyDbContext();
                    var off = new Refresh();

                    var dto = await context.Request.ReadFromJsonAsync<RefreshToken>();

                    await off.DisableAllRefreshToken(dto.Token, _context);
                    await context.Response.WriteAsync("Logged off from all devices");
                });
            });
        }
    }
}