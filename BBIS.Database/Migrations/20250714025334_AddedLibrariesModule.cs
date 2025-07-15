using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BBIS.Database.Migrations
{
    public partial class AddedLibrariesModule : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Module",
                columns: new[] { "ModuleId", "Icon", "IsActive", "IsParentMenu", "Link", "Menu", "OrderNo", "ParentModuleId" },
                values: new object[] { new Guid("d4a1b8a7-14f3-4c6d-9fd2-1e7b8c4f0b93"), "mdi-library", true, false, "/libraries", "Libraries", 13, new Guid("f0fa95f2-2288-434f-8df6-71f00e9be302") });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "RoleId", "RoleName" },
                values: new object[,]
                {
                    { new Guid("6bd4aadf-4a7c-11f0-82bc-1c697a39b59b"), "VitalSigns" },
                    { new Guid("6c5fac31-4a7c-11f0-82bc-1c697a39b59b"), "Counselor" },
                    { new Guid("aa2ed04a-4a7b-11f0-82bc-1c697a39b59b"), "MethodBloodCollection" },
                    { new Guid("b20c470c-4a7b-11f0-82bc-1c697a39b59b"), "IssuanceOfBloodBag" },
                    { new Guid("c7b02c83-4a7b-11f0-82bc-1c697a39b59b"), "PostDonationCare" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Module",
                keyColumn: "ModuleId",
                keyValue: new Guid("d4a1b8a7-14f3-4c6d-9fd2-1e7b8c4f0b93"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: new Guid("6bd4aadf-4a7c-11f0-82bc-1c697a39b59b"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: new Guid("6c5fac31-4a7c-11f0-82bc-1c697a39b59b"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: new Guid("aa2ed04a-4a7b-11f0-82bc-1c697a39b59b"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: new Guid("b20c470c-4a7b-11f0-82bc-1c697a39b59b"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: new Guid("c7b02c83-4a7b-11f0-82bc-1c697a39b59b"));
        }
    }
}
