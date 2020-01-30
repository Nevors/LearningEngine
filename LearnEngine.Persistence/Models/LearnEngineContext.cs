﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningEngine.Persistence.Models
{
    public class LearnEngineContext: DbContext
    {
        public DbSet<Card> Cards { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Theme> Themes { get; set; }

        public LearnEngineContext(DbContextOptions<LearnEngineContext> options): base(options)
        {

        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=LearnEngineDb;Trusted_Connection=true");
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Permission>()
                .HasOne(prm => prm.User)
                .WithMany(usr => usr.Permissions)
                .HasForeignKey(prm => prm.UserId);
            modelBuilder.Entity<Permission>()
                .HasOne(prm => prm.Theme)
                .WithMany(thm => thm.Permissions)
                .HasForeignKey(prm => prm.ThemeId);

            modelBuilder.Entity<Theme>()
                .HasOne(thm => thm.ParentTheme)
                .WithMany(thm => thm.SubThemes)
                .HasForeignKey(thm => thm.ParentThemeId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<User>()
                .HasIndex(usr => usr.UserName)
                .IsUnique();
            modelBuilder.Entity<User>()
                .HasIndex(usr => usr.Email)
                .IsUnique();
            modelBuilder.Entity<User>()
                .HasData(new User
                {
                    Id = 1,
                    Email = "rolit@mail.cor",
                    UserName = "rolit",
                    Password = "123"
                });
        }
    }
}