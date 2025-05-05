using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BBIS.Database.Migrations
{
    public partial class Update_role_DonorEvent_to_MedTech : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: new Guid("c3f8818a-7a67-4d54-ba8e-3a5bd247e730"),
                column: "RoleName",
                value: "MedTech");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: new Guid("c3f8818a-7a67-4d54-ba8e-3a5bd247e730"),
                column: "RoleName",
                value: "DonorEvent");
        }
    }
}
