using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FocusPlanner.Infastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPropertyHasNotified : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasNotified",
                table: "Tasks",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Reminders",
                keyColumn: "Id",
                keyValue: 1,
                column: "ReminderTime",
                value: new DateTime(2024, 11, 3, 9, 33, 57, 55, DateTimeKind.Local).AddTicks(1973));

            migrationBuilder.UpdateData(
                table: "Reminders",
                keyColumn: "Id",
                keyValue: 2,
                column: "ReminderTime",
                value: new DateTime(2024, 11, 1, 8, 33, 57, 55, DateTimeKind.Local).AddTicks(1976));

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DueDate", "FinishDate", "HasNotified", "StartDate" },
                values: new object[] { new DateTime(2024, 11, 4, 3, 33, 57, 55, DateTimeKind.Local).AddTicks(1922), new DateTime(2024, 11, 4, 2, 33, 57, 55, DateTimeKind.Local).AddTicks(1924), false, new DateTime(2024, 11, 1, 19, 33, 57, 55, DateTimeKind.Local).AddTicks(1880) });

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DueDate", "FinishDate", "HasNotified", "StartDate" },
                values: new object[] { new DateTime(2024, 11, 1, 22, 33, 57, 55, DateTimeKind.Local).AddTicks(1928), new DateTime(2024, 11, 1, 23, 33, 57, 55, DateTimeKind.Local).AddTicks(1930), false, new DateTime(2024, 10, 31, 12, 33, 57, 55, DateTimeKind.Local).AddTicks(1927) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasNotified",
                table: "Tasks");

            migrationBuilder.UpdateData(
                table: "Reminders",
                keyColumn: "Id",
                keyValue: 1,
                column: "ReminderTime",
                value: new DateTime(2024, 10, 26, 13, 21, 16, 438, DateTimeKind.Local).AddTicks(8051));

            migrationBuilder.UpdateData(
                table: "Reminders",
                keyColumn: "Id",
                keyValue: 2,
                column: "ReminderTime",
                value: new DateTime(2024, 10, 24, 12, 21, 16, 438, DateTimeKind.Local).AddTicks(8053));

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DueDate", "FinishDate", "StartDate" },
                values: new object[] { new DateTime(2024, 10, 27, 7, 21, 16, 438, DateTimeKind.Local).AddTicks(8025), new DateTime(2024, 10, 27, 6, 21, 16, 438, DateTimeKind.Local).AddTicks(8027), new DateTime(2024, 10, 24, 23, 21, 16, 438, DateTimeKind.Local).AddTicks(7984) });

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DueDate", "FinishDate", "StartDate" },
                values: new object[] { new DateTime(2024, 10, 25, 2, 21, 16, 438, DateTimeKind.Local).AddTicks(8032), new DateTime(2024, 10, 25, 3, 21, 16, 438, DateTimeKind.Local).AddTicks(8034), new DateTime(2024, 10, 23, 16, 21, 16, 438, DateTimeKind.Local).AddTicks(8031) });
        }
    }
}
