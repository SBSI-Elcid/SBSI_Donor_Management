using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BBIS.Database.Migrations
{
    public partial class Add_Lookup_Option : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "LookupOption",
                columns: new[] { "LookupOptionId", "IsActive", "LookupId", "Name", "Value" },
                values: new object[] { 64, true, 17, "N/A", "N/A" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "LookupOption",
                keyColumn: "LookupOptionId",
                keyValue: 64);
        }
    }
}
