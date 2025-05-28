using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BBIS.Database.Migrations
{
    public partial class addedPostDonationDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PostDonationDetail",
                columns: table => new
                {
                    PostDonationDetailsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DonorPostDonationCareId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Details = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MonitoredBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Others = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostDonationDetail", x => x.PostDonationDetailsId);
                    table.ForeignKey(
                        name: "FK_PostDonationDetail_DonorPostDonationCare_DonorPostDonationCa~",
                        column: x => x.DonorPostDonationCareId,
                        principalTable: "DonorPostDonationCare",
                        principalColumn: "DonorPostDonationCareId");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_PostDonationDetail_DonorPostDonationCareId",
                table: "PostDonationDetail",
                column: "DonorPostDonationCareId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PostDonationDetail");
        }
    }
}
