using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BBIS.Database.Migrations
{
    public partial class ChangesOnBloodCollectionAndIssuance : Migration
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
                name: "CollectionSubType",
                table: "DonorBloodCollection");

            migrationBuilder.DropColumn(
                name: "CollectionType",
                table: "DonorBloodCollection");

            migrationBuilder.DropColumn(
                name: "MedicationGiven",
                table: "DonorBloodCollection");

            migrationBuilder.DropColumn(
                name: "PatientFirstName",
                table: "DonorBloodBagIssuance");

            migrationBuilder.DropColumn(
                name: "PatientLastName",
                table: "DonorBloodBagIssuance");

            migrationBuilder.DropColumn(
                name: "PatientMiddleName",
                table: "DonorBloodBagIssuance");

            migrationBuilder.AddColumn<string>(
                name: "PatientFirstName",
                table: "DonorBloodCollection",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "PatientLastName",
                table: "DonorBloodCollection",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "PatientMiddleName",
                table: "DonorBloodCollection",
                type: "longtext",
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
                principalColumn: "DonorTransactionId");
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
                name: "PatientFirstName",
                table: "DonorBloodCollection");

            migrationBuilder.DropColumn(
                name: "PatientLastName",
                table: "DonorBloodCollection");

            migrationBuilder.DropColumn(
                name: "PatientMiddleName",
                table: "DonorBloodCollection");

            migrationBuilder.AddColumn<string>(
                name: "CollectionSubType",
                table: "DonorBloodCollection",
                type: "varchar(15)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "CollectionType",
                table: "DonorBloodCollection",
                type: "varchar(15)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "MedicationGiven",
                table: "DonorBloodCollection",
                type: "varchar(100)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "PatientFirstName",
                table: "DonorBloodBagIssuance",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "PatientLastName",
                table: "DonorBloodBagIssuance",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "PatientMiddleName",
                table: "DonorBloodBagIssuance",
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
    }
}
