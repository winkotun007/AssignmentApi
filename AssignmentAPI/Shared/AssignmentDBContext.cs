using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using AssignmentAPI.Models;
using Microsoft.Extensions.Hosting;

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



            /// Seeding Data
            ///
            modelBuilder.Entity<UserModel>().HasData(
                new UserModel { UserId = Guid.NewGuid().ToString(), UserName = "admin", FristName = "Win Ko", LastName = "Htun" ,Password= "SawMrF4MIPLybRhUuydNLnFedhTP2TqS", PasswordSalt= "17urfIO+0X9aVngltY8OCc7mJXkkFOqz" }
            );

            modelBuilder.Entity<BuildingModel>().HasData(
                new BuildingModel { BuildingId = "1", BuildingCode = "Oscar", BuildingName = "Oscar"},
                 new BuildingModel { BuildingId = "2", BuildingCode = "Hira", BuildingName = "Hira"}
            );

            modelBuilder.Entity<LevelModel>().HasData(
                new LevelModel { LevelId = "1", LevelName = "Level-1", LevelCode = "Level-1", BuildingId = "1" },
                 new LevelModel { LevelId = "2", LevelName = "Level-2", LevelCode = "Level-2", BuildingId = "2" }
            );

            modelBuilder.Entity<RoomModel>().HasData(
                new RoomModel { RoomId = "1", RoomCode = "R-101", RoomName = "Oscar" ,LevelId="1"},
                 new RoomModel { RoomId = "2", RoomCode = "R-201", RoomName = "Hira" ,LevelId="2"}
            );

            modelBuilder.Entity<GuestAccessModel>().HasData(
                new GuestAccessModel { GuestAccessId = "1",Path= "api/Rooms/GetRoomsByLevel",isGetAccess=true,isPostAccess=true,isPutAccess=true,isDeleteAccess=true },
                new GuestAccessModel { GuestAccessId = "2", Path = "/swagger/index.html", isGetAccess = true, isPostAccess = true, isPutAccess = true, isDeleteAccess = true },
                new GuestAccessModel { GuestAccessId = "3", Path = "/api/User", isGetAccess = true, isPostAccess = true, isPutAccess = true, isDeleteAccess = true },
                new GuestAccessModel { GuestAccessId = "4", Path = "/api/Levels/GetLevelsByBuilding", isGetAccess = true, isPostAccess = true, isPutAccess = true, isDeleteAccess = true},
                new GuestAccessModel { GuestAccessId = "5", Path = "/api/Visitors", isGetAccess = true, isPostAccess = true, isPutAccess = true, isDeleteAccess = true},
                new GuestAccessModel { GuestAccessId = "6", Path = "/api/Rooms", isGetAccess = true, isPostAccess = true, isPutAccess = true, isDeleteAccess = true },
                new GuestAccessModel { GuestAccessId = "7", Path = "/api/Levels", isGetAccess = true, isPostAccess = true, isPutAccess = true, isDeleteAccess = true },
                new GuestAccessModel { GuestAccessId = "8", Path = "/api/Building", isGetAccess = true, isPostAccess = true, isPutAccess = true, isDeleteAccess = true },
                new GuestAccessModel { GuestAccessId = "9", Path = "/swagger/v1/swagger.json", isGetAccess = true, isPostAccess = true, isPutAccess = true, isDeleteAccess = true},
                new GuestAccessModel { GuestAccessId = "10", Path = "/api/User/UserLogin", isGetAccess = true, isPostAccess = true, isPutAccess = true, isDeleteAccess = true },
                new GuestAccessModel { GuestAccessId = "11", Path = "/api/Levels/GetLevelsByBuilding", isGetAccess = true, isPostAccess = true, isPutAccess = true, isDeleteAccess = true }
                );

            modelBuilder.Entity<VisitorsModel>().HasData(
                new VisitorsModel { VisitorId="1",FirstName="win" ,LastName="ko Htun",NRICNumber="14/Test" ,PlateNumber="5H_000",CompanyName="TestCompany"
                                    ,Designation= "Test De",BuildingId="1",LevelId="1",RoomId="1",isAcknowledged=true, isConfirmed14Day=false,isFever=false,isStayHomeNotice=false}
                );

            base.OnModelCreating(modelBuilder);
        }

    }
}

