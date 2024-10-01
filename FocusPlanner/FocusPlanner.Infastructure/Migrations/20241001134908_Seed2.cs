using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FocusPlanner.Infastructure.Migrations
{
    /// <inheritdoc />
    public partial class Seed2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Work" },
                    { 2, "Personal" },
                    { 3, "Fitness" }
                });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "Id", "CategoryId", "Description", "DueDate", "IsCompleted", "Title" },
                values: new object[,]
                {
                    { 1, 1, "Finish the FocusPlanner project", new DateTime(2024, 10, 4, 15, 49, 8, 212, DateTimeKind.Local).AddTicks(6998), false, "Complete Project" },
                    { 2, 3, "Complete full body workout", new DateTime(2024, 10, 2, 15, 49, 8, 212, DateTimeKind.Local).AddTicks(7032), false, "Gym Workout" }
                });

            migrationBuilder.InsertData(
                table: "Reminders",
                columns: new[] { "Id", "ReminderTime", "TaskId" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 10, 4, 14, 49, 8, 212, DateTimeKind.Local).AddTicks(7051), 1 },
                    { 2, new DateTime(2024, 10, 2, 13, 49, 8, 212, DateTimeKind.Local).AddTicks(7054), 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Reminders",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Reminders",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
