﻿using DatingApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.Infrastructure.Data
{
    public class DataContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<AppUser> Users { get; set; }
        public DbSet<UserLike> Likes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserLike>().HasKey(k => new { k.SourceUserId, k.TargetUserId });
            modelBuilder.Entity<UserLike>().HasOne(s => s.SourceUser).WithMany(l => l.LikedUsers).HasForeignKey(s => s.SourceUserId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<UserLike>().HasOne(s => s.TargetUser).WithMany(l => l.LikedByUsers).HasForeignKey(s => s.TargetUserId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}


//dotnet ef migrations add InitialCreate --project DatingApp.Infrastructure --startup-project DatingApp.Api
//dotnet ef database update --project DatingApp.Infrastructure --startup-project DatingApp.Api