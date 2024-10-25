using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FocusPlanner.Infastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPropertystartenddate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DueDate",
                table: "Tasks",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<DateTime>(
                name: "FinishDate",
                table: "Tasks",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Tasks",
                type: "datetime2",
                nullable: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FinishDate",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Tasks");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DueDate",
                table: "Tasks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Reminders",
                keyColumn: "Id",
                keyValue: 1,
                column: "ReminderTime",
                value: new DateTime(2024, 10, 16, 13, 10, 54, 978, DateTimeKind.Local).AddTicks(5941));

            migrationBuilder.UpdateData(
                table: "Reminders",
                keyColumn: "Id",
                keyValue: 2,
                column: "ReminderTime",
                value: new DateTime(2024, 10, 14, 12, 10, 54, 978, DateTimeKind.Local).AddTicks(5944));

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 1,
                column: "DueDate",
                value: new DateTime(2024, 10, 16, 14, 10, 54, 978, DateTimeKind.Local).AddTicks(5882));

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 2,
                column: "DueDate",
                value: new DateTime(2024, 10, 14, 14, 10, 54, 978, DateTimeKind.Local).AddTicks(5920));
        }
    }
}
