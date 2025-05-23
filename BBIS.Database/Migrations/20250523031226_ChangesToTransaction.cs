using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BBIS.Database.Migrations
{
    public partial class ChangesToTransaction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DonorBloodBagIssuance_DonorTransaction_DonorTransactionId",
                table: "DonorBloodBagIssuance");

            migrationBuilder.DropIndex(
                name: "IX_DonorBloodBagIssuance_DonorTransactionId",
                table: "DonorBloodBagIssuance");

            migrationBuilder.DropColumn(
                name: "DOHNBBNetsBarcode",
                table: "DonorTransaction");

            migrationBuilder.DropColumn(
                name: "FinalBloodType",
                table: "DonorTransaction");

            migrationBuilder.DropColumn(
                name: "PRCBloodDonorNumber",
                table: "DonorTransaction");

            migrationBuilder.DropColumn(
                name: "UnitSerialNumber",
                table: "DonorTransaction");

            migrationBuilder.AddColumn<string>(
                name: "BloodType",
                table: "DonorTransaction",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "SegmentSerialNumber",
                table: "DonorTransaction",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_DonorBloodBagIssuance_DonorTransactionId",
                table: "DonorBloodBagIssuance",
                column: "DonorTransactionId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DonorBloodBagIssuance_DonorTransaction_DonorTransactionId",
                table: "DonorBloodBagIssuance",
                column: "DonorTransactionId",
                principalTable: "DonorTransaction",
                principalColumn: "DonorTransactionId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DonorBloodBagIssuance_DonorTransaction_DonorTransactionId",
                table: "DonorBloodBagIssuance");

            migrationBuilder.DropIndex(
                name: "IX_DonorBloodBagIssuance_DonorTransactionId",
                table: "DonorBloodBagIssuance");

            migrationBuilder.DropColumn(
                name: "BloodType",
                table: "DonorTransaction");

            migrationBuilder.DropColumn(
                name: "SegmentSerialNumber",
                table: "DonorTransaction");

            migrationBuilder.AddColumn<string>(
                name: "DOHNBBNetsBarcode",
                table: "DonorTransaction",
                type: "varchar(30)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "FinalBloodType",
                table: "DonorTransaction",
                type: "varchar(4)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "PRCBloodDonorNumber",
                table: "DonorTransaction",
                type: "varchar(30)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "UnitSerialNumber",
                table: "DonorTransaction",
                type: "varchar(30)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_DonorBloodBagIssuance_DonorTransactionId",
                table: "DonorBloodBagIssuance",
                column: "DonorTransactionId");

            migrationBuilder.AddForeignKey(
                name: "FK_DonorBloodBagIssuance_DonorTransaction_DonorTransactionId",
                table: "DonorBloodBagIssuance",
                column: "DonorTransactionId",
                principalTable: "DonorTransaction",
                principalColumn: "DonorTransactionId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
