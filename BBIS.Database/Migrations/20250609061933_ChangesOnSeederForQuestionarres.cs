using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BBIS.Database.Migrations
{
    public partial class ChangesOnSeederForQuestionarres : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 1,
                columns: new[] { "QuestionEnglishText", "QuestionOtherDialectText", "QuestionTagalogText" },
                values: new object[] { "Slept at least 6 hours?", "Nakatulog kaba'g 6 ka oras o sobra?", "" });

            migrationBuilder.UpdateData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 2,
                columns: new[] { "QuestionEnglishText", "QuestionOtherDialectText", "QuestionTagalogText" },
                values: new object[] { "Have you had any alcoholic drink in past 12 to 24 hours?", "Naka-inom ka ba'g makahubog nga ilimnon sa ning-aging 12 ngadto sa 24 ka oras?", "" });

            migrationBuilder.UpdateData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 3,
                columns: new[] { "QuestionEnglishText", "QuestionOtherDialectText", "QuestionTagalogText" },
                values: new object[] { "Have you smoked in the past 3-4 hours?", "Nakasigarilyo ka ba sa ning-aging 3 o 4 ka oras?", "" });

            migrationBuilder.UpdateData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 4,
                columns: new[] { "QuestionEnglishText", "QuestionOtherDialectText", "QuestionTagalogText" },
                values: new object[] { "Have you had a meal within the past 4 hours?", "Nakakaon ka ba sa ning-aging 4 ka oras?", "" });

            migrationBuilder.UpdateData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 5,
                columns: new[] { "HeaderText", "QuestionEnglishText", "QuestionOtherDialectText", "QuestionTagalogText" },
                values: new object[] { "Are you...", "Feeling healthy today?", "Maayo ra ang pamati sa panglawas karon?", "" });

            migrationBuilder.UpdateData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 6,
                columns: new[] { "HeaderText", "QuestionEnglishText", "QuestionOtherDialectText", "QuestionTagalogText" },
                values: new object[] { "Currently taking medication?", "Have you taken any medication from the deferral list?", "Aduna ka bay tambal nga na-inom nga nalista sa deferral list?", "" });

            migrationBuilder.UpdateData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 7,
                columns: new[] { "QuestionEnglishText", "QuestionOtherDialectText", "QuestionTagalogText" },
                values: new object[] { "Have you received any vaccination?", "Adunay nadawat nga bakuna?", "" });

            migrationBuilder.UpdateData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 8,
                columns: new[] { "HeaderText", "QuestionEnglishText", "QuestionOtherDialectText", "QuestionTagalogText" },
                values: new object[] { "In the past three days...", "Have you taken aspirin or anything that has aspirin in it?", "Nakatumar ka ba ug aspirin o uban pang tambal nga adunay sagol nga aspirin", "" });

            migrationBuilder.UpdateData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 9,
                columns: new[] { "GenderOption", "HeaderText", "QuestionEnglishText", "QuestionOtherDialectText", "QuestionTagalogText" },
                values: new object[] { "Female", "ARE FOR FEMALE DONORS", "Are you pregnant or have been pregnant before? Last Menstrual Period", "Mabdob kaba o niagi ka na ba ug pamabdos? Katapusang petsa sa regla", "" });

            migrationBuilder.UpdateData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 10,
                columns: new[] { "HeaderText", "QuestionEnglishText", "QuestionOtherDialectText", "QuestionTagalogText" },
                values: new object[] { "In the past 12 weeks have you", "Donated blood, platelet or plasma?", "Nakadonar ug dugo platelet o plasma?", "" });

            migrationBuilder.UpdateData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 11,
                columns: new[] { "HeaderText", "QuestionEnglishText", "QuestionOtherDialectText", "QuestionTagalogText" },
                values: new object[] { "In the past 12 months have you", "Had a blood transfusion?", "Na-abunchan ug dugo?", "" });

            migrationBuilder.UpdateData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 12,
                columns: new[] { "HeaderText", "QuestionEnglishText", "QuestionOtherDialectText", "QuestionTagalogText" },
                values: new object[] { null, "Had surgical operation, dental extraction?", "Na-operahan o na-ibtan ug ngipon?", "" });

            migrationBuilder.UpdateData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 13,
                columns: new[] { "QuestionEnglishText", "QuestionOtherDialectText", "QuestionTagalogText" },
                values: new object[] { "Had a tattoo, ear or body piercing, accidental contact with blood, needle-stick, and acupuncture?", "Nakapatatoo, patusok sa dunggan, o natuslokan ug dogum nga nahugawan ug dugo, nakapa- accupuncture?", "" });

            migrationBuilder.UpdateData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 14,
                columns: new[] { "QuestionEnglishText", "QuestionOtherDialectText", "QuestionTagalogText" },
                values: new object[] { "Had sexual contact with high risk individuals?", "Nakighilawas ug usa nga risgo nga adunay impeksyon?", "" });

            migrationBuilder.UpdateData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 15,
                columns: new[] { "QuestionEnglishText", "QuestionOtherDialectText", "QuestionTagalogText" },
                values: new object[] { "Had sexual contact with anyone in exchange for material or monetary gain?", "Nakighilawas aron mobayaran ka ug kwarta o laing hulip sa kwarta?", "" });

            migrationBuilder.UpdateData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 16,
                columns: new[] { "QuestionEnglishText", "QuestionOtherDialectText", "QuestionTagalogText" },
                values: new object[] { "Had sexual contact with a person who has worked abroad?", "Nakighilawas ug usa nga nakatrabaho sa gawas sa nasud?", "" });

            migrationBuilder.UpdateData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 17,
                columns: new[] { "QuestionEnglishText", "QuestionOtherDialectText", "QuestionTagalogText" },
                values: new object[] { "Engaged in casual sex?", "Nakighilawas bisan sa sinugdan wala tuyo-a o planoha?", "" });

            migrationBuilder.UpdateData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 18,
                columns: new[] { "QuestionEnglishText", "QuestionOtherDialectText", "QuestionTagalogText" },
                values: new object[] { "Lived with a person who has hepatitis?", "Naka-ipon ug puyo sa usa ka adunay sakit nga hepatitis?", "" });

            migrationBuilder.UpdateData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 19,
                columns: new[] { "QuestionEnglishText", "QuestionOtherDialectText", "QuestionTagalogText" },
                values: new object[] { "Have you been imprisoned??", "Nakasulay ug pagkapriso?", "" });

            migrationBuilder.UpdateData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 20,
                columns: new[] { "HeaderText", "QuestionEnglishText", "QuestionOtherDialectText", "QuestionTagalogText" },
                values: new object[] { null, "Have any of your relatives had Creutzfeldt-Jakob (Mad Cow) disease?", "Adunay paryente nga nasakit ug Creutzfeldt-Jakob (Mad Cow) disease?", "" });

            migrationBuilder.UpdateData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 21,
                columns: new[] { "HeaderText", "QuestionEnglishText", "QuestionOtherDialectText", "QuestionTagalogText" },
                values: new object[] { "Have you ever...", "lived outside your place of residence?", "Nakapuyo gawas sa imong na-andan nga puloy-anan?", "" });

            migrationBuilder.UpdateData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 22,
                columns: new[] { "QuestionEnglishText", "QuestionOtherDialectText", "QuestionTagalogText" },
                values: new object[] { "lived outside the Philippines?", "Nakapuyo gawas the Pilipinas?", "" });

            migrationBuilder.UpdateData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 23,
                columns: new[] { "QuestionEnglishText", "QuestionOtherDialectText", "QuestionTagalogText" },
                values: new object[] { "Used needles to take drugs, steroids, or anything not prescribed by your doctor?", "Nakagamit ug dagum para maka-indyeksyon ug druga, steroids, o laing matang sa tambal nga walay riseta sa doctor?", "" });

            migrationBuilder.UpdateData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 24,
                columns: new[] { "QuestionEnglishText", "QuestionOtherDialectText", "QuestionTagalogText" },
                values: new object[] { "Used clotting factor concentrates?", "Nahatagan ug mga clotting concentrates?", "" });

            migrationBuilder.UpdateData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 25,
                columns: new[] { "QuestionEnglishText", "QuestionOtherDialectText", "QuestionTagalogText" },
                values: new object[] { "Known that if you have the AIDS/Hepatitis virus, you can give it to someone else though you may feel well and have a negative HIV/Hepatitis test?", "Nahibalo nga kung aduna kay impeksyon nga AIDS/Hepatitis, mahimo nimo kining mapanakod bisan ug wala kay gipamati sa panglawas ug negatibo ang resulta sa imong testing sa dugo?", "" });

            migrationBuilder.UpdateData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 26,
                columns: new[] { "HeaderText", "QuestionEnglishText", "QuestionOtherDialectText", "QuestionTagalogText" },
                values: new object[] { null, "Had a positive test for the HIV/AIDS virus, Hepatitis virus, Syphilis or Malaria?", "Nakapatesting ka sa imong dugo para sa HIV/AIDS virus, Hepatitis virus, Syphilis or Malaria ug positibo ang resulto sa bisan ha-in niini?", "" });

            migrationBuilder.UpdateData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 27,
                columns: new[] { "QuestionEnglishText", "QuestionOtherDialectText", "QuestionTagalogText" },
                values: new object[] { "Had Hepatitis?", "Nagkasakit ug hepatitis?", "" });

            migrationBuilder.UpdateData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 28,
                columns: new[] { "QuestionEnglishText", "QuestionOtherDialectText", "QuestionTagalogText" },
                values: new object[] { "Had malaria?", "Nagkasakit ug malaria?", "" });

            migrationBuilder.UpdateData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 29,
                columns: new[] { "QuestionEnglishText", "QuestionOtherDialectText", "QuestionTagalogText" },
                values: new object[] { "Been told to have or treated for genital wart, syphilis, gonorrhea or other Sexually Transmissible Infections?", "Nogkasakit o nakapatambal para genital wart, syphilis, tulo (gonorrhea) o ubang laing mga sakit nga makuha sa pakighilawas?) 26. Had any type of cancer, for example leukemia? (nagkasakit ug kanser sama sa lukemya o uban pa?", "" });

            migrationBuilder.UpdateData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 30,
                columns: new[] { "QuestionEnglishText", "QuestionOtherDialectText", "QuestionTagalogText" },
                values: new object[] { "Had any problems with your heart and lungs?", "Adunay sakit o problema sa baga o kasing-kasing?", "" });

            migrationBuilder.UpdateData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 31,
                columns: new[] { "QuestionEnglishText", "QuestionOtherDialectText", "QuestionTagalogText" },
                values: new object[] { "Had a bleeding condition or a blood disease?", "Adunay problema sa pag-dugo or sakit sa dugo?", "" });

            migrationBuilder.UpdateData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 32,
                columns: new[] { "QuestionEnglishText", "QuestionOtherDialectText", "QuestionTagalogText" },
                values: new object[] { "Had a bleeding condition or a blood disease?", "Adunay problema sa pag-dugo or sakit sa dugo?", "" });

            migrationBuilder.UpdateData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 33,
                columns: new[] { "GenderOption", "HeaderText", "QuestionEnglishText", "QuestionOtherDialectText", "QuestionTagalogText" },
                values: new object[] { null, null, "Had a bleeding condition or a blood disease?", "Adunay problema sa pag-dugo or sakit sa dugo?", "" });

            migrationBuilder.UpdateData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 34,
                columns: new[] { "GenderOption", "QuestionEnglishText", "QuestionOtherDialectText", "QuestionTagalogText" },
                values: new object[] { null, "Are you giving blood because you wanted to be tested for HIV or Hepatitis virus?", "Mu-donar ug dugo tungod gusto ka magpa-testing sa HIV o hepatitis?", "" });

            migrationBuilder.UpdateData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 35,
                columns: new[] { "GenderOption", "QuestionEnglishText", "QuestionOtherDialectText", "QuestionTagalogText" },
                values: new object[] { null, "Would you be willing to be recalled to donate blood at VSMMC - Blood Services facility should the need arise?", "Kung magkinahanglan, mutugot ka ba nga tawagon aron pag-ari sa VSMMC - Blood Services aron sa pagdonar ug dugo?", "" });

            migrationBuilder.UpdateData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 36,
                columns: new[] { "GenderOption", "HeaderText", "QuestionEnglishText", "QuestionOtherDialectText", "QuestionTagalogText" },
                values: new object[] { null, "Additional Screening Tool for Blood Donors Based on NVBSP Guidelines (DOH-NVBSP Department Memorandum No. 2020-0124)", "Have you travelled outside the Philippines within the past 28 days? If YES, please indicate the specific country.", "Nakabiyahe ba ka sa gawas sa Pilipinas sa nilabay nga 28 ka adlaw? Kung oo, palihug isuwot kung asa nga lugar.", "" });

            migrationBuilder.UpdateData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 37,
                columns: new[] { "GenderOption", "QuestionEnglishText", "QuestionOtherDialectText", "QuestionTagalogText" },
                values: new object[] { null, "Have you experienced OR have been exposed to any of the following symptoms within the past 14 days up to present?", "Aduna ka ba'y bisan unsa aning mga mosunod o nakahimamat ug bisan kinsa nga adunay usa sa mga mosunod gikan sa niaging 14 ka adlaw hangtud karon?", "" });

            migrationBuilder.UpdateData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 38,
                columns: new[] { "HeaderText", "QuestionEnglishText", "QuestionOtherDialectText", "QuestionTagalogText" },
                values: new object[] { null, "Fever, Cough, Sore throat, Shortness of breath or difficulty of breathing, Diarrhea. If YES, indicate the date symptoms started", "Hilanat,Ubo,Pagsakit sa tutunlan,Kalisud sa pagginhawa,Kalibanga. Kung aduna, isuwat ang petsa kanus-a nagsugod", "" });

            migrationBuilder.UpdateData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 39,
                columns: new[] { "QuestionEnglishText", "QuestionOtherDialectText", "QuestionTagalogText" },
                values: new object[] { "Have you been identified as one of the following for COVID-19?", "Nailhan ka ba nga usa sa mga mosunod alang sa COVID-19?", "" });

            migrationBuilder.UpdateData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 40,
                columns: new[] { "QuestionEnglishText", "QuestionOtherDialectText", "QuestionTagalogText" },
                values: new object[] { "Have you been identified as one of the following for COVID-19?", "Nailhan ka ba nga usa sa mga mosunod alang sa COVID-19?", "" });

            migrationBuilder.UpdateData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 41,
                columns: new[] { "QuestionEnglishText", "QuestionOtherDialectText", "QuestionTagalogText" },
                values: new object[] { "COVID-19 suspect COVID-19 probable", "", "" });

            migrationBuilder.UpdateData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 42,
                columns: new[] { "QuestionEnglishText", "QuestionOtherDialectText", "QuestionTagalogText" },
                values: new object[] { "Confirmed (+) for COVID-19. If confirmed COVID-19(+), indicate the date of recovery", "Napamatud-an nga Positibo sa COVID-19. Kung nagpositibo sa COVID-19, isuwat ang petsa sa pagkaayo?", "" });

            migrationBuilder.UpdateData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 43,
                columns: new[] { "QuestionEnglishText", "QuestionOtherDialectText", "QuestionTagalogText" },
                values: new object[] { "Have you been exposed to somebody who is identified or has any of the following within the past 28 days?", "Aduna ka bay nahimamat nga mga tawo nga usa o adunay bisan unsa sa mga mosunod sa niaging 28 ka adlaw?", "" });

            migrationBuilder.UpdateData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 44,
                columns: new[] { "QuestionEnglishText", "QuestionOtherDialectText", "QuestionTagalogText" },
                values: new object[] { "COVID-19 suspect COVID-19 probable", "", "" });

            migrationBuilder.InsertData(
                table: "MedicalQuestionnaire",
                columns: new[] { "MedicalQuestionnaireId", "GenderOption", "HeaderText", "OrderNo", "QuestionEnglishText", "QuestionOtherDialectText", "QuestionTagalogText" },
                values: new object[] { 45, null, null, 45, "Confirmed (+) for COVID-19", "Napamatud-an nga Positibo sa COVID-19", "" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 45);

            migrationBuilder.UpdateData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 1,
                columns: new[] { "QuestionEnglishText", "QuestionOtherDialectText", "QuestionTagalogText" },
                values: new object[] { "Do you feel well and healthy today?", null, "Do you feel well and healthy today?" });

            migrationBuilder.UpdateData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 2,
                columns: new[] { "QuestionEnglishText", "QuestionOtherDialectText", "QuestionTagalogText" },
                values: new object[] { null, null, "Have you been refused as a blood donor or told not to donate blood?" });

            migrationBuilder.UpdateData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 3,
                columns: new[] { "QuestionEnglishText", "QuestionOtherDialectText", "QuestionTagalogText" },
                values: new object[] { null, null, "Will you voluntary allow your blood to be extracted for testing of HIV/AIDS, Hepatitis virus or other contagious diseases?" });

            migrationBuilder.UpdateData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 4,
                columns: new[] { "QuestionEnglishText", "QuestionOtherDialectText", "QuestionTagalogText" },
                values: new object[] { null, null, "Are you aware that a person with HIV/Hepatitis can still infect other people even if the HIV/AIDS/Hepatitis test result is negative?" });

            migrationBuilder.UpdateData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 5,
                columns: new[] { "HeaderText", "QuestionEnglishText", "QuestionOtherDialectText", "QuestionTagalogText" },
                values: new object[] { null, null, null, "Have you within the last 12 hours had alcohol?" });

            migrationBuilder.UpdateData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 6,
                columns: new[] { "HeaderText", "QuestionEnglishText", "QuestionOtherDialectText", "QuestionTagalogText" },
                values: new object[] { null, null, null, "Have you taken aspirin within the last 3 days?" });

            migrationBuilder.UpdateData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 7,
                columns: new[] { "QuestionEnglishText", "QuestionOtherDialectText", "QuestionTagalogText" },
                values: new object[] { null, null, "Have you taken any medication or have been vaccinated within the last 4 weeks?" });

            migrationBuilder.UpdateData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 8,
                columns: new[] { "HeaderText", "QuestionEnglishText", "QuestionOtherDialectText", "QuestionTagalogText" },
                values: new object[] { null, null, null, "Have you donated blood, platelets or plasma for the last 3 months?" });

            migrationBuilder.UpdateData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 9,
                columns: new[] { "GenderOption", "HeaderText", "QuestionEnglishText", "QuestionOtherDialectText", "QuestionTagalogText" },
                values: new object[] { null, "FOR THE PAST 6 MONTHS HAVE YOU:", null, null, "Gone to places in the Philippines or other countries known to have ZIKA virus?" });

            migrationBuilder.UpdateData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 10,
                columns: new[] { "HeaderText", "QuestionEnglishText", "QuestionOtherDialectText", "QuestionTagalogText" },
                values: new object[] { null, null, null, "Had intercourse with a person known and confirmed to be infected with ZIKA virus?" });

            migrationBuilder.UpdateData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 11,
                columns: new[] { "HeaderText", "QuestionEnglishText", "QuestionOtherDialectText", "QuestionTagalogText" },
                values: new object[] { null, null, null, "Had intercource with a person who had gone to places in the Philippines or other countries known to have ZIKA virus?" });

            migrationBuilder.UpdateData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 12,
                columns: new[] { "HeaderText", "QuestionEnglishText", "QuestionOtherDialectText", "QuestionTagalogText" },
                values: new object[] { "FOR THE PAST 12 MONTHS HAVE YOU:", null, null, "Been a recipient of donated blood for hemophilia or have been operated on or a recipient of organ transplant?" });

            migrationBuilder.UpdateData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 13,
                columns: new[] { "QuestionEnglishText", "QuestionOtherDialectText", "QuestionTagalogText" },
                values: new object[] { null, null, "Been medically operated on or had tooth extraction?" });

            migrationBuilder.UpdateData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 14,
                columns: new[] { "QuestionEnglishText", "QuestionOtherDialectText", "QuestionTagalogText" },
                values: new object[] { null, null, "Had tattoo, ear or body piercing, acupuncture, pricked by a needle or accidentally came in contact with blood?" });

            migrationBuilder.UpdateData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 15,
                columns: new[] { "QuestionEnglishText", "QuestionOtherDialectText", "QuestionTagalogText" },
                values: new object[] { null, null, "Had intercourse with a person with high possibility of not being safe or in exchange for monetary or material things?" });

            migrationBuilder.UpdateData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 16,
                columns: new[] { "QuestionEnglishText", "QuestionOtherDialectText", "QuestionTagalogText" },
                values: new object[] { null, null, "Had intercourse with a person without protected or deemed not safe?" });

            migrationBuilder.UpdateData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 17,
                columns: new[] { "QuestionEnglishText", "QuestionOtherDialectText", "QuestionTagalogText" },
                values: new object[] { null, null, "Had experience having jaundice, liver disease or mingled with those with liver diseases?" });

            migrationBuilder.UpdateData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 18,
                columns: new[] { "QuestionEnglishText", "QuestionOtherDialectText", "QuestionTagalogText" },
                values: new object[] { null, null, "Been locked up or imprisoned?" });

            migrationBuilder.UpdateData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 19,
                columns: new[] { "QuestionEnglishText", "QuestionOtherDialectText", "QuestionTagalogText" },
                values: new object[] { null, null, "Lived or had relatives in the United Kingdom or Europe?" });

            migrationBuilder.UpdateData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 20,
                columns: new[] { "HeaderText", "QuestionEnglishText", "QuestionOtherDialectText", "QuestionTagalogText" },
                values: new object[] { "HAVE YOU:", null, null, "Travelled or lived apart from your current residence or outside the Philippines?" });

            migrationBuilder.UpdateData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 21,
                columns: new[] { "HeaderText", "QuestionEnglishText", "QuestionOtherDialectText", "QuestionTagalogText" },
                values: new object[] { null, null, null, "Used prohibited drugs? (inducing, sniffing or injecting)" });

            migrationBuilder.UpdateData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 22,
                columns: new[] { "QuestionEnglishText", "QuestionOtherDialectText", "QuestionTagalogText" },
                values: new object[] { null, null, "Taken medications for stopping and treating abnormal bleeding?" });

            migrationBuilder.UpdateData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 23,
                columns: new[] { "QuestionEnglishText", "QuestionOtherDialectText", "QuestionTagalogText" },
                values: new object[] { null, null, "Tested positive for HIV, Hepatitis, Syphilis or Malaria?" });

            migrationBuilder.UpdateData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 24,
                columns: new[] { "QuestionEnglishText", "QuestionOtherDialectText", "QuestionTagalogText" },
                values: new object[] { null, null, "Had malaria or liver disease?" });

            migrationBuilder.UpdateData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 25,
                columns: new[] { "QuestionEnglishText", "QuestionOtherDialectText", "QuestionTagalogText" },
                values: new object[] { null, null, "Had or treated warts in the sensitive parts of the body, syphilis, gonorrhea or any other sexually transmitted diseases?" });

            migrationBuilder.UpdateData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 26,
                columns: new[] { "HeaderText", "QuestionEnglishText", "QuestionOtherDialectText", "QuestionTagalogText" },
                values: new object[] { "HAVE YOU HAD THE FOLLOWING:", null, null, "Cancer, blood disease or hemophilia?" });

            migrationBuilder.UpdateData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 27,
                columns: new[] { "QuestionEnglishText", "QuestionOtherDialectText", "QuestionTagalogText" },
                values: new object[] { null, null, "Heart disease or chest pain?" });

            migrationBuilder.UpdateData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 28,
                columns: new[] { "QuestionEnglishText", "QuestionOtherDialectText", "QuestionTagalogText" },
                values: new object[] { null, null, "Lung disease, tuberculosis or asthma?" });

            migrationBuilder.UpdateData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 29,
                columns: new[] { "QuestionEnglishText", "QuestionOtherDialectText", "QuestionTagalogText" },
                values: new object[] { null, null, "Kidney dusease, diabetes or epilepsy?" });

            migrationBuilder.UpdateData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 30,
                columns: new[] { "QuestionEnglishText", "QuestionOtherDialectText", "QuestionTagalogText" },
                values: new object[] { null, null, "Small pox, canker sore or ulcers?" });

            migrationBuilder.UpdateData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 31,
                columns: new[] { "QuestionEnglishText", "QuestionOtherDialectText", "QuestionTagalogText" },
                values: new object[] { null, null, "Other diseases not mentioned or had an operation?" });

            migrationBuilder.UpdateData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 32,
                columns: new[] { "QuestionEnglishText", "QuestionOtherDialectText", "QuestionTagalogText" },
                values: new object[] { null, null, "Rashes or fever? Is it simultaneous with body aches, rheumatism or eye redness?" });

            migrationBuilder.UpdateData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 33,
                columns: new[] { "GenderOption", "HeaderText", "QuestionEnglishText", "QuestionOtherDialectText", "QuestionTagalogText" },
                values: new object[] { "Female", "FOR FEMALE ONLY:", null, null, "Are you currently pregnant? Have you been pregnant in the past?" });

            migrationBuilder.UpdateData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 34,
                columns: new[] { "GenderOption", "QuestionEnglishText", "QuestionOtherDialectText", "QuestionTagalogText" },
                values: new object[] { "Female", null, null, "When was the last time you have given birth?" });

            migrationBuilder.UpdateData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 35,
                columns: new[] { "GenderOption", "QuestionEnglishText", "QuestionOtherDialectText", "QuestionTagalogText" },
                values: new object[] { "Female", null, null, "For the past year, have you had miscarriage or abortion?" });

            migrationBuilder.UpdateData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 36,
                columns: new[] { "GenderOption", "HeaderText", "QuestionEnglishText", "QuestionOtherDialectText", "QuestionTagalogText" },
                values: new object[] { "Female", null, null, null, "Are you currently breast feeding?" });

            migrationBuilder.UpdateData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 37,
                columns: new[] { "GenderOption", "QuestionEnglishText", "QuestionOtherDialectText", "QuestionTagalogText" },
                values: new object[] { "Female", null, null, "When was the last time you had your menstruation?" });

            migrationBuilder.UpdateData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 38,
                columns: new[] { "HeaderText", "QuestionEnglishText", "QuestionOtherDialectText", "QuestionTagalogText" },
                values: new object[] { "ACCEPTANCE CRITERIA IN RELATION TO COVID-19:", null, null, "Do you have colds, coughs and/or fever today?" });

            migrationBuilder.UpdateData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 39,
                columns: new[] { "QuestionEnglishText", "QuestionOtherDialectText", "QuestionTagalogText" },
                values: new object[] { null, null, "Were you tested for COVID 19 infection? Result and date of last test?" });

            migrationBuilder.UpdateData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 40,
                columns: new[] { "QuestionEnglishText", "QuestionOtherDialectText", "QuestionTagalogText" },
                values: new object[] { null, null, "Were you diagnosed with COVID 19 infection? Date of last test and treatment?" });

            migrationBuilder.UpdateData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 41,
                columns: new[] { "QuestionEnglishText", "QuestionOtherDialectText", "QuestionTagalogText" },
                values: new object[] { null, null, "In the last 14 days, have you had close contact with a confirmed case of COVID-19 or cared for an infected patient?" });

            migrationBuilder.UpdateData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 42,
                columns: new[] { "QuestionEnglishText", "QuestionOtherDialectText", "QuestionTagalogText" },
                values: new object[] { null, null, "In the last 14 days, have you travelled from areas with known community transmissions or any place outside the Philippines?" });

            migrationBuilder.UpdateData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 43,
                columns: new[] { "QuestionEnglishText", "QuestionOtherDialectText", "QuestionTagalogText" },
                values: new object[] { null, null, "In the last 4 weeks have you come in close contact with wild or exotic animals including its products?" });

            migrationBuilder.UpdateData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 44,
                columns: new[] { "QuestionEnglishText", "QuestionOtherDialectText", "QuestionTagalogText" },
                values: new object[] { null, null, "In the last 4 weeks, have you eaten raw or uncooked meat and drank unprocessed milk?" });
        }
    }
}
