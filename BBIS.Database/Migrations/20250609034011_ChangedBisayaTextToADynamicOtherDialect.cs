using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BBIS.Database.Migrations
{
    public partial class ChangedBisayaTextToADynamicOtherDialect : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
        "ALTER TABLE `MedicalQuestionnaire` CHANGE `QuestionBisayaText` `QuestionOtherDialectText` VARCHAR(500);");

            migrationBuilder.UpdateData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 1,
                columns: new[] { "HeaderText", "QuestionEnglishText" },
                values: new object[] { "Pre-Donation preparations", "Do you feel well and healthy today?" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
        "ALTER TABLE `MedicalQuestionnaire` CHANGE `QuestionOtherDialectText` `QuestionBisayaText` VARCHAR(500);");

            migrationBuilder.UpdateData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 1,
                columns: new[] { "HeaderText", "QuestionEnglishText" },
                values: new object[] { null, null });
        }
    }
}
