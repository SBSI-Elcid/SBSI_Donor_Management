using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BBIS.Database.Migrations
{
    public partial class AddedScheduleIdForRegistration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 43);

            migrationBuilder.AddColumn<Guid>(
                name: "ScheduleId",
                table: "DonorTransaction",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "ScheduleId",
                table: "DonorRegistration",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.UpdateData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 38,
                columns: new[] { "QuestionEnglishText", "QuestionOtherDialectText" },
                values: new object[] { "COVID-19 suspect COVID-19 probable", "COVID-19 suspect COVID-19 probable" });

            migrationBuilder.UpdateData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 39,
                columns: new[] { "QuestionEnglishText", "QuestionOtherDialectText" },
                values: new object[] { "Confirmed (+) for COVID-19. If confirmed COVID-19(+), indicate the date of recovery", "Napamatud-an nga Positibo sa COVID-19. Kung nagpositibo sa COVID-19, isuwat ang petsa sa pagkaayo?" });

            migrationBuilder.UpdateData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 40,
                columns: new[] { "QuestionEnglishText", "QuestionOtherDialectText" },
                values: new object[] { "Have you been exposed to somebody who is identified or has any of the following within the past 28 days?", "Aduna ka bay nahimamat nga mga tawo nga usa o adunay bisan unsa sa mga mosunod sa niaging 28 ka adlaw?" });

            migrationBuilder.UpdateData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 41,
                columns: new[] { "QuestionEnglishText", "QuestionOtherDialectText" },
                values: new object[] { "COVID-19 suspect COVID-19 probable", "COVID-19 suspect COVID-19 probable" });

            migrationBuilder.UpdateData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 42,
                columns: new[] { "QuestionEnglishText", "QuestionOtherDialectText" },
                values: new object[] { "Confirmed (+) for COVID-19", "Napamatud-an nga Positibo sa COVID-19" });

            migrationBuilder.CreateIndex(
                name: "IX_DonorTransaction_ScheduleId",
                table: "DonorTransaction",
                column: "ScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_DonorRegistration_ScheduleId",
                table: "DonorRegistration",
                column: "ScheduleId");

            migrationBuilder.AddForeignKey(
                name: "FK_DonorRegistration_Schedule_ScheduleId",
                table: "DonorRegistration",
                column: "ScheduleId",
                principalTable: "Schedule",
                principalColumn: "ScheduleId");

            migrationBuilder.AddForeignKey(
                name: "FK_DonorTransaction_Schedule_ScheduleId",
                table: "DonorTransaction",
                column: "ScheduleId",
                principalTable: "Schedule",
                principalColumn: "ScheduleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DonorRegistration_Schedule_ScheduleId",
                table: "DonorRegistration");

            migrationBuilder.DropForeignKey(
                name: "FK_DonorTransaction_Schedule_ScheduleId",
                table: "DonorTransaction");

            migrationBuilder.DropIndex(
                name: "IX_DonorTransaction_ScheduleId",
                table: "DonorTransaction");

            migrationBuilder.DropIndex(
                name: "IX_DonorRegistration_ScheduleId",
                table: "DonorRegistration");

            migrationBuilder.DropColumn(
                name: "ScheduleId",
                table: "DonorTransaction");

            migrationBuilder.DropColumn(
                name: "ScheduleId",
                table: "DonorRegistration");

            migrationBuilder.UpdateData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 38,
                columns: new[] { "QuestionEnglishText", "QuestionOtherDialectText" },
                values: new object[] { "Have you been identified as one of the following for COVID-19?", "Nailhan ka ba nga usa sa mga mosunod alang sa COVID-19?" });

            migrationBuilder.UpdateData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 39,
                columns: new[] { "QuestionEnglishText", "QuestionOtherDialectText" },
                values: new object[] { "COVID-19 suspect COVID-19 probable", "" });

            migrationBuilder.UpdateData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 40,
                columns: new[] { "QuestionEnglishText", "QuestionOtherDialectText" },
                values: new object[] { "Confirmed (+) for COVID-19. If confirmed COVID-19(+), indicate the date of recovery", "Napamatud-an nga Positibo sa COVID-19. Kung nagpositibo sa COVID-19, isuwat ang petsa sa pagkaayo?" });

            migrationBuilder.UpdateData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 41,
                columns: new[] { "QuestionEnglishText", "QuestionOtherDialectText" },
                values: new object[] { "Have you been exposed to somebody who is identified or has any of the following within the past 28 days?", "Aduna ka bay nahimamat nga mga tawo nga usa o adunay bisan unsa sa mga mosunod sa niaging 28 ka adlaw?" });

            migrationBuilder.UpdateData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 42,
                columns: new[] { "QuestionEnglishText", "QuestionOtherDialectText" },
                values: new object[] { "COVID-19 suspect COVID-19 probable", "" });

            migrationBuilder.InsertData(
                table: "MedicalQuestionnaire",
                columns: new[] { "MedicalQuestionnaireId", "GenderOption", "HeaderText", "OrderNo", "QuestionEnglishText", "QuestionOtherDialectText", "QuestionTagalogText" },
                values: new object[] { 43, null, null, 43, "Confirmed (+) for COVID-19", "Napamatud-an nga Positibo sa COVID-19", "" });
        }
    }
}
