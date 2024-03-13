using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using AssignmentAPI.Models;
namespace AssignmentAPI.Shared
{
    public class AssignmentDBContext : DbContext
    {
        public AssignmentDBContext(DbContextOptions<AssignmentDBContext> options) : base(options)
        {
        }

        public  DbSet<LevelModel>  levels { get; set; }
        public  DbSet<BuildingModel> Buildings { get; set; }
        public  DbSet<RoomModel> Rooms { get; set; }
        public  DbSet<VisitorsModel> Visitors { get; set; }
        public  DbSet<UserModel> Users { get; set; }
        public  DbSet<GuestAccessModel> GuestAccess { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GuestAccessModel>(entity =>
            {
                entity.HasKey(e => e.GuestAccessId)
                    .HasName("PRIMARY");
            });

            modelBuilder.Entity<UserModel>(entity=>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PRIMARY");
            });

            modelBuilder.Entity<BuildingModel>(entity =>
            {
                entity.HasKey(e => e.BuildingId)
                    .HasName("PRIMARY");

            });

            modelBuilder.Entity<LevelModel>(entity =>
            {
                entity.HasKey(e => e.LevelId)
                    .HasName("PRIMARY");

                entity.HasOne(d => d.Building)
                   .WithMany(p => p.Levels)
                   .HasForeignKey(d => d.BuildingId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .HasConstraintName("FK_LevelModel_BuildingModel");
            });

            modelBuilder.Entity<RoomModel>(entity =>
            {
                entity.HasKey(e => e.RoomId)
                    .HasName("PRIMARY");

                entity.HasOne(d => d.Levels)
                    .WithMany(p => p.Rooms)
                    .HasForeignKey(d => d.LevelId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Room_Level");
            });



            modelBuilder.Entity<VisitorsModel>(entity =>
            {
                entity.HasKey(e => e.VisitorId).HasName("PRIMARY");
                entity.Property(e => e.VisitorId).ValueGeneratedNever();

                entity.HasOne(e => e.Building)
                    .WithMany()
                    .HasForeignKey(e => e.BuildingId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Level)
                    .WithMany()
                    .HasForeignKey(e => e.LevelId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Room)
                    .WithMany()
                    .HasForeignKey(e => e.RoomId)
                    .OnDelete(DeleteBehavior.Restrict);
            });


            base.OnModelCreating(modelBuilder);
        }

    }
}

