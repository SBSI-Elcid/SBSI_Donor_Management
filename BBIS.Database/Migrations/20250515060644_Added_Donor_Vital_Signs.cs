using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BBIS.Database.Migrations
{
    public partial class Added_Donor_Vital_Signs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BloodBagType",
                table: "DonorPhysicalExamination");

            migrationBuilder.DropColumn(
                name: "BloodPressure",
                table: "DonorPhysicalExamination");

            migrationBuilder.DropColumn(
                name: "DoctorName",
                table: "DonorPhysicalExamination");

            migrationBuilder.DropColumn(
                name: "FailedRemarks",
                table: "DonorPhysicalExamination");

            migrationBuilder.DropColumn(
                name: "Pulse",
                table: "DonorPhysicalExamination");

            migrationBuilder.DropColumn(
                name: "ResultStatus",
                table: "DonorPhysicalExamination");

            migrationBuilder.DropColumn(
                name: "Temperature",
                table: "DonorPhysicalExamination");

            migrationBuilder.AddColumn<string>(
                name: "QuestionBisayaText",
                table: "MedicalQuestionnaire",
                type: "varchar(500)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DonorVitalSigns",
                columns: table => new
                {
                    DonorVitalSignsId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    DonorTransactionId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    BodyWeight = table.Column<double>(type: "double", nullable: false),
                    Temperature = table.Column<double>(type: "double", nullable: false),
                    BloodPressure = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PulseRate = table.Column<double>(type: "double", nullable: false),
                    RespiratoryRate = table.Column<double>(type: "double", nullable: false),
                    OxygenSaturation = table.Column<double>(type: "double", nullable: false),
                    FacilitatedBy = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    DateOfExamination = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonorVitalSigns", x => x.DonorVitalSignsId);
                    table.ForeignKey(
                        name: "FK_DonorVitalSigns_DonorTransaction_DonorTransactionId",
                        column: x => x.DonorTransactionId,
                        principalTable: "DonorTransaction",
                        principalColumn: "DonorTransactionId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_DonorVitalSigns_DonorTransactionId",
                table: "DonorVitalSigns",
                column: "DonorTransactionId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DonorVitalSigns");

            migrationBuilder.DropColumn(
                name: "QuestionBisayaText",
                table: "MedicalQuestionnaire");

            migrationBuilder.AddColumn<string>(
                name: "BloodBagType",
                table: "DonorPhysicalExamination",
                type: "varchar(15)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "BloodPressure",
                table: "DonorPhysicalExamination",
                type: "varchar(10)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "DoctorName",
                table: "DonorPhysicalExamination",
                type: "varchar(60)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "FailedRemarks",
                table: "DonorPhysicalExamination",
                type: "varchar(200)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<double>(
                name: "Pulse",
                table: "DonorPhysicalExamination",
                type: "double",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "ResultStatus",
                table: "DonorPhysicalExamination",
                type: "varchar(20)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<double>(
                name: "Temperature",
                table: "DonorPhysicalExamination",
                type: "double",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
