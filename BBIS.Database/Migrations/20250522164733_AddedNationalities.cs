using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BBIS.Database.Migrations
{
    public partial class AddedNationalities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DonorBloodBagIssuance",
                columns: table => new
                {
                    DonorBloodBagIssuanceId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    DonorTransactionId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    BloodBagToBeUsed = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BloodBagType = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UnitSerialNumber = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SegmentSerialNumber = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PatientFirstName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PatientLastName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PatientMiddleName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IssuedBy = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    IssuedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonorBloodBagIssuance", x => x.DonorBloodBagIssuanceId);
                    table.ForeignKey(
                        name: "FK_DonorBloodBagIssuance_DonorTransaction_DonorTransactionId",
                        column: x => x.DonorTransactionId,
                        principalTable: "DonorTransaction",
                        principalColumn: "DonorTransactionId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Lookup",
                columns: new[] { "LookupId", "Description", "IsActive", "LookupKey" },
                values: new object[] { 19, "Nationalities", true, "lookup.Nationalities" });

            migrationBuilder.InsertData(
                table: "LookupOption",
                columns: new[] { "LookupOptionId", "IsActive", "LookupId", "Name", "Value" },
                values: new object[] { 65, true, 19, "Filipino", "Filipino" });

            migrationBuilder.CreateIndex(
                name: "IX_DonorBloodBagIssuance_DonorTransactionId",
                table: "DonorBloodBagIssuance",
                column: "DonorTransactionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DonorBloodBagIssuance");

            migrationBuilder.DeleteData(
                table: "LookupOption",
                keyColumn: "LookupOptionId",
                keyValue: 65);

            migrationBuilder.DeleteData(
                table: "Lookup",
                keyColumn: "LookupId",
                keyValue: 19);
        }
    }
}
