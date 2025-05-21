using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BBIS.Database.Migrations
{
    public partial class ChangesToInitialScreening : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BloodType",
                table: "DonorInitialScreening");

            migrationBuilder.DropColumn(
                name: "DonationType",
                table: "DonorInitialScreening");

            migrationBuilder.DropColumn(
                name: "InHouseTypeValue",
                table: "DonorInitialScreening");

            migrationBuilder.DropColumn(
                name: "MobileBloodDonationOrganizer",
                table: "DonorInitialScreening");

            migrationBuilder.DropColumn(
                name: "MobileBloodDonationPlace",
                table: "DonorInitialScreening");

            migrationBuilder.DropColumn(
                name: "NameOfPatient",
                table: "DonorInitialScreening");

            migrationBuilder.DropColumn(
                name: "PatientBloodType",
                table: "DonorInitialScreening");

            migrationBuilder.DropColumn(
                name: "PatientHospital",
                table: "DonorInitialScreening");

            migrationBuilder.DropColumn(
                name: "PatientNoOfUnits",
                table: "DonorInitialScreening");

            migrationBuilder.DropColumn(
                name: "PatientWBOrComponent",
                table: "DonorInitialScreening");

            migrationBuilder.DropColumn(
                name: "PrcOffice",
                table: "DonorInitialScreening");

            migrationBuilder.DropColumn(
                name: "SPGR",
                table: "DonorInitialScreening");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "DonorInitialScreening");

            migrationBuilder.AddColumn<string>(
                name: "MethodOfBloodCollection",
                table: "DonorInitialScreening",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MethodOfBloodCollection",
                table: "DonorInitialScreening");

            migrationBuilder.AddColumn<string>(
                name: "BloodType",
                table: "DonorInitialScreening",
                type: "varchar(10)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "DonationType",
                table: "DonorInitialScreening",
                type: "varchar(25)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "InHouseTypeValue",
                table: "DonorInitialScreening",
                type: "varchar(20)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "MobileBloodDonationOrganizer",
                table: "DonorInitialScreening",
                type: "varchar(50)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "MobileBloodDonationPlace",
                table: "DonorInitialScreening",
                type: "varchar(100)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "NameOfPatient",
                table: "DonorInitialScreening",
                type: "varchar(100)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "PatientBloodType",
                table: "DonorInitialScreening",
                type: "varchar(5)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "PatientHospital",
                table: "DonorInitialScreening",
                type: "varchar(100)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "PatientNoOfUnits",
                table: "DonorInitialScreening",
                type: "varchar(10)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "PatientWBOrComponent",
                table: "DonorInitialScreening",
                type: "varchar(10)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "PrcOffice",
                table: "DonorInitialScreening",
                type: "varchar(70)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<double>(
                name: "SPGR",
                table: "DonorInitialScreening",
                type: "double",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Weight",
                table: "DonorInitialScreening",
                type: "double",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
