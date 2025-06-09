using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BBIS.Database.Migrations
{
    public partial class FixDoubleDataOnQuestionnares : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 45);

            migrationBuilder.UpdateData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 32,
                columns: new[] { "QuestionEnglishText", "QuestionOtherDialectText" },
                values: new object[] { "Are you giving blood because you wanted to be tested for HIV or Hepatitis virus?", "Mu-donar ug dugo tungod gusto ka magpa-testing sa HIV o hepatitis?" });

            migrationBuilder.UpdateData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 33,
                columns: new[] { "QuestionEnglishText", "QuestionOtherDialectText" },
                values: new object[] { "Would you be willing to be recalled to donate blood at VSMMC - Blood Services facility should the need arise?", "Kung magkinahanglan, mutugot ka ba nga tawagon aron pag-ari sa VSMMC - Blood Services aron sa pagdonar ug dugo?" });

            migrationBuilder.UpdateData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 34,
                columns: new[] { "HeaderText", "QuestionEnglishText", "QuestionOtherDialectText" },
                values: new object[] { "Additional Screening Tool for Blood Donors Based on NVBSP Guidelines (DOH-NVBSP Department Memorandum No. 2020-0124)", "Have you travelled outside the Philippines within the past 28 days? If YES, please indicate the specific country.", "Nakabiyahe ba ka sa gawas sa Pilipinas sa nilabay nga 28 ka adlaw? Kung oo, palihug isuwot kung asa nga lugar." });

            migrationBuilder.UpdateData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 35,
                columns: new[] { "QuestionEnglishText", "QuestionOtherDialectText" },
                values: new object[] { "Have you experienced OR have been exposed to any of the following symptoms within the past 14 days up to present?", "Aduna ka ba'y bisan unsa aning mga mosunod o nakahimamat ug bisan kinsa nga adunay usa sa mga mosunod gikan sa niaging 14 ka adlaw hangtud karon?" });

            migrationBuilder.UpdateData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 36,
                columns: new[] { "HeaderText", "QuestionEnglishText", "QuestionOtherDialectText" },
                values: new object[] { null, "Fever, Cough, Sore throat, Shortness of breath or difficulty of breathing, Diarrhea. If YES, indicate the date symptoms started", "Hilanat,Ubo,Pagsakit sa tutunlan,Kalisud sa pagginhawa,Kalibanga. Kung aduna, isuwat ang petsa kanus-a nagsugod" });

            migrationBuilder.UpdateData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 37,
                columns: new[] { "QuestionEnglishText", "QuestionOtherDialectText" },
                values: new object[] { "Have you been identified as one of the following for COVID-19?", "Nailhan ka ba nga usa sa mga mosunod alang sa COVID-19?" });

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

            migrationBuilder.UpdateData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 43,
                columns: new[] { "QuestionEnglishText", "QuestionOtherDialectText" },
                values: new object[] { "Confirmed (+) for COVID-19", "Napamatud-an nga Positibo sa COVID-19" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 32,
                columns: new[] { "QuestionEnglishText", "QuestionOtherDialectText" },
                values: new object[] { "Had a bleeding condition or a blood disease?", "Adunay problema sa pag-dugo or sakit sa dugo?" });

            migrationBuilder.UpdateData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 33,
                columns: new[] { "QuestionEnglishText", "QuestionOtherDialectText" },
                values: new object[] { "Had a bleeding condition or a blood disease?", "Adunay problema sa pag-dugo or sakit sa dugo?" });

            migrationBuilder.UpdateData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 34,
                columns: new[] { "HeaderText", "QuestionEnglishText", "QuestionOtherDialectText" },
                values: new object[] { null, "Are you giving blood because you wanted to be tested for HIV or Hepatitis virus?", "Mu-donar ug dugo tungod gusto ka magpa-testing sa HIV o hepatitis?" });

            migrationBuilder.UpdateData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 35,
                columns: new[] { "QuestionEnglishText", "QuestionOtherDialectText" },
                values: new object[] { "Would you be willing to be recalled to donate blood at VSMMC - Blood Services facility should the need arise?", "Kung magkinahanglan, mutugot ka ba nga tawagon aron pag-ari sa VSMMC - Blood Services aron sa pagdonar ug dugo?" });

            migrationBuilder.UpdateData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 36,
                columns: new[] { "HeaderText", "QuestionEnglishText", "QuestionOtherDialectText" },
                values: new object[] { "Additional Screening Tool for Blood Donors Based on NVBSP Guidelines (DOH-NVBSP Department Memorandum No. 2020-0124)", "Have you travelled outside the Philippines within the past 28 days? If YES, please indicate the specific country.", "Nakabiyahe ba ka sa gawas sa Pilipinas sa nilabay nga 28 ka adlaw? Kung oo, palihug isuwot kung asa nga lugar." });

            migrationBuilder.UpdateData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 37,
                columns: new[] { "QuestionEnglishText", "QuestionOtherDialectText" },
                values: new object[] { "Have you experienced OR have been exposed to any of the following symptoms within the past 14 days up to present?", "Aduna ka ba'y bisan unsa aning mga mosunod o nakahimamat ug bisan kinsa nga adunay usa sa mga mosunod gikan sa niaging 14 ka adlaw hangtud karon?" });

            migrationBuilder.UpdateData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 38,
                columns: new[] { "QuestionEnglishText", "QuestionOtherDialectText" },
                values: new object[] { "Fever, Cough, Sore throat, Shortness of breath or difficulty of breathing, Diarrhea. If YES, indicate the date symptoms started", "Hilanat,Ubo,Pagsakit sa tutunlan,Kalisud sa pagginhawa,Kalibanga. Kung aduna, isuwat ang petsa kanus-a nagsugod" });

            migrationBuilder.UpdateData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 39,
                columns: new[] { "QuestionEnglishText", "QuestionOtherDialectText" },
                values: new object[] { "Have you been identified as one of the following for COVID-19?", "Nailhan ka ba nga usa sa mga mosunod alang sa COVID-19?" });

            migrationBuilder.UpdateData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 40,
                columns: new[] { "QuestionEnglishText", "QuestionOtherDialectText" },
                values: new object[] { "Have you been identified as one of the following for COVID-19?", "Nailhan ka ba nga usa sa mga mosunod alang sa COVID-19?" });

            migrationBuilder.UpdateData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 41,
                columns: new[] { "QuestionEnglishText", "QuestionOtherDialectText" },
                values: new object[] { "COVID-19 suspect COVID-19 probable", "" });

            migrationBuilder.UpdateData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 42,
                columns: new[] { "QuestionEnglishText", "QuestionOtherDialectText" },
                values: new object[] { "Confirmed (+) for COVID-19. If confirmed COVID-19(+), indicate the date of recovery", "Napamatud-an nga Positibo sa COVID-19. Kung nagpositibo sa COVID-19, isuwat ang petsa sa pagkaayo?" });

            migrationBuilder.UpdateData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 43,
                columns: new[] { "QuestionEnglishText", "QuestionOtherDialectText" },
                values: new object[] { "Have you been exposed to somebody who is identified or has any of the following within the past 28 days?", "Aduna ka bay nahimamat nga mga tawo nga usa o adunay bisan unsa sa mga mosunod sa niaging 28 ka adlaw?" });

            migrationBuilder.InsertData(
                table: "MedicalQuestionnaire",
                columns: new[] { "MedicalQuestionnaireId", "GenderOption", "HeaderText", "OrderNo", "QuestionEnglishText", "QuestionOtherDialectText", "QuestionTagalogText" },
                values: new object[,]
                {
                    { 44, null, null, 44, "COVID-19 suspect COVID-19 probable", "", "" },
                    { 45, null, null, 45, "Confirmed (+) for COVID-19", "Napamatud-an nga Positibo sa COVID-19", "" }
                });
        }
    }
}
