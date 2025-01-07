using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConcertApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Performances",
                keyColumn: "ID",
                keyValue: 1,
                column: "DateTime",
                value: new DateTime(2025, 1, 7, 16, 4, 57, 729, DateTimeKind.Utc).AddTicks(2538));

            migrationBuilder.UpdateData(
                table: "Performances",
                keyColumn: "ID",
                keyValue: 2,
                column: "DateTime",
                value: new DateTime(2025, 1, 7, 18, 4, 57, 729, DateTimeKind.Utc).AddTicks(3057));

            migrationBuilder.UpdateData(
                table: "Performances",
                keyColumn: "ID",
                keyValue: 3,
                column: "DateTime",
                value: new DateTime(2025, 1, 8, 17, 4, 57, 729, DateTimeKind.Utc).AddTicks(3062));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Performances",
                keyColumn: "ID",
                keyValue: 1,
                column: "DateTime",
                value: new DateTime(2025, 1, 7, 15, 58, 37, 940, DateTimeKind.Utc).AddTicks(7047));

            migrationBuilder.UpdateData(
                table: "Performances",
                keyColumn: "ID",
                keyValue: 2,
                column: "DateTime",
                value: new DateTime(2025, 1, 7, 17, 58, 37, 940, DateTimeKind.Utc).AddTicks(7548));

            migrationBuilder.UpdateData(
                table: "Performances",
                keyColumn: "ID",
                keyValue: 3,
                column: "DateTime",
                value: new DateTime(2025, 1, 8, 16, 58, 37, 940, DateTimeKind.Utc).AddTicks(7552));
        }
    }
}
