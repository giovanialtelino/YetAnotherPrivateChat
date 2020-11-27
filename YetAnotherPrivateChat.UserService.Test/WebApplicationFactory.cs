using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using YetAnotherPrivateChat.UserService.Context;

namespace YetAnotherPrivateChat.UserService.Test
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            string dbString = "Host=localhost;Database=privatechattest;User Id=admin;Password=patoverde1";

            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                                d => d.ServiceType == typeof(DbContextOptions<MyDbContext>));

                services.Remove(descriptor);

                services.AddDbContext<MyDbContext>(options =>
                {
                    options.UseNpgsql(dbString);
                });

                var sp = services.BuildServiceProvider();

                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<MyDbContext>();
                    var logger = scopedServices.GetRequiredService<ILogger<WebApplicationFactory<TStartup>>>();

                    db.Database.EnsureDeleted();
                    db.Database.EnsureCreated();

                    // try
                    // {
                    //     Utilities.InitializeDbForTests(db);
                    // }
                    // catch (Exception ex)
                    // {

                    //     logger.LogError(ex, "An error occurred seeding the " +
                    //         "database with test messages. Error: {Message}", ex.Message);
                    // }
                }
            });
        }
    }
}