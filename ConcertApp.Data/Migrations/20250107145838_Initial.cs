using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ConcertApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Concerts",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Concerts", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", maxLength: 36, nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Performances",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Location = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", maxLength: 100, nullable: false),
                    ConcertId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Performances", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Performances_Concerts_ConcertId",
                        column: x => x.ConcertId,
                        principalTable: "Concerts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    PerformanceID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Bookings_Performances_PerformanceID",
                        column: x => x.PerformanceID,
                        principalTable: "Performances",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bookings_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Concerts",
                columns: new[] { "ID", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "A night of amazing rock music.", "Rock Concert" },
                    { 2, "Smooth jazz performances all evening.", "Jazz Night" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "ID", "email", "name", "password" },
                values: new object[,]
                {
                    { 1, "johndoe@example.com", "John Doe", "P@ssw0rd123" },
                    { 2, "janesmith@example.com", "Jane Smith", "Str0ngP@ssword!" }
                });

            migrationBuilder.InsertData(
                table: "Performances",
                columns: new[] { "ID", "ConcertId", "DateTime", "Location", "Name" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2025, 1, 7, 15, 58, 37, 940, DateTimeKind.Utc).AddTicks(7047), "Main Stage", "Opening Act" },
                    { 2, 1, new DateTime(2025, 1, 7, 17, 58, 37, 940, DateTimeKind.Utc).AddTicks(7548), "Main Stage", "Metallica" },
                    { 3, 2, new DateTime(2025, 1, 8, 16, 58, 37, 940, DateTimeKind.Utc).AddTicks(7552), "Jazz Club", "Jazz Ensemble" }
                });

            migrationBuilder.InsertData(
                table: "Bookings",
                columns: new[] { "ID", "Email", "Name", "PerformanceID", "UserId" },
                values: new object[,]
                {
                    { 1, "johndoe@example.com", "John's Rock Booking", 2, 1 },
                    { 2, "janesmith@example.com", "Jane's Jazz Booking", 3, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_PerformanceID",
                table: "Bookings",
                column: "PerformanceID");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_UserId",
                table: "Bookings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Performances_ConcertId",
                table: "Performances",
                column: "ConcertId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "Performances");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Concerts");
        }
    }
}
