using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using YetAnotherPrivateChat.Shared.UserClass;

namespace YetAnotherPrivateChat.UserService.Context
{
    public class MyDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //string dbString = Environment.GetEnvironmentVariable("ConnectionString");
            string dbString = "Host=localhost;Database=privatechat;User Id=admin;Password=patoverde1";

            optionsBuilder.UseNpgsql(dbString);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            modelbuilder.Entity<User>().ToTable("User");
            modelbuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserId);
                entity.Property(e => e.UserId).ValueGeneratedOnAdd();
            });
        }

        public DbSet<User> Users { get; set; }
    }
}