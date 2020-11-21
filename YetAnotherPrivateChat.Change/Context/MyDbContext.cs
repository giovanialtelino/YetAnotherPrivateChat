using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using YetAnotherPrivateChat.Shared;
using YetAnotherPrivateChat.Shared.HelperShared.JWT;

namespace YetAnotherPrivateChat.Change.Context
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
            modelbuilder.Entity<Message>().ToTable("Message");
            modelbuilder.Entity<Message>(entity =>
            {
                entity.HasKey(e => e.MessageId);
                entity.Property(e => e.MessageId).ValueGeneratedOnAdd();
            });

            modelbuilder.Entity<Quote>().ToTable("Quote");
            modelbuilder.Entity<Quote>(entity =>
            {
                entity.HasKey(e => e.MessageId);
                entity.Property(e => e.MessageId).ValueGeneratedOnAdd();
            });

            modelbuilder.Entity<Reply>().ToTable("Reply");
            modelbuilder.Entity<Reply>(entity =>
            {
                entity.HasKey(e => e.ReplyId);
                entity.Property(e => e.ReplyId).ValueGeneratedOnAdd();
            });

            modelbuilder.Entity<Room>().ToTable("Room");
            modelbuilder.Entity<Room>(entity =>
            {
                entity.HasKey(e => e.RoomID);
                entity.Property(e => e.RoomID).ValueGeneratedOnAdd();
            });
        }

        public DbSet<Message> Messages { get; set; }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<Reply> Replies { get; set; }
        public DbSet<Room> Rooms { get; set; }
    }
}