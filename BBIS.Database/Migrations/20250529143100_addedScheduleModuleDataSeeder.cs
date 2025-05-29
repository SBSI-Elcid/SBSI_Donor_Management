using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BBIS.Database.Migrations
{
    public partial class addedScheduleModuleDataSeeder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Module",
                keyColumn: "ModuleId",
                keyValue: new Guid("22a8eb15-168c-470a-a680-71e85b8514c6"),
                column: "OrderNo",
                value: 11);

            migrationBuilder.UpdateData(
                table: "Module",
                keyColumn: "ModuleId",
                keyValue: new Guid("53223248-2221-4d48-a551-8477a33e96c4"),
                column: "OrderNo",
                value: 10);

            migrationBuilder.UpdateData(
                table: "Module",
                keyColumn: "ModuleId",
                keyValue: new Guid("68b94e71-abf0-4989-aa29-9ec6d6419a9c"),
                column: "OrderNo",
                value: 12);

            migrationBuilder.UpdateData(
                table: "Module",
                keyColumn: "ModuleId",
                keyValue: new Guid("7402be5a-bc2f-47b9-a389-b2c72c7173ba"),
                column: "OrderNo",
                value: 6);

            migrationBuilder.UpdateData(
                table: "Module",
                keyColumn: "ModuleId",
                keyValue: new Guid("a0fe13b2-3b07-468f-91e8-b74b26b64f12"),
                column: "OrderNo",
                value: 5);

            migrationBuilder.UpdateData(
                table: "Module",
                keyColumn: "ModuleId",
                keyValue: new Guid("bc8d57ab-af77-4ec3-a348-5d040802f6e3"),
                column: "OrderNo",
                value: 4);

            migrationBuilder.UpdateData(
                table: "Module",
                keyColumn: "ModuleId",
                keyValue: new Guid("be14af28-ad5b-46b3-80a0-9952a98b876e"),
                column: "OrderNo",
                value: 7);

            migrationBuilder.UpdateData(
                table: "Module",
                keyColumn: "ModuleId",
                keyValue: new Guid("bec0b869-882f-4661-8bc1-856f6accca16"),
                column: "OrderNo",
                value: 8);

            migrationBuilder.UpdateData(
                table: "Module",
                keyColumn: "ModuleId",
                keyValue: new Guid("f0fa95f2-2288-434f-8df6-71f00e9be302"),
                column: "OrderNo",
                value: 9);

            migrationBuilder.InsertData(
                table: "Module",
                columns: new[] { "ModuleId", "Icon", "IsActive", "IsParentMenu", "Link", "Menu", "OrderNo", "ParentModuleId" },
                values: new object[] { new Guid("2ba843a3-d26d-4c11-ab66-462e1ea4b491"), "mdi-calendar-arrow-right", true, false, "/schedules", "Schedule Services", 3, null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Module",
                keyColumn: "ModuleId",
                keyValue: new Guid("2ba843a3-d26d-4c11-ab66-462e1ea4b491"));

            migrationBuilder.UpdateData(
                table: "Module",
                keyColumn: "ModuleId",
                keyValue: new Guid("22a8eb15-168c-470a-a680-71e85b8514c6"),
                column: "OrderNo",
                value: 10);

            migrationBuilder.UpdateData(
                table: "Module",
                keyColumn: "ModuleId",
                keyValue: new Guid("53223248-2221-4d48-a551-8477a33e96c4"),
                column: "OrderNo",
                value: 9);

            migrationBuilder.UpdateData(
                table: "Module",
                keyColumn: "ModuleId",
                keyValue: new Guid("68b94e71-abf0-4989-aa29-9ec6d6419a9c"),
                column: "OrderNo",
                value: 11);

            migrationBuilder.UpdateData(
                table: "Module",
                keyColumn: "ModuleId",
                keyValue: new Guid("7402be5a-bc2f-47b9-a389-b2c72c7173ba"),
                column: "OrderNo",
                value: 5);

            migrationBuilder.UpdateData(
                table: "Module",
                keyColumn: "ModuleId",
                keyValue: new Guid("a0fe13b2-3b07-468f-91e8-b74b26b64f12"),
                column: "OrderNo",
                value: 4);

            migrationBuilder.UpdateData(
                table: "Module",
                keyColumn: "ModuleId",
                keyValue: new Guid("bc8d57ab-af77-4ec3-a348-5d040802f6e3"),
                column: "OrderNo",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Module",
                keyColumn: "ModuleId",
                keyValue: new Guid("be14af28-ad5b-46b3-80a0-9952a98b876e"),
                column: "OrderNo",
                value: 6);

            migrationBuilder.UpdateData(
                table: "Module",
                keyColumn: "ModuleId",
                keyValue: new Guid("bec0b869-882f-4661-8bc1-856f6accca16"),
                column: "OrderNo",
                value: 7);

            migrationBuilder.UpdateData(
                table: "Module",
                keyColumn: "ModuleId",
                keyValue: new Guid("f0fa95f2-2288-434f-8df6-71f00e9be302"),
                column: "OrderNo",
                value: 8);
        }
    }
}
