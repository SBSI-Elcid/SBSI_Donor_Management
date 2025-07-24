using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BBIS.Database.Migrations
{
    public partial class AddedUserRoleAccess : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserRoleScreeningAccess",
                columns: table => new
                {
                    UserRoleScreeningAccessId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    RoleId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ScreeningTabName = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoleScreeningAccess", x => x.UserRoleScreeningAccessId);
                    table.ForeignKey(
                        name: "FK_UserRoleScreeningAccess_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Module",
                keyColumn: "ModuleId",
                keyValue: new Guid("d4a1b8a7-14f3-4c6d-9fd2-1e7b8c4f0b93"),
                column: "IsParentMenu",
                value: false);

            migrationBuilder.CreateIndex(
                name: "IX_UserRoleScreeningAccess_RoleId",
                table: "UserRoleScreeningAccess",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserRoleScreeningAccess");

            migrationBuilder.UpdateData(
                table: "Module",
                keyColumn: "ModuleId",
                keyValue: new Guid("d4a1b8a7-14f3-4c6d-9fd2-1e7b8c4f0b93"),
                column: "IsParentMenu",
                value: false);
        }
    }
}
