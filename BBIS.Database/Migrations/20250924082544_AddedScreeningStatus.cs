using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BBIS.Database.Migrations
{
    public partial class AddedScreeningStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ScreeningStatus",
                table: "UserRoleScreeningAccess",
                type: "VARCHAR(255)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Module",
                keyColumn: "ModuleId",
                keyValue: new Guid("d4a1b8a7-14f3-4c6d-9fd2-1e7b8c4f0b93"),
                column: "IsParentMenu",
                value: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ScreeningStatus",
                table: "UserRoleScreeningAccess");

            migrationBuilder.UpdateData(
                table: "Module",
                keyColumn: "ModuleId",
                keyValue: new Guid("d4a1b8a7-14f3-4c6d-9fd2-1e7b8c4f0b93"),
                column: "IsParentMenu",
                value: true);
        }
    }
}
