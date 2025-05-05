using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BBIS.Database.Migrations
{
    public partial class Seed_Data : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ApplicationSetting",
                columns: new[] { "ApplicationSettingId", "IsActive", "SettingKey", "SettingValue" },
                values: new object[,]
                {
                    { new Guid("1891de09-2589-46da-ac21-f7053e2027aa"), true, "BloodCollectionUnitOfMeasure", "mL" },
                    { new Guid("41f1542b-2a84-40bf-b39e-774c75265853"), true, "InstitutionName", "SBSI" },
                    { new Guid("f89d1eaf-94f0-4993-93e0-b9175dbe8f04"), true, "BloodComponentsUnitOfMeasure", "mL" }
                });

            migrationBuilder.InsertData(
                table: "BloodComponent",
                columns: new[] { "BloodComponentId", "ComponentName", "ExpiryInDays", "IsActive", "NotifyOnDaysBefore" },
                values: new object[,]
                {
                    { new Guid("2247d0fd-d546-40e7-b2f3-cfc64d6e48a0"), "Fresh Frozen Plasma", 365, true, 0 },
                    { new Guid("601f8624-1984-4fb3-bd93-30dfb5ca2835"), "Leukoreduced Red Blood Cells", 5, true, 0 },
                    { new Guid("b03ac043-8569-4f7d-97d1-e8bbdb1a360a"), "Platelet Concentrate", 5, true, 0 },
                    { new Guid("d7431819-53dd-4bfc-a4e9-fed3139a4998"), "Packed Red Blood Cells", 35, true, 0 }
                });

            migrationBuilder.InsertData(
                table: "BloodTestType",
                columns: new[] { "BloodTestTypeId", "IsActive", "TypeName" },
                values: new object[,]
                {
                    { new Guid("027d2030-29e8-4dd8-a227-92312815bdf8"), true, "HCV" },
                    { new Guid("29d3082d-e681-466a-aeb1-2ed9440fba11"), true, "HBsAg" },
                    { new Guid("94f2e936-5ba0-4bdb-8d31-0e2260f09075"), true, "Malaria" },
                    { new Guid("a9e9fafe-9a2a-4d4f-b1da-ecd7f64af5ce"), true, "Syphilis" },
                    { new Guid("ba180ddd-c55c-419e-870a-ca45ec8f91d9"), true, "HIV" }
                });

            migrationBuilder.InsertData(
                table: "Lookup",
                columns: new[] { "LookupId", "Description", "IsActive", "LookupKey" },
                values: new object[,]
                {
                    { 1, "Blood type options", true, "lookup.BloodTypes" },
                    { 2, "Blood Donation Type options", true, "lookup.DonationTypes" },
                    { 3, "In house type options", true, "lookup.InHouseTypes" },
                    { 4, "Blood bag type options", true, "lookup.BloodBagTypes" },
                    { 5, "Agency type options", true, "lookup.AgencyTypes" },
                    { 6, "Donor Status options", true, "lookup.DonorStatus" },
                    { 7, "Blood Bag Size options", true, "lookup.BloodBagSizeTypes" },
                    { 8, "Special Bag type options", true, "lookup.SpecialBagTypes" },
                    { 9, "Apheresis Bag type options", true, "lookup.ApheresisBagTypes" },
                    { 10, "Blood Bag Collection Type options", true, "lookup.BloodBagCollectionTypes" },
                    { 11, "Civil Status options", true, "lookup.CivilStatus" },
                    { 12, "Deferral Status options", true, "lookup.DeferralStatus" },
                    { 13, "Physical Exam Result options", true, "lookup.PhysicalExamResult" },
                    { 14, "Priority Level options", true, "lookup.PriorityLevel" },
                    { 15, "Cross Matching options", true, "lookup.CrossMatchingOptions" },
                    { 16, "Patient Type options", true, "lookup.PatientType" },
                    { 17, "Priority Level options", true, "lookup.Membership" },
                    { 18, "Cross Match Types", true, "lookup.CrossMatchTypes" }
                });

            migrationBuilder.InsertData(
                table: "MedicalQuestionnaire",
                columns: new[] { "MedicalQuestionnaireId", "GenderOption", "HeaderText", "OrderNo", "QuestionEnglishText", "QuestionTagalogText" },
                values: new object[,]
                {
                    { 1, null, null, 1, null, "Do you feel well and healthy today?" },
                    { 2, null, null, 2, null, "Have you been refused as a blood donor or told not to donate blood?" },
                    { 3, null, null, 3, null, "Will you voluntary allow your blood to be extracted for testing of HIV/AIDS, Hepatitis virus or other contagious diseases?" },
                    { 4, null, null, 4, null, "Are you aware that a person with HIV/Hepatitis can still infect other people even if the HIV/AIDS/Hepatitis test result is negative?" },
                    { 5, null, null, 5, null, "Have you within the last 12 hours had alcohol?" },
                    { 6, null, null, 6, null, "Have you taken aspirin within the last 3 days?" },
                    { 7, null, null, 7, null, "Have you taken any medication or have been vaccinated within the last 4 weeks?" },
                    { 8, null, null, 8, null, "Have you donated blood, platelets or plasma for the last 3 months?" },
                    { 9, null, "FOR THE PAST 6 MONTHS HAVE YOU:", 9, null, "Gone to places in the Philippines or other countries known to have ZIKA virus?" },
                    { 10, null, null, 10, null, "Had intercourse with a person known and confirmed to be infected with ZIKA virus?" },
                    { 11, null, null, 11, null, "Had intercource with a person who had gone to places in the Philippines or other countries known to have ZIKA virus?" },
                    { 12, null, "FOR THE PAST 12 MONTHS HAVE YOU:", 12, null, "Been a recipient of donated blood for hemophilia or have been operated on or a recipient of organ transplant?" },
                    { 13, null, null, 13, null, "Been medically operated on or had tooth extraction?" },
                    { 14, null, null, 14, null, "Had tattoo, ear or body piercing, acupuncture, pricked by a needle or accidentally came in contact with blood?" },
                    { 15, null, null, 15, null, "Had intercourse with a person with high possibility of not being safe or in exchange for monetary or material things?" },
                    { 16, null, null, 16, null, "Had intercourse with a person without protected or deemed not safe?" },
                    { 17, null, null, 17, null, "Had experience having jaundice, liver disease or mingled with those with liver diseases?" },
                    { 18, null, null, 18, null, "Been locked up or imprisoned?" },
                    { 19, null, null, 19, null, "Lived or had relatives in the United Kingdom or Europe?" },
                    { 20, null, "HAVE YOU:", 20, null, "Travelled or lived apart from your current residence or outside the Philippines?" },
                    { 21, null, null, 21, null, "Used prohibited drugs? (inducing, sniffing or injecting)" },
                    { 22, null, null, 22, null, "Taken medications for stopping and treating abnormal bleeding?" },
                    { 23, null, null, 23, null, "Tested positive for HIV, Hepatitis, Syphilis or Malaria?" },
                    { 24, null, null, 24, null, "Had malaria or liver disease?" },
                    { 25, null, null, 25, null, "Had or treated warts in the sensitive parts of the body, syphilis, gonorrhea or any other sexually transmitted diseases?" },
                    { 26, null, "HAVE YOU HAD THE FOLLOWING:", 26, null, "Cancer, blood disease or hemophilia?" },
                    { 27, null, null, 27, null, "Heart disease or chest pain?" },
                    { 28, null, null, 28, null, "Lung disease, tuberculosis or asthma?" },
                    { 29, null, null, 29, null, "Kidney dusease, diabetes or epilepsy?" },
                    { 30, null, null, 30, null, "Small pox, canker sore or ulcers?" },
                    { 31, null, null, 31, null, "Other diseases not mentioned or had an operation?" },
                    { 32, null, null, 32, null, "Rashes or fever? Is it simultaneous with body aches, rheumatism or eye redness?" },
                    { 33, "Female", "FOR FEMALE ONLY:", 33, null, "Are you currently pregnant? Have you been pregnant in the past?" },
                    { 34, "Female", null, 34, null, "When was the last time you have given birth?" },
                    { 35, "Female", null, 35, null, "For the past year, have you had miscarriage or abortion?" },
                    { 36, "Female", null, 36, null, "Are you currently breast feeding?" },
                    { 37, "Female", null, 37, null, "When was the last time you had your menstruation?" },
                    { 38, null, "ACCEPTANCE CRITERIA IN RELATION TO COVID-19:", 38, null, "Do you have colds, coughs and/or fever today?" },
                    { 39, null, null, 39, null, "Were you tested for COVID 19 infection? Result and date of last test?" },
                    { 40, null, null, 40, null, "Were you diagnosed with COVID 19 infection? Date of last test and treatment?" },
                    { 41, null, null, 41, null, "In the last 14 days, have you had close contact with a confirmed case of COVID-19 or cared for an infected patient?" },
                    { 42, null, null, 42, null, "In the last 14 days, have you travelled from areas with known community transmissions or any place outside the Philippines?" },
                    { 43, null, null, 43, null, "In the last 4 weeks have you come in close contact with wild or exotic animals including its products?" },
                    { 44, null, null, 44, null, "In the last 4 weeks, have you eaten raw or uncooked meat and drank unprocessed milk?" }
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "RoleId", "RoleName" },
                values: new object[,]
                {
                    { new Guid("22ee22cd-9580-4d5c-8e90-2d615283df1e"), "PhysicalExamScreener" },
                    { new Guid("489abc53-af8a-4914-bd21-7d99c26e2e33"), "Admin" },
                    { new Guid("69e19e84-eeae-4361-bf8d-53456def3994"), "BloodCollector" },
                    { new Guid("84148d25-7e25-4bdc-bfb5-d0f3cd729ab7"), "RequisitionUser" },
                    { new Guid("9922d75f-0f7a-4fb2-ab1b-5f06b118136c"), "InitialScreener" },
                    { new Guid("b42f4287-089a-41f9-89ef-a5a3f474f754"), "InventoryUser" },
                    { new Guid("c2f7694a-1d5e-4754-8575-2c62cbfdf627"), "DonorAdmin" },
                    { new Guid("c3f8818a-7a67-4d54-ba8e-3a5bd247e730"), "DonorEvent" }
                });

            migrationBuilder.InsertData(
                table: "TestOrderType",
                columns: new[] { "TestOrderTypeId", "Code", "Description", "IsActive", "Name" },
                values: new object[,]
                {
                    { new Guid("3b382719-3167-4ccd-941e-1cd6e1952372"), "BScreen", "Blood Screening", true, "Blood Screening" },
                    { new Guid("5092b84e-ed91-4370-9984-42392ffa97f4"), "Coombs", "Coombs Test", true, "Coombs Test" },
                    { new Guid("722bcb71-9a94-4734-857e-68ddb2711259"), "BTyping", "Blood Typing", true, "Blood Typing" }
                });

            migrationBuilder.InsertData(
                table: "UserAccount",
                columns: new[] { "UserAccountId", "ChangePasswordOnLogin", "EmailAddress", "Firstname", "IsActive", "IsDeleted", "Lastname", "Password", "PasswordSalt", "UpdatedAt", "UpdatedBy", "Username" },
                values: new object[] { new Guid("057f1f23-3015-4e71-81fc-0bae7f1db34b"), false, "serallain@gmail.com", "Admin", true, false, "Default", "52cMcbtmJdCm0zhkH0ZQJK6xThufij3Hk7SGN1Z0Kf8=", "ay94yItGEYzB02M7QkJ2CaZMs6CvuZI5EVMyE/5+", new DateTime(2022, 6, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("057f1f23-3015-4e71-81fc-0bae7f1db34b"), "admin" });

            migrationBuilder.InsertData(
                table: "LookupOption",
                columns: new[] { "LookupOptionId", "IsActive", "LookupId", "Name", "Value" },
                values: new object[,]
                {
                    { 1, true, 1, "O", "O" },
                    { 2, true, 1, "A", "A" },
                    { 3, true, 1, "B", "B" },
                    { 4, true, 1, "AB", "AB" },
                    { 5, true, 2, "In-House", "InHouse" },
                    { 6, true, 2, "Mobile Blood Donation", "Mobile" },
                    { 7, true, 3, "WALK-IN/VOLUNTARY", "WalkInOrVolountary" },
                    { 8, true, 3, "REPLACEMENT", "Replacement" },
                    { 9, true, 3, "PATIENT-DIRECTED", "PatientDirected" },
                    { 10, true, 4, "Single", "Single" },
                    { 11, true, 4, "Multiple", "Multiple" },
                    { 12, true, 4, "Top & Bottom", "TopAndBottom" },
                    { 13, true, 4, "Apheresis", "Apheresis" },
                    { 14, true, 5, "Red Cross", "RedCross" },
                    { 15, true, 5, "Hospital", "Hospital" },
                    { 16, true, 6, "For Initial Screening", "ForInitialScreening" },
                    { 17, true, 6, "For Physical Examination", "ForPhysicalExamination" },
                    { 18, true, 6, "For Blood Collection", "ForBloodCollection" },
                    { 19, true, 6, "For Laboratory Test", "ForLaboratoryTest" },
                    { 20, true, 6, "Success", "Success" },
                    { 21, true, 6, "Deferred", "Deferred" },
                    { 22, true, 7, "Single", "Single" },
                    { 23, true, 7, "Double", "Double" },
                    { 24, true, 7, "Tripple", "Tripple" },
                    { 25, true, 7, "Quadruple", "Quadruple" },
                    { 26, true, 8, "FK T&B", "FK T&B" },
                    { 27, true, 8, "TRM T&B", "TRM T&B" },
                    { 28, true, 9, "FRES", "FRES" },
                    { 29, true, 9, "AMI", "AMI" },
                    { 30, true, 9, "HAE", "HAE" },
                    { 31, true, 9, "TRI", "TRI" },
                    { 32, true, 10, "KARMI", "KARMI" },
                    { 33, true, 10, "TERUMO", "TERUMO" },
                    { 34, true, 10, "SPECIAL BAG", "SpecialBag" },
                    { 35, true, 10, "APHERESIS", "Apheresis" },
                    { 36, true, 11, "Single", "Single" },
                    { 37, true, 11, "Married", "Married" },
                    { 38, true, 11, "Separated", "Separated" },
                    { 39, true, 11, "Widow", "Widow" },
                    { 40, true, 12, "Permanent", "Permanent" },
                    { 41, true, 12, "Temporary", "Temporary" },
                    { 42, true, 13, "Passed", "Passed" },
                    { 43, true, 13, "Temporary Deferral", "TemporaryDeferral" },
                    { 44, true, 13, "Permanent Deferral", "PermanentDeferral" },
                    { 45, true, 13, "Refused", "Refused" },
                    { 46, true, 14, "Routine", "Routine" },
                    { 47, true, 14, "STAT", "STAT" },
                    { 48, true, 14, "Schedule", "Schedule" },
                    { 49, true, 15, "Saline Phase Only", "SalinePhaseOnly" },
                    { 50, true, 15, "Saline Albumin Phase Only", "SalineAlbuminPhaseOnly" },
                    { 51, true, 15, "Saline Albumin Globulin Phase Only", "SalineAlbuminGlobulinPhaseOnly" },
                    { 52, true, 16, "Out Patient", "OutPatient" },
                    { 53, true, 16, "In Patient", "InPatient" },
                    { 54, true, 17, "GSIS", "GSIS" },
                    { 55, true, 17, "SSS", "SSS" },
                    { 56, true, 17, "OCW", "OCW" },
                    { 57, true, 17, "Dependent", "Dependent" },
                    { 58, true, 17, "Indigent", "Indigent" },
                    { 59, true, 18, "Major", "Major" },
                    { 60, true, 18, "Minor", "Minor" },
                    { 61, true, 18, "Neutral", "Neutral" },
                    { 62, true, 18, "AHG", "AHG" },
                    { 63, true, 6, "Inventory", "Inventory" }
                });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "UserRoleId", "RoleId", "UserAccountId" },
                values: new object[] { new Guid("ac4ad7ff-6169-41ed-8daa-1665d7d38d85"), new Guid("489abc53-af8a-4914-bd21-7d99c26e2e33"), new Guid("057f1f23-3015-4e71-81fc-0bae7f1db34b") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ApplicationSetting",
                keyColumn: "ApplicationSettingId",
                keyValue: new Guid("1891de09-2589-46da-ac21-f7053e2027aa"));

            migrationBuilder.DeleteData(
                table: "ApplicationSetting",
                keyColumn: "ApplicationSettingId",
                keyValue: new Guid("41f1542b-2a84-40bf-b39e-774c75265853"));

            migrationBuilder.DeleteData(
                table: "ApplicationSetting",
                keyColumn: "ApplicationSettingId",
                keyValue: new Guid("f89d1eaf-94f0-4993-93e0-b9175dbe8f04"));

            migrationBuilder.DeleteData(
                table: "BloodComponent",
                keyColumn: "BloodComponentId",
                keyValue: new Guid("2247d0fd-d546-40e7-b2f3-cfc64d6e48a0"));

            migrationBuilder.DeleteData(
                table: "BloodComponent",
                keyColumn: "BloodComponentId",
                keyValue: new Guid("601f8624-1984-4fb3-bd93-30dfb5ca2835"));

            migrationBuilder.DeleteData(
                table: "BloodComponent",
                keyColumn: "BloodComponentId",
                keyValue: new Guid("b03ac043-8569-4f7d-97d1-e8bbdb1a360a"));

            migrationBuilder.DeleteData(
                table: "BloodComponent",
                keyColumn: "BloodComponentId",
                keyValue: new Guid("d7431819-53dd-4bfc-a4e9-fed3139a4998"));

            migrationBuilder.DeleteData(
                table: "BloodTestType",
                keyColumn: "BloodTestTypeId",
                keyValue: new Guid("027d2030-29e8-4dd8-a227-92312815bdf8"));

            migrationBuilder.DeleteData(
                table: "BloodTestType",
                keyColumn: "BloodTestTypeId",
                keyValue: new Guid("29d3082d-e681-466a-aeb1-2ed9440fba11"));

            migrationBuilder.DeleteData(
                table: "BloodTestType",
                keyColumn: "BloodTestTypeId",
                keyValue: new Guid("94f2e936-5ba0-4bdb-8d31-0e2260f09075"));

            migrationBuilder.DeleteData(
                table: "BloodTestType",
                keyColumn: "BloodTestTypeId",
                keyValue: new Guid("a9e9fafe-9a2a-4d4f-b1da-ecd7f64af5ce"));

            migrationBuilder.DeleteData(
                table: "BloodTestType",
                keyColumn: "BloodTestTypeId",
                keyValue: new Guid("ba180ddd-c55c-419e-870a-ca45ec8f91d9"));

            migrationBuilder.DeleteData(
                table: "LookupOption",
                keyColumn: "LookupOptionId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "LookupOption",
                keyColumn: "LookupOptionId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "LookupOption",
                keyColumn: "LookupOptionId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "LookupOption",
                keyColumn: "LookupOptionId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "LookupOption",
                keyColumn: "LookupOptionId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "LookupOption",
                keyColumn: "LookupOptionId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "LookupOption",
                keyColumn: "LookupOptionId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "LookupOption",
                keyColumn: "LookupOptionId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "LookupOption",
                keyColumn: "LookupOptionId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "LookupOption",
                keyColumn: "LookupOptionId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "LookupOption",
                keyColumn: "LookupOptionId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "LookupOption",
                keyColumn: "LookupOptionId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "LookupOption",
                keyColumn: "LookupOptionId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "LookupOption",
                keyColumn: "LookupOptionId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "LookupOption",
                keyColumn: "LookupOptionId",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "LookupOption",
                keyColumn: "LookupOptionId",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "LookupOption",
                keyColumn: "LookupOptionId",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "LookupOption",
                keyColumn: "LookupOptionId",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "LookupOption",
                keyColumn: "LookupOptionId",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "LookupOption",
                keyColumn: "LookupOptionId",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "LookupOption",
                keyColumn: "LookupOptionId",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "LookupOption",
                keyColumn: "LookupOptionId",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "LookupOption",
                keyColumn: "LookupOptionId",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "LookupOption",
                keyColumn: "LookupOptionId",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "LookupOption",
                keyColumn: "LookupOptionId",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "LookupOption",
                keyColumn: "LookupOptionId",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "LookupOption",
                keyColumn: "LookupOptionId",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "LookupOption",
                keyColumn: "LookupOptionId",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "LookupOption",
                keyColumn: "LookupOptionId",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "LookupOption",
                keyColumn: "LookupOptionId",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "LookupOption",
                keyColumn: "LookupOptionId",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "LookupOption",
                keyColumn: "LookupOptionId",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "LookupOption",
                keyColumn: "LookupOptionId",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "LookupOption",
                keyColumn: "LookupOptionId",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "LookupOption",
                keyColumn: "LookupOptionId",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "LookupOption",
                keyColumn: "LookupOptionId",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "LookupOption",
                keyColumn: "LookupOptionId",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "LookupOption",
                keyColumn: "LookupOptionId",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "LookupOption",
                keyColumn: "LookupOptionId",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "LookupOption",
                keyColumn: "LookupOptionId",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "LookupOption",
                keyColumn: "LookupOptionId",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "LookupOption",
                keyColumn: "LookupOptionId",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "LookupOption",
                keyColumn: "LookupOptionId",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "LookupOption",
                keyColumn: "LookupOptionId",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "LookupOption",
                keyColumn: "LookupOptionId",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "LookupOption",
                keyColumn: "LookupOptionId",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "LookupOption",
                keyColumn: "LookupOptionId",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "LookupOption",
                keyColumn: "LookupOptionId",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "LookupOption",
                keyColumn: "LookupOptionId",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "LookupOption",
                keyColumn: "LookupOptionId",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "LookupOption",
                keyColumn: "LookupOptionId",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "LookupOption",
                keyColumn: "LookupOptionId",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "LookupOption",
                keyColumn: "LookupOptionId",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "LookupOption",
                keyColumn: "LookupOptionId",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "LookupOption",
                keyColumn: "LookupOptionId",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "LookupOption",
                keyColumn: "LookupOptionId",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "LookupOption",
                keyColumn: "LookupOptionId",
                keyValue: 57);

            migrationBuilder.DeleteData(
                table: "LookupOption",
                keyColumn: "LookupOptionId",
                keyValue: 58);

            migrationBuilder.DeleteData(
                table: "LookupOption",
                keyColumn: "LookupOptionId",
                keyValue: 59);

            migrationBuilder.DeleteData(
                table: "LookupOption",
                keyColumn: "LookupOptionId",
                keyValue: 60);

            migrationBuilder.DeleteData(
                table: "LookupOption",
                keyColumn: "LookupOptionId",
                keyValue: 61);

            migrationBuilder.DeleteData(
                table: "LookupOption",
                keyColumn: "LookupOptionId",
                keyValue: 62);

            migrationBuilder.DeleteData(
                table: "LookupOption",
                keyColumn: "LookupOptionId",
                keyValue: 63);

            migrationBuilder.DeleteData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "MedicalQuestionnaire",
                keyColumn: "MedicalQuestionnaireId",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: new Guid("22ee22cd-9580-4d5c-8e90-2d615283df1e"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: new Guid("69e19e84-eeae-4361-bf8d-53456def3994"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: new Guid("84148d25-7e25-4bdc-bfb5-d0f3cd729ab7"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: new Guid("9922d75f-0f7a-4fb2-ab1b-5f06b118136c"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: new Guid("b42f4287-089a-41f9-89ef-a5a3f474f754"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: new Guid("c2f7694a-1d5e-4754-8575-2c62cbfdf627"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: new Guid("c3f8818a-7a67-4d54-ba8e-3a5bd247e730"));

            migrationBuilder.DeleteData(
                table: "TestOrderType",
                keyColumn: "TestOrderTypeId",
                keyValue: new Guid("3b382719-3167-4ccd-941e-1cd6e1952372"));

            migrationBuilder.DeleteData(
                table: "TestOrderType",
                keyColumn: "TestOrderTypeId",
                keyValue: new Guid("5092b84e-ed91-4370-9984-42392ffa97f4"));

            migrationBuilder.DeleteData(
                table: "TestOrderType",
                keyColumn: "TestOrderTypeId",
                keyValue: new Guid("722bcb71-9a94-4734-857e-68ddb2711259"));

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumn: "UserRoleId",
                keyValue: new Guid("ac4ad7ff-6169-41ed-8daa-1665d7d38d85"));

            migrationBuilder.DeleteData(
                table: "Lookup",
                keyColumn: "LookupId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Lookup",
                keyColumn: "LookupId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Lookup",
                keyColumn: "LookupId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Lookup",
                keyColumn: "LookupId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Lookup",
                keyColumn: "LookupId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Lookup",
                keyColumn: "LookupId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Lookup",
                keyColumn: "LookupId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Lookup",
                keyColumn: "LookupId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Lookup",
                keyColumn: "LookupId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Lookup",
                keyColumn: "LookupId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Lookup",
                keyColumn: "LookupId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Lookup",
                keyColumn: "LookupId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Lookup",
                keyColumn: "LookupId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Lookup",
                keyColumn: "LookupId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Lookup",
                keyColumn: "LookupId",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Lookup",
                keyColumn: "LookupId",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Lookup",
                keyColumn: "LookupId",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Lookup",
                keyColumn: "LookupId",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: new Guid("489abc53-af8a-4914-bd21-7d99c26e2e33"));

            migrationBuilder.DeleteData(
                table: "UserAccount",
                keyColumn: "UserAccountId",
                keyValue: new Guid("057f1f23-3015-4e71-81fc-0bae7f1db34b"));
        }
    }
}
