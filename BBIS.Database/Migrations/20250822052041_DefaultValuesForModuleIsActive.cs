using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BBIS.Database.Migrations
{
    public partial class DefaultValuesForModuleIsActive : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ScreeningTabName",
                table: "UserRoleScreeningAccess",
                type: "VARCHAR(50)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(50)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Module",
                keyColumn: "ModuleId",
                keyValue: new Guid("68b94e71-abf0-4989-aa29-9ec6d6419a9c"),
                column: "IsActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "Module",
                keyColumn: "ModuleId",
                keyValue: new Guid("7402be5a-bc2f-47b9-a389-b2c72c7173ba"),
                column: "IsActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "Module",
                keyColumn: "ModuleId",
                keyValue: new Guid("a0fe13b2-3b07-468f-91e8-b74b26b64f12"),
                column: "IsActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "Module",
                keyColumn: "ModuleId",
                keyValue: new Guid("bc8d57ab-af77-4ec3-a348-5d040802f6e3"),
                column: "IsActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "Module",
                keyColumn: "ModuleId",
                keyValue: new Guid("be14af28-ad5b-46b3-80a0-9952a98b876e"),
                column: "IsActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "Module",
                keyColumn: "ModuleId",
                keyValue: new Guid("d4a1b8a7-14f3-4c6d-9fd2-1e7b8c4f0b93"),
                column: "IsParentMenu",
                value: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "UserRoleScreeningAccess",
                keyColumn: "ScreeningTabName",
                keyValue: null,
                column: "ScreeningTabName",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "ScreeningTabName",
                table: "UserRoleScreeningAccess",
                type: "VARCHAR(50)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(50)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Module",
                keyColumn: "ModuleId",
                keyValue: new Guid("68b94e71-abf0-4989-aa29-9ec6d6419a9c"),
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Module",
                keyColumn: "ModuleId",
                keyValue: new Guid("7402be5a-bc2f-47b9-a389-b2c72c7173ba"),
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Module",
                keyColumn: "ModuleId",
                keyValue: new Guid("a0fe13b2-3b07-468f-91e8-b74b26b64f12"),
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Module",
                keyColumn: "ModuleId",
                keyValue: new Guid("bc8d57ab-af77-4ec3-a348-5d040802f6e3"),
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Module",
                keyColumn: "ModuleId",
                keyValue: new Guid("be14af28-ad5b-46b3-80a0-9952a98b876e"),
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Module",
                keyColumn: "ModuleId",
                keyValue: new Guid("d4a1b8a7-14f3-4c6d-9fd2-1e7b8c4f0b93"),
                column: "IsParentMenu",
                value: false);
        }
    }
}
