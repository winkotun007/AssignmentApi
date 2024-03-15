using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssignmentAPI.Migrations
{
    public partial class YourMigrationName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Buildings",
                columns: table => new
                {
                    BuildingId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BuildingName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BuildingCode = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    CreatedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UpdatedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.BuildingId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "GuestAccess",
                columns: table => new
                {
                    GuestAccessId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Path = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    isGetAccess = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    isPostAccess = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    isPutAccess = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    isDeleteAccess = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    CreatedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UpdatedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.GuestAccessId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserName = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FristName = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastName = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Password = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PasswordSalt = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    CreatedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UpdatedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.UserId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "levels",
                columns: table => new
                {
                    LevelId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LevelName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LevelCode = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BuildingId = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    CreatedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UpdatedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.LevelId);
                    table.ForeignKey(
                        name: "FK_LevelModel_BuildingModel",
                        column: x => x.BuildingId,
                        principalTable: "Buildings",
                        principalColumn: "BuildingId",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    RoomId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RoomName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RoomCode = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LevelId = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    CreatedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UpdatedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.RoomId);
                    table.ForeignKey(
                        name: "FK_Room_Level",
                        column: x => x.LevelId,
                        principalTable: "levels",
                        principalColumn: "LevelId",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Visitors",
                columns: table => new
                {
                    VisitorId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FirstName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NRICNumber = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PlateNumber = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CompanyName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Designation = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BuildingId = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LevelId = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RoomId = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    isStayHomeNotice = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    isConfirmed14Day = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    isFever = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    isAcknowledged = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    CreatedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UpdatedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.VisitorId);
                    table.ForeignKey(
                        name: "FK_Visitors_Buildings_BuildingId",
                        column: x => x.BuildingId,
                        principalTable: "Buildings",
                        principalColumn: "BuildingId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Visitors_levels_LevelId",
                        column: x => x.LevelId,
                        principalTable: "levels",
                        principalColumn: "LevelId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Visitors_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "RoomId",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Buildings",
                columns: new[] { "BuildingId", "BuildingCode", "BuildingName", "CreatedBy", "CreatedDate", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { "1", "Oscar", "Oscar", "admin", new DateTime(2024, 3, 14, 12, 22, 21, 17, DateTimeKind.Utc).AddTicks(220), "admin", new DateTime(2024, 3, 14, 12, 22, 21, 17, DateTimeKind.Utc).AddTicks(220) },
                    { "2", "Hira", "Hira", "admin", new DateTime(2024, 3, 14, 12, 22, 21, 17, DateTimeKind.Utc).AddTicks(230), "admin", new DateTime(2024, 3, 14, 12, 22, 21, 17, DateTimeKind.Utc).AddTicks(230) }
                });

            migrationBuilder.InsertData(
                table: "GuestAccess",
                columns: new[] { "GuestAccessId", "CreatedBy", "CreatedDate", "Path", "UpdatedBy", "UpdatedDate", "isDeleteAccess", "isGetAccess", "isPostAccess", "isPutAccess" },
                values: new object[,]
                {
                    { "1", "admin", new DateTime(2024, 3, 14, 12, 22, 21, 17, DateTimeKind.Utc).AddTicks(280), "api/Rooms/GetRoomsByLevel", "admin", new DateTime(2024, 3, 14, 12, 22, 21, 17, DateTimeKind.Utc).AddTicks(280), true, true, true, true },
                    { "10", "admin", new DateTime(2024, 3, 14, 12, 22, 21, 17, DateTimeKind.Utc).AddTicks(330), "/api/User/UserLogin", "admin", new DateTime(2024, 3, 14, 12, 22, 21, 17, DateTimeKind.Utc).AddTicks(330), true, true, true, true },
                    { "11", "admin", new DateTime(2024, 3, 14, 12, 22, 21, 17, DateTimeKind.Utc).AddTicks(330), "/api/Levels/GetLevelsByBuilding", "admin", new DateTime(2024, 3, 14, 12, 22, 21, 17, DateTimeKind.Utc).AddTicks(330), true, true, true, true },
                    { "2", "admin", new DateTime(2024, 3, 14, 12, 22, 21, 17, DateTimeKind.Utc).AddTicks(290), "/swagger/index.html", "admin", new DateTime(2024, 3, 14, 12, 22, 21, 17, DateTimeKind.Utc).AddTicks(290), true, true, true, true },
                    { "3", "admin", new DateTime(2024, 3, 14, 12, 22, 21, 17, DateTimeKind.Utc).AddTicks(290), "/api/User", "admin", new DateTime(2024, 3, 14, 12, 22, 21, 17, DateTimeKind.Utc).AddTicks(290), true, true, true, true },
                    { "4", "admin", new DateTime(2024, 3, 14, 12, 22, 21, 17, DateTimeKind.Utc).AddTicks(290), "/api/Levels/GetLevelsByBuilding", "admin", new DateTime(2024, 3, 14, 12, 22, 21, 17, DateTimeKind.Utc).AddTicks(290), true, true, true, true },
                    { "5", "admin", new DateTime(2024, 3, 14, 12, 22, 21, 17, DateTimeKind.Utc).AddTicks(300), "/api/Visitors", "admin", new DateTime(2024, 3, 14, 12, 22, 21, 17, DateTimeKind.Utc).AddTicks(300), true, true, true, true },
                    { "6", "admin", new DateTime(2024, 3, 14, 12, 22, 21, 17, DateTimeKind.Utc).AddTicks(310), "/api/Rooms", "admin", new DateTime(2024, 3, 14, 12, 22, 21, 17, DateTimeKind.Utc).AddTicks(310), true, true, true, true },
                    { "7", "admin", new DateTime(2024, 3, 14, 12, 22, 21, 17, DateTimeKind.Utc).AddTicks(310), "/api/Levels", "admin", new DateTime(2024, 3, 14, 12, 22, 21, 17, DateTimeKind.Utc).AddTicks(310), true, true, true, true },
                    { "8", "admin", new DateTime(2024, 3, 14, 12, 22, 21, 17, DateTimeKind.Utc).AddTicks(310), "/api/Building", "admin", new DateTime(2024, 3, 14, 12, 22, 21, 17, DateTimeKind.Utc).AddTicks(310), true, true, true, true },
                    { "9", "admin", new DateTime(2024, 3, 14, 12, 22, 21, 17, DateTimeKind.Utc).AddTicks(320), "/swagger/v1/swagger.json", "admin", new DateTime(2024, 3, 14, 12, 22, 21, 17, DateTimeKind.Utc).AddTicks(320), true, true, true, true }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CreatedBy", "CreatedDate", "FristName", "LastName", "Password", "PasswordSalt", "UpdatedBy", "UpdatedDate", "UserName" },
                values: new object[] { "b4f0b488-8487-45bd-b2c0-4316a8e23310", "admin", new DateTime(2024, 3, 14, 12, 22, 21, 17, DateTimeKind.Utc).AddTicks(160), "Win Ko", "Htun", "SawMrF4MIPLybRhUuydNLnFedhTP2TqS", "17urfIO+0X9aVngltY8OCc7mJXkkFOqz", "admin", new DateTime(2024, 3, 14, 12, 22, 21, 17, DateTimeKind.Utc).AddTicks(160), "admin" });

            migrationBuilder.InsertData(
                table: "levels",
                columns: new[] { "LevelId", "BuildingId", "CreatedBy", "CreatedDate", "LevelCode", "LevelName", "UpdatedBy", "UpdatedDate" },
                values: new object[] { "1", "1", "admin", new DateTime(2024, 3, 14, 12, 22, 21, 17, DateTimeKind.Utc).AddTicks(240), "Level-1", "Level-1", "admin", new DateTime(2024, 3, 14, 12, 22, 21, 17, DateTimeKind.Utc).AddTicks(240) });

            migrationBuilder.InsertData(
                table: "levels",
                columns: new[] { "LevelId", "BuildingId", "CreatedBy", "CreatedDate", "LevelCode", "LevelName", "UpdatedBy", "UpdatedDate" },
                values: new object[] { "2", "2", "admin", new DateTime(2024, 3, 14, 12, 22, 21, 17, DateTimeKind.Utc).AddTicks(250), "Level-2", "Level-2", "admin", new DateTime(2024, 3, 14, 12, 22, 21, 17, DateTimeKind.Utc).AddTicks(250) });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "RoomId", "CreatedBy", "CreatedDate", "LevelId", "RoomCode", "RoomName", "UpdatedBy", "UpdatedDate" },
                values: new object[] { "1", "admin", new DateTime(2024, 3, 14, 12, 22, 21, 17, DateTimeKind.Utc).AddTicks(260), "1", "R-101", "Oscar", "admin", new DateTime(2024, 3, 14, 12, 22, 21, 17, DateTimeKind.Utc).AddTicks(260) });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "RoomId", "CreatedBy", "CreatedDate", "LevelId", "RoomCode", "RoomName", "UpdatedBy", "UpdatedDate" },
                values: new object[] { "2", "admin", new DateTime(2024, 3, 14, 12, 22, 21, 17, DateTimeKind.Utc).AddTicks(270), "2", "R-201", "Hira", "admin", new DateTime(2024, 3, 14, 12, 22, 21, 17, DateTimeKind.Utc).AddTicks(270) });

            migrationBuilder.InsertData(
                table: "Visitors",
                columns: new[] { "VisitorId", "BuildingId", "CompanyName", "CreatedBy", "CreatedDate", "Designation", "FirstName", "LastName", "LevelId", "NRICNumber", "PlateNumber", "RoomId", "UpdatedBy", "UpdatedDate", "isAcknowledged", "isConfirmed14Day", "isFever", "isStayHomeNotice" },
                values: new object[] { "1", "1", "TestCompany", "admin", new DateTime(2024, 3, 14, 12, 22, 21, 17, DateTimeKind.Utc).AddTicks(350), "Test De", "win", "ko Htun", "1", "14/Test", "5H_000", "1", "admin", new DateTime(2024, 3, 14, 12, 22, 21, 17, DateTimeKind.Utc).AddTicks(350), true, false, false, false });

            migrationBuilder.CreateIndex(
                name: "IX_levels_BuildingId",
                table: "levels",
                column: "BuildingId");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_LevelId",
                table: "Rooms",
                column: "LevelId");

            migrationBuilder.CreateIndex(
                name: "IX_Visitors_BuildingId",
                table: "Visitors",
                column: "BuildingId");

            migrationBuilder.CreateIndex(
                name: "IX_Visitors_LevelId",
                table: "Visitors",
                column: "LevelId");

            migrationBuilder.CreateIndex(
                name: "IX_Visitors_RoomId",
                table: "Visitors",
                column: "RoomId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GuestAccess");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Visitors");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropTable(
                name: "levels");

            migrationBuilder.DropTable(
                name: "Buildings");
        }
    }
}
