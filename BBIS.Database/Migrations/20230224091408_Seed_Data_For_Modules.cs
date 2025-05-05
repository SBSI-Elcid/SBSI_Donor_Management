using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BBIS.Database.Migrations
{
    public partial class Seed_Data_For_Modules : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Module",
                columns: new[] { "ModuleId", "Icon", "IsActive", "IsParentMenu", "Link", "Menu", "OrderNo", "ParentModuleId" },
                values: new object[,]
                {
                    { new Guid("11826fd9-ebd9-47de-be2c-fb6a355eb228"), "mdi-account-group-outline", true, false, "/donors", "Donors", 2, null },
                    { new Guid("22a8eb15-168c-470a-a680-71e85b8514c6"), "mdi-cogs", true, false, "/app-settings/unit-of-measurement", "App Setting", 10, new Guid("f0fa95f2-2288-434f-8df6-71f00e9be302") },
                    { new Guid("3f5db581-1c6f-46b0-a7dd-240fb5b47d98"), "mdi-account-details-outline", true, false, "/registrations", "Registrations", 1, null },
                    { new Guid("53223248-2221-4d48-a551-8477a33e96c4"), "mdi-account-cog-outline", true, false, "/users", "Users", 9, new Guid("f0fa95f2-2288-434f-8df6-71f00e9be302") },
                    { new Guid("68b94e71-abf0-4989-aa29-9ec6d6419a9c"), "mdi-draw", true, false, "/signatories", "Signatories", 11, new Guid("f0fa95f2-2288-434f-8df6-71f00e9be302") },
                    { new Guid("7402be5a-bc2f-47b9-a389-b2c72c7173ba"), "mdi-account-injury", true, false, "/patients", "Patients", 5, null },
                    { new Guid("a0fe13b2-3b07-468f-91e8-b74b26b64f12"), "mdi-form-select", true, false, "/requisitions/availability", "Requisitions", 4, null },
                    { new Guid("bc8d57ab-af77-4ec3-a348-5d040802f6e3"), "mdi-hand-coin-outline", true, false, "/orders/donors", "Test Orders", 3, null },
                    { new Guid("be14af28-ad5b-46b3-80a0-9952a98b876e"), "mdi-table", true, false, "/inventory", "Inventory", 6, null },
                    { new Guid("bec0b869-882f-4661-8bc1-856f6accca16"), "mdi-chart-bar-stacked", true, false, "/reports", "Reports", 7, null },
                    { new Guid("f0fa95f2-2288-434f-8df6-71f00e9be302"), "mdi-cog", true, true, "/setting", "Setting", 8, null }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Module",
                keyColumn: "ModuleId",
                keyValue: new Guid("11826fd9-ebd9-47de-be2c-fb6a355eb228"));

            migrationBuilder.DeleteData(
                table: "Module",
                keyColumn: "ModuleId",
                keyValue: new Guid("22a8eb15-168c-470a-a680-71e85b8514c6"));

            migrationBuilder.DeleteData(
                table: "Module",
                keyColumn: "ModuleId",
                keyValue: new Guid("3f5db581-1c6f-46b0-a7dd-240fb5b47d98"));

            migrationBuilder.DeleteData(
                table: "Module",
                keyColumn: "ModuleId",
                keyValue: new Guid("53223248-2221-4d48-a551-8477a33e96c4"));

            migrationBuilder.DeleteData(
                table: "Module",
                keyColumn: "ModuleId",
                keyValue: new Guid("68b94e71-abf0-4989-aa29-9ec6d6419a9c"));

            migrationBuilder.DeleteData(
                table: "Module",
                keyColumn: "ModuleId",
                keyValue: new Guid("7402be5a-bc2f-47b9-a389-b2c72c7173ba"));

            migrationBuilder.DeleteData(
                table: "Module",
                keyColumn: "ModuleId",
                keyValue: new Guid("a0fe13b2-3b07-468f-91e8-b74b26b64f12"));

            migrationBuilder.DeleteData(
                table: "Module",
                keyColumn: "ModuleId",
                keyValue: new Guid("bc8d57ab-af77-4ec3-a348-5d040802f6e3"));

            migrationBuilder.DeleteData(
                table: "Module",
                keyColumn: "ModuleId",
                keyValue: new Guid("be14af28-ad5b-46b3-80a0-9952a98b876e"));

            migrationBuilder.DeleteData(
                table: "Module",
                keyColumn: "ModuleId",
                keyValue: new Guid("bec0b869-882f-4661-8bc1-856f6accca16"));

            migrationBuilder.DeleteData(
                table: "Module",
                keyColumn: "ModuleId",
                keyValue: new Guid("f0fa95f2-2288-434f-8df6-71f00e9be302"));
        }
    }
}
