using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BBIS.Database.Migrations
{
    public partial class ChangedDatatypeForVitalSignsMonitoring : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PR",
                table: "VitalSignsMonitoring",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "double")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "BP",
                table: "VitalSignsMonitoring",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "double")
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "PR",
                table: "VitalSignsMonitoring",
                type: "double",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<double>(
                name: "BP",
                table: "VitalSignsMonitoring",
                type: "double",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
