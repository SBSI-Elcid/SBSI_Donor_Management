using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BBIS.Database.Migrations
{
    public partial class AddedPostDonationCareAndVitalSignsMonitoring : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DonorPostDonationCare",
                columns: table => new
                {
                    DonorPostDonationCareId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    DonorTransactionId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    TypeOfReaction = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SeverityOfReaction = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ReactionManifestation = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ActionInterventions = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DoctorsNote = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DischargeStatus = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PostDonationMonitoringId = table.Column<int>(type: "int", nullable: false),
                    MonitoredBy = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    DoctorName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DischargeDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonorPostDonationCare", x => x.DonorPostDonationCareId);
                    table.ForeignKey(
                        name: "FK_DonorPostDonationCare_DonorTransaction_DonorTransactionId",
                        column: x => x.DonorTransactionId,
                        principalTable: "DonorTransaction",
                        principalColumn: "DonorTransactionId");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "VitalSignsMonitoring",
                columns: table => new
                {
                    VitalSignsMonitoringId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DonorPostDonationCareId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Time = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    BP = table.Column<double>(type: "double", nullable: false),
                    PR = table.Column<double>(type: "double", nullable: false),
                    Others = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VitalSignsMonitoring", x => x.VitalSignsMonitoringId);
                    table.ForeignKey(
                        name: "FK_VitalSignsMonitoring_DonorPostDonationCare_DonorPostDonation~",
                        column: x => x.DonorPostDonationCareId,
                        principalTable: "DonorPostDonationCare",
                        principalColumn: "DonorPostDonationCareId");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_DonorPostDonationCare_DonorTransactionId",
                table: "DonorPostDonationCare",
                column: "DonorTransactionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VitalSignsMonitoring_DonorPostDonationCareId",
                table: "VitalSignsMonitoring",
                column: "DonorPostDonationCareId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VitalSignsMonitoring");

            migrationBuilder.DropTable(
                name: "DonorPostDonationCare");
        }
    }
}
