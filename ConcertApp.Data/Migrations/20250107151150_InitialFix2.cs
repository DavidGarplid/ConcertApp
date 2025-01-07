using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ConcertApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialFix2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Performances",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Performances",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Performances",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Concerts",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Concerts",
                keyColumn: "ID",
                keyValue: 2);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                    { 1, 1, new DateTime(2025, 1, 7, 16, 4, 57, 729, DateTimeKind.Utc).AddTicks(2538), "Main Stage", "Opening Act" },
                    { 2, 1, new DateTime(2025, 1, 7, 18, 4, 57, 729, DateTimeKind.Utc).AddTicks(3057), "Main Stage", "Metallica" },
                    { 3, 2, new DateTime(2025, 1, 8, 17, 4, 57, 729, DateTimeKind.Utc).AddTicks(3062), "Jazz Club", "Jazz Ensemble" }
                });

            migrationBuilder.InsertData(
                table: "Bookings",
                columns: new[] { "ID", "Email", "Name", "PerformanceID", "UserId" },
                values: new object[,]
                {
                    { 1, "johndoe@example.com", "John's Rock Booking", 2, 1 },
                    { 2, "janesmith@example.com", "Jane's Jazz Booking", 3, 2 }
                });
        }
    }
}
