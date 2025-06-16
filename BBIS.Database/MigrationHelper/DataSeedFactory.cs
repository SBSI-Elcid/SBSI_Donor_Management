using BBIS.Common.Enums;
using BBIS.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BBIS.Database.MigrationHelper
{
    public static class DataSeedFactory
    {
        public static void SeedData(ModelBuilder modelBuilder)
        {
            AddRoles(modelBuilder);
            AddDefaultUser(modelBuilder);
            AddQuestionTexts(modelBuilder);
            AddApplicationSettings(modelBuilder);
            AddBloodComponents(modelBuilder);
            AddBloodTestTypes(modelBuilder);
            AddTestOrderTypes(modelBuilder);
            LookupDataSeed.Setup(modelBuilder);
            LookupDataSeed.SetupLookupOptions(modelBuilder);
            AddModules(modelBuilder);
        }

        private static void AddRoles(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(
               new Role { RoleId = Guid.Parse("489abc53-af8a-4914-bd21-7d99c26e2e33"), RoleName = "Admin" },
               new Role { RoleId = Guid.Parse("c2f7694a-1d5e-4754-8575-2c62cbfdf627"), RoleName = "DonorAdmin" },
               new Role { RoleId = Guid.Parse("9922d75f-0f7a-4fb2-ab1b-5f06b118136c"), RoleName = "InitialScreener" },
               new Role { RoleId = Guid.Parse("22ee22cd-9580-4d5c-8e90-2d615283df1e"), RoleName = "PhysicalExamScreener" },
               new Role { RoleId = Guid.Parse("69e19e84-eeae-4361-bf8d-53456def3994"), RoleName = "BloodCollector" },
               new Role { RoleId = Guid.Parse("b42f4287-089a-41f9-89ef-a5a3f474f754"), RoleName = "InventoryUser" },
               new Role { RoleId = Guid.Parse("84148d25-7e25-4bdc-bfb5-d0f3cd729ab7"), RoleName = "RequisitionUser" },
               new Role { RoleId = Guid.Parse("c3f8818a-7a67-4d54-ba8e-3a5bd247e730"), RoleName = "MedTech" },

               // New Roles
               new Role { RoleId = Guid.Parse("6bd4aadf-4a7c-11f0-82bc-1c697a39b59b"), RoleName = "VitalSigns" },
               new Role { RoleId = Guid.Parse("6c5fac31-4a7c-11f0-82bc-1c697a39b59b"), RoleName = "Counselor" },
               new Role { RoleId = Guid.Parse("aa2ed04a-4a7b-11f0-82bc-1c697a39b59b"), RoleName = "MethodBloodCollection" },
               new Role { RoleId = Guid.Parse("b20c470c-4a7b-11f0-82bc-1c697a39b59b"), RoleName = "IssuanceOfBloodBag" },
               new Role { RoleId = Guid.Parse("c7b02c83-4a7b-11f0-82bc-1c697a39b59b"), RoleName = "PostDonationCare" }
           );
        }

        private static void AddDefaultUser(ModelBuilder modelBuilder)
        {
            var date = DateTime.ParseExact("06/08/2022", "MM/dd/yyyy", null);

            modelBuilder.Entity<UserAccount>().HasData(
                new UserAccount { 
                    UserAccountId = Guid.Parse("057f1f23-3015-4e71-81fc-0bae7f1db34b"), 
                    Username = "admin",
                    Firstname = "Admin",
                    Lastname = "Default",
                    EmailAddress = "serallain@gmail.com",
                    PasswordSalt = "ay94yItGEYzB02M7QkJ2CaZMs6CvuZI5EVMyE/5+",
                    Password = "52cMcbtmJdCm0zhkH0ZQJK6xThufij3Hk7SGN1Z0Kf8=", // @dm!n
                    IsActive = true,
                    IsDeleted = false,
                    UpdatedAt = date,
                    UpdatedBy = Guid.Parse("057f1f23-3015-4e71-81fc-0bae7f1db34b")
                }              
            );

            modelBuilder.Entity<UserRole>().HasData(
              new UserRole
              {
                    UserAccountId = Guid.Parse("057f1f23-3015-4e71-81fc-0bae7f1db34b"),
                    RoleId = Guid.Parse("489abc53-af8a-4914-bd21-7d99c26e2e33"),
                    UserRoleId = Guid.Parse("ac4ad7ff-6169-41ed-8daa-1665d7d38d85")
              }
            );
        }

        private static void AddQuestionTexts(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MedicalQuestionnaire>().HasData(
              new MedicalQuestionnaire
              {
                  MedicalQuestionnaireId = 1,
                  HeaderText = "Pre-Donation preparations",
                  QuestionEnglishText = "Slept at least 6 hours?",
                  QuestionTagalogText = "",
                  QuestionOtherDialectText = "Nakatulog kaba'g 6 ka oras o sobra?",
                  OrderNo = 1

              },
              new MedicalQuestionnaire
              {
                  MedicalQuestionnaireId = 2,
                  QuestionEnglishText = "Have you had any alcoholic drink in past 12 to 24 hours?",
                  QuestionTagalogText = "",
                  QuestionOtherDialectText = "Naka-inom ka ba'g makahubog nga ilimnon sa ning-aging 12 ngadto sa 24 ka oras?",
                  OrderNo = 2

              },
              new MedicalQuestionnaire
              {
                  MedicalQuestionnaireId = 3,
                  QuestionEnglishText = "Have you smoked in the past 3-4 hours?",
                  QuestionTagalogText = "",
                  QuestionOtherDialectText = "Nakasigarilyo ka ba sa ning-aging 3 o 4 ka oras?",
                  OrderNo = 3

              },
              new MedicalQuestionnaire
              {
                  MedicalQuestionnaireId = 4,
                  QuestionEnglishText = "Have you had a meal within the past 4 hours?",
                  QuestionTagalogText = "",
                  QuestionOtherDialectText = "Nakakaon ka ba sa ning-aging 4 ka oras?",
                  OrderNo = 4
              },
              new MedicalQuestionnaire
              {
                  MedicalQuestionnaireId = 5,
                  HeaderText = "Are you...",
                  QuestionEnglishText = "Feeling healthy today?",
                  QuestionTagalogText = "",
                  QuestionOtherDialectText = "Maayo ra ang pamati sa panglawas karon?",
                  OrderNo = 5
              },
              new MedicalQuestionnaire
              {
                  MedicalQuestionnaireId = 6,
                  HeaderText = "Currently taking medication?",
                  QuestionEnglishText = "Have you taken any medication from the deferral list?",
                  QuestionTagalogText = "",
                  QuestionOtherDialectText = "Aduna ka bay tambal nga na-inom nga nalista sa deferral list?",
                  OrderNo = 6
              },
              new MedicalQuestionnaire
              {
                  MedicalQuestionnaireId = 7,
                  QuestionEnglishText = "Have you received any vaccination?",
                  QuestionTagalogText = "",
                  QuestionOtherDialectText = "Adunay nadawat nga bakuna?",
                  OrderNo = 7
              },
              new MedicalQuestionnaire
              {
                  MedicalQuestionnaireId = 8,
                  HeaderText = "In the past three days...",
                  QuestionEnglishText = "Have you taken aspirin or anything that has aspirin in it?",
                  QuestionTagalogText = "",
                  QuestionOtherDialectText = "Nakatumar ka ba ug aspirin o uban pang tambal nga adunay sagol nga aspirin",
                  OrderNo = 8
              },
              new MedicalQuestionnaire
              {
                  MedicalQuestionnaireId = 9,
                  HeaderText = "ARE FOR FEMALE DONORS",
                  QuestionEnglishText = "Are you pregnant or have been pregnant before? Last Menstrual Period",
                  QuestionTagalogText = "",
                  QuestionOtherDialectText = "Mabdob kaba o niagi ka na ba ug pamabdos? Katapusang petsa sa regla",
                  GenderOption = "Female",
                  OrderNo = 9
              },
              new MedicalQuestionnaire
              {
                  MedicalQuestionnaireId = 10,
                  HeaderText = "In the past 12 weeks have you",
                  QuestionEnglishText = "Donated blood, platelet or plasma?",
                  QuestionTagalogText = "",
                  QuestionOtherDialectText = "Nakadonar ug dugo platelet o plasma?",
                  OrderNo = 10

              },
              new MedicalQuestionnaire
              {
                  MedicalQuestionnaireId = 11,
                  HeaderText = "In the past 12 months have you",
                  QuestionEnglishText = "Had a blood transfusion?",
                  QuestionTagalogText = "",
                  QuestionOtherDialectText = "Na-abunchan ug dugo?",
                  OrderNo = 11

              },
              new MedicalQuestionnaire
              {
                  MedicalQuestionnaireId = 12,
                  QuestionEnglishText = "Had surgical operation, dental extraction?",
                  QuestionTagalogText = "",
                  QuestionOtherDialectText = "Na-operahan o na-ibtan ug ngipon?",
                  OrderNo = 12

              },
              new MedicalQuestionnaire
              {
                  MedicalQuestionnaireId = 13,
                  QuestionEnglishText = "Had a tattoo, ear or body piercing, accidental contact with blood, needle-stick, and acupuncture?",
                  QuestionTagalogText = "",
                  QuestionOtherDialectText = "Nakapatatoo, patusok sa dunggan, o natuslokan ug dogum nga nahugawan ug dugo, nakapa- accupuncture?",
                  OrderNo = 13
              },
              new MedicalQuestionnaire
              {
                  MedicalQuestionnaireId = 14,
                  QuestionEnglishText = "Had sexual contact with high risk individuals?",
                  QuestionTagalogText = "",
                  QuestionOtherDialectText = "Nakighilawas ug usa nga risgo nga adunay impeksyon?",
                  OrderNo = 14
              },
              new MedicalQuestionnaire
              {
                  MedicalQuestionnaireId = 15,
                  QuestionEnglishText = "Had sexual contact with anyone in exchange for material or monetary gain?",
                  QuestionTagalogText = "",
                  QuestionOtherDialectText = "Nakighilawas aron mobayaran ka ug kwarta o laing hulip sa kwarta?",
                  OrderNo = 15
              },
              new MedicalQuestionnaire
              {
                  MedicalQuestionnaireId = 16,
                  QuestionEnglishText = "Had sexual contact with a person who has worked abroad?",
                  QuestionTagalogText = "",
                  QuestionOtherDialectText = "Nakighilawas ug usa nga nakatrabaho sa gawas sa nasud?",
                  OrderNo = 16
              },
              new MedicalQuestionnaire
              {
                  MedicalQuestionnaireId = 17,
                  QuestionEnglishText = "Engaged in casual sex?",
                  QuestionTagalogText = "",
                  QuestionOtherDialectText = "Nakighilawas bisan sa sinugdan wala tuyo-a o planoha?",
                  OrderNo = 17
              },
              new MedicalQuestionnaire
              {
                  MedicalQuestionnaireId = 18,
                  QuestionEnglishText = "Lived with a person who has hepatitis?",
                  QuestionTagalogText = "",
                  QuestionOtherDialectText = "Naka-ipon ug puyo sa usa ka adunay sakit nga hepatitis?",
                  OrderNo = 18
              },
              new MedicalQuestionnaire
              {
                  MedicalQuestionnaireId = 19,
                  QuestionEnglishText = "Have you been imprisoned??",
                  QuestionTagalogText = "",
                  QuestionOtherDialectText = "Nakasulay ug pagkapriso?",
                  OrderNo = 19
              },
              new MedicalQuestionnaire
              {
                  MedicalQuestionnaireId = 20,
                  QuestionEnglishText = "Have any of your relatives had Creutzfeldt-Jakob (Mad Cow) disease?",
                  QuestionTagalogText = "",
                  QuestionOtherDialectText = "Adunay paryente nga nasakit ug Creutzfeldt-Jakob (Mad Cow) disease?",
                  OrderNo = 20
              },
              new MedicalQuestionnaire
              {
                  MedicalQuestionnaireId = 21,
                  HeaderText = "Have you ever...",
                  QuestionEnglishText = "lived outside your place of residence?",
                  QuestionTagalogText = "",
                  QuestionOtherDialectText = "Nakapuyo gawas sa imong na-andan nga puloy-anan?",
                  OrderNo = 21
              },
              new MedicalQuestionnaire
              {
                  MedicalQuestionnaireId = 22,
                  QuestionEnglishText = "lived outside the Philippines?",
                  QuestionTagalogText = "",
                  QuestionOtherDialectText = "Nakapuyo gawas the Pilipinas?",
                  OrderNo = 22
              },
              new MedicalQuestionnaire
              {
                  MedicalQuestionnaireId = 23,
                  QuestionEnglishText = "Used needles to take drugs, steroids, or anything not prescribed by your doctor?",
                  QuestionTagalogText = "",
                  QuestionOtherDialectText = "Nakagamit ug dagum para maka-indyeksyon ug druga, steroids, o laing matang sa tambal nga walay riseta sa doctor?",
                  OrderNo = 23
              },
              new MedicalQuestionnaire
              {
                  MedicalQuestionnaireId = 24,
                  QuestionEnglishText = "Used clotting factor concentrates?",
                  QuestionTagalogText = "",
                  QuestionOtherDialectText = "Nahatagan ug mga clotting concentrates?",
                  OrderNo = 24
              },
              new MedicalQuestionnaire
              {
                  MedicalQuestionnaireId = 25,
                  QuestionEnglishText = "Known that if you have the AIDS/Hepatitis virus, you can give it to someone else though you may feel well and have a negative HIV/Hepatitis test?",
                  QuestionTagalogText = "",
                  QuestionOtherDialectText = "Nahibalo nga kung aduna kay impeksyon nga AIDS/Hepatitis, mahimo nimo kining mapanakod bisan ug wala kay gipamati sa panglawas ug negatibo ang resulta sa imong testing sa dugo?",
                  OrderNo = 25
              },
              new MedicalQuestionnaire
              {
                  MedicalQuestionnaireId = 26,
                  QuestionEnglishText = "Had a positive test for the HIV/AIDS virus, Hepatitis virus, Syphilis or Malaria?",
                  QuestionTagalogText = "",
                  QuestionOtherDialectText = "Nakapatesting ka sa imong dugo para sa HIV/AIDS virus, Hepatitis virus, Syphilis or Malaria ug positibo ang resulto sa bisan ha-in niini?",
                  OrderNo = 26
              },
              new MedicalQuestionnaire
              {
                  MedicalQuestionnaireId = 27,
                  QuestionEnglishText = "Had Hepatitis?",
                  QuestionTagalogText = "",
                  QuestionOtherDialectText = "Nagkasakit ug hepatitis?",
                  OrderNo = 27
              },
              new MedicalQuestionnaire
              {
                  MedicalQuestionnaireId = 28,
                  QuestionEnglishText = "Had malaria?",
                  QuestionTagalogText = "",
                  QuestionOtherDialectText = "Nagkasakit ug malaria?",
                  OrderNo = 28
              },
              new MedicalQuestionnaire
              {
                  MedicalQuestionnaireId = 29,
                  QuestionEnglishText = "Been told to have or treated for genital wart, syphilis, gonorrhea or other Sexually Transmissible Infections?",
                  QuestionTagalogText = "",
                  QuestionOtherDialectText = "Nogkasakit o nakapatambal para genital wart, syphilis, tulo (gonorrhea) o ubang laing mga sakit nga makuha sa pakighilawas?) 26. Had any type of cancer, for example leukemia? (nagkasakit ug kanser sama sa lukemya o uban pa?",
                  OrderNo = 29
              },
              new MedicalQuestionnaire
              {
                  MedicalQuestionnaireId = 30,
                  QuestionEnglishText = "Had any problems with your heart and lungs?",
                  QuestionTagalogText = "",
                  QuestionOtherDialectText = "Adunay sakit o problema sa baga o kasing-kasing?",
                  OrderNo = 30
              },
              new MedicalQuestionnaire
              {
                  MedicalQuestionnaireId = 31,
                  QuestionEnglishText = "Had a bleeding condition or a blood disease?",
                  QuestionTagalogText = "",
                  QuestionOtherDialectText = "Adunay problema sa pag-dugo or sakit sa dugo?",
                  OrderNo = 31
              },
              
              new MedicalQuestionnaire
              {
                  MedicalQuestionnaireId = 32,
                  QuestionEnglishText = "Are you giving blood because you wanted to be tested for HIV or Hepatitis virus?",
                  QuestionTagalogText = "",
                  QuestionOtherDialectText = "Mu-donar ug dugo tungod gusto ka magpa-testing sa HIV o hepatitis?",
                  OrderNo = 32
                  
              },
              new MedicalQuestionnaire
              {
                  MedicalQuestionnaireId = 33,
                  QuestionEnglishText = "Would you be willing to be recalled to donate blood at VSMMC - Blood Services facility should the need arise?",
                  QuestionTagalogText = "",
                  QuestionOtherDialectText = "Kung magkinahanglan, mutugot ka ba nga tawagon aron pag-ari sa VSMMC - Blood Services aron sa pagdonar ug dugo?",
                  OrderNo = 33
                  
              },
              new MedicalQuestionnaire
              {
                  MedicalQuestionnaireId = 34,
                  HeaderText = "Additional Screening Tool for Blood Donors Based on NVBSP Guidelines (DOH-NVBSP Department Memorandum No. 2020-0124)",
                  QuestionEnglishText = "Have you travelled outside the Philippines within the past 28 days? If YES, please indicate the specific country.",
                  QuestionTagalogText = "",
                  QuestionOtherDialectText = "Nakabiyahe ba ka sa gawas sa Pilipinas sa nilabay nga 28 ka adlaw? Kung oo, palihug isuwot kung asa nga lugar.",
                  OrderNo = 34
                  
              },
              new MedicalQuestionnaire
              {
                  MedicalQuestionnaireId = 35,
                  QuestionEnglishText = "Have you experienced OR have been exposed to any of the following symptoms within the past 14 days up to present?",
                  QuestionTagalogText = "",
                  QuestionOtherDialectText = "Aduna ka ba'y bisan unsa aning mga mosunod o nakahimamat ug bisan kinsa nga adunay usa sa mga mosunod gikan sa niaging 14 ka adlaw hangtud karon?",
                  OrderNo = 35
              },
              new MedicalQuestionnaire
              {
                  MedicalQuestionnaireId = 36,
                  QuestionEnglishText = "Fever, Cough, Sore throat, Shortness of breath or difficulty of breathing, Diarrhea. If YES, indicate the date symptoms started",
                  QuestionTagalogText = "",
                  QuestionOtherDialectText = "Hilanat,Ubo,Pagsakit sa tutunlan,Kalisud sa pagginhawa,Kalibanga. Kung aduna, isuwat ang petsa kanus-a nagsugod",
                  OrderNo = 36
              },
              new MedicalQuestionnaire
              {
                  MedicalQuestionnaireId = 37,
                  QuestionEnglishText = "Have you been identified as one of the following for COVID-19?",
                  QuestionTagalogText = "",
                  QuestionOtherDialectText = "Nailhan ka ba nga usa sa mga mosunod alang sa COVID-19?",
                  OrderNo = 37
              },
              new MedicalQuestionnaire
              {
                  MedicalQuestionnaireId = 38,
                  QuestionEnglishText = "COVID-19 suspect COVID-19 probable",
                  QuestionTagalogText = "",
                  QuestionOtherDialectText = "COVID-19 suspect COVID-19 probable",
                  OrderNo = 38
              },
              new MedicalQuestionnaire
              {
                  MedicalQuestionnaireId = 39,
                  QuestionEnglishText = "Confirmed (+) for COVID-19. If confirmed COVID-19(+), indicate the date of recovery",
                  QuestionTagalogText = "",
                  QuestionOtherDialectText = "Napamatud-an nga Positibo sa COVID-19. Kung nagpositibo sa COVID-19, isuwat ang petsa sa pagkaayo?",
                  OrderNo = 39
              },
              new MedicalQuestionnaire
              {
                  MedicalQuestionnaireId = 40,
                  QuestionEnglishText = "Have you been exposed to somebody who is identified or has any of the following within the past 28 days?",
                  QuestionTagalogText = "",
                  QuestionOtherDialectText = "Aduna ka bay nahimamat nga mga tawo nga usa o adunay bisan unsa sa mga mosunod sa niaging 28 ka adlaw?",
                  OrderNo = 40
              },
              new MedicalQuestionnaire
              {
                  MedicalQuestionnaireId = 41,
                  QuestionEnglishText = "COVID-19 suspect COVID-19 probable",
                  QuestionTagalogText = "",
                  QuestionOtherDialectText = "COVID-19 suspect COVID-19 probable",
                  OrderNo = 41
              },
              new MedicalQuestionnaire
              {
                  MedicalQuestionnaireId = 42,
                  QuestionEnglishText = "Confirmed (+) for COVID-19",
                  QuestionTagalogText = "",
                  QuestionOtherDialectText = "Napamatud-an nga Positibo sa COVID-19",
                  OrderNo = 42
              }
            );
        }

        private static void AddApplicationSettings(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationSetting>().HasData(
              new ApplicationSetting { ApplicationSettingId = Guid.Parse("1891de09-2589-46da-ac21-f7053e2027aa"), SettingKey = ApplicationSettingKeys.BloodCollectionUnitOfMeasure, SettingValue = "mL", IsActive = true },
              new ApplicationSetting { ApplicationSettingId = Guid.Parse("f89d1eaf-94f0-4993-93e0-b9175dbe8f04"), SettingKey = ApplicationSettingKeys.BloodComponentsUnitOfMeasure, SettingValue = "mL", IsActive = true },
              new ApplicationSetting { ApplicationSettingId = Guid.Parse("41f1542b-2a84-40bf-b39e-774c75265853"), SettingKey = ApplicationSettingKeys.InstitutionName, SettingValue = "SBSI", IsActive = true }
            );
        }

        private static void AddBloodComponents(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BloodComponent>().HasData(
                new BloodComponent { BloodComponentId = Guid.Parse("d7431819-53dd-4bfc-a4e9-fed3139a4998"), ComponentName = "Packed Red Blood Cells", ExpiryInDays = 35, IsActive = true },
                new BloodComponent { BloodComponentId = Guid.Parse("2247d0fd-d546-40e7-b2f3-cfc64d6e48a0"), ComponentName = "Fresh Frozen Plasma", ExpiryInDays = 365, IsActive = true },
                new BloodComponent { BloodComponentId = Guid.Parse("b03ac043-8569-4f7d-97d1-e8bbdb1a360a"), ComponentName = "Platelet Concentrate", ExpiryInDays = 5, IsActive = true },
                new BloodComponent { BloodComponentId = Guid.Parse("601f8624-1984-4fb3-bd93-30dfb5ca2835"), ComponentName = "Leukoreduced Red Blood Cells", ExpiryInDays = 5, IsActive = true }
            );
        }

        private static void AddBloodTestTypes(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BloodTestType>().HasData(
                new BloodTestType { BloodTestTypeId = Guid.Parse("ba180ddd-c55c-419e-870a-ca45ec8f91d9"), TypeName = "HIV", IsActive  = true },
                new BloodTestType { BloodTestTypeId = Guid.Parse("027d2030-29e8-4dd8-a227-92312815bdf8"), TypeName = "HCV", IsActive = true },
                new BloodTestType { BloodTestTypeId = Guid.Parse("a9e9fafe-9a2a-4d4f-b1da-ecd7f64af5ce"), TypeName = "Syphilis", IsActive = true },
                new BloodTestType { BloodTestTypeId = Guid.Parse("29d3082d-e681-466a-aeb1-2ed9440fba11"), TypeName = "HBsAg", IsActive = true },
                new BloodTestType { BloodTestTypeId = Guid.Parse("94f2e936-5ba0-4bdb-8d31-0e2260f09075"), TypeName = "Malaria", IsActive = true }
            );
        }
           
        private static void AddTestOrderTypes(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TestOrderType>().HasData(
               //new TestOrderType { TestOrderTypeId = Guid.Parse("be4ba590-7c4b-42ec-8ea2-ef1519dcc5b7"), Code = "XMatch", Name = "Compatibility Testing", Description = "Cross match testing", IsActive = true },
               new TestOrderType { TestOrderTypeId = Guid.Parse("722bcb71-9a94-4734-857e-68ddb2711259"), Code = "BTyping", Name = "Blood Typing", Description = "Blood Typing", IsActive = true },
               new TestOrderType { TestOrderTypeId = Guid.Parse("3b382719-3167-4ccd-941e-1cd6e1952372"), Code = "BScreen", Name = "Blood Screening", Description = "Blood Screening", IsActive = true },
               new TestOrderType { TestOrderTypeId = Guid.Parse("5092b84e-ed91-4370-9984-42392ffa97f4"), Code = "Coombs", Name = "Coombs Test", Description = "Coombs Test", IsActive = true }
            );
        }

        private static void AddModules(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Module>().HasData(
                new Module { OrderNo = 1, ModuleId = Guid.Parse("3f5db581-1c6f-46b0-a7dd-240fb5b47d98"), Menu = "Registrations", Icon = "mdi-account-details-outline", Link = "/registrations", IsActive = true, IsParentMenu = false },    
                new Module { OrderNo = 2, ModuleId = Guid.Parse("11826fd9-ebd9-47de-be2c-fb6a355eb228"), Menu = "Donors", Icon = "mdi-account-group-outline", Link = "/donors", IsActive = true, IsParentMenu = false },
                new Module { OrderNo = 3, ModuleId = Guid.Parse("2ba843a3-d26d-4c11-ab66-462e1ea4b491"), Menu = "Schedule Services", Icon = "mdi-calendar-arrow-right", Link = "/schedules", IsActive = true, IsParentMenu = false },
                new Module { OrderNo = 4, ModuleId = Guid.Parse("bc8d57ab-af77-4ec3-a348-5d040802f6e3"), Menu = "Test Orders", Icon = "mdi-hand-coin-outline", Link = "/orders/donors", IsActive = true, IsParentMenu = false },    
                new Module { OrderNo = 5, ModuleId = Guid.Parse("a0fe13b2-3b07-468f-91e8-b74b26b64f12"), Menu = "Requisitions", Icon = "mdi-form-select", Link = "/requisitions/availability", IsActive = true, IsParentMenu = false },    
                new Module { OrderNo = 6, ModuleId = Guid.Parse("7402be5a-bc2f-47b9-a389-b2c72c7173ba"), Menu = "Patients", Icon = "mdi-account-injury", Link = "/patients", IsActive = true, IsParentMenu = false },    
                new Module { OrderNo = 7, ModuleId = Guid.Parse("be14af28-ad5b-46b3-80a0-9952a98b876e"), Menu = "Inventory", Icon = "mdi-table", Link = "/inventory", IsActive = true, IsParentMenu = false },    
                new Module { OrderNo = 8, ModuleId = Guid.Parse("bec0b869-882f-4661-8bc1-856f6accca16"), Menu = "Reports", Icon = "mdi-chart-bar-stacked", Link = "/reports", IsActive = true, IsParentMenu = false },    
                new Module { OrderNo = 9, ModuleId = Guid.Parse("f0fa95f2-2288-434f-8df6-71f00e9be302"), Menu = "Setting", Icon = "mdi-cog", Link = "/setting", IsActive = true, IsParentMenu = true },    
                new Module { OrderNo = 10, ModuleId = Guid.Parse("53223248-2221-4d48-a551-8477a33e96c4"), Menu = "Users", Icon = "mdi-account-cog-outline", Link = "/users", IsActive = true, IsParentMenu = false, ParentModuleId = Guid.Parse("f0fa95f2-2288-434f-8df6-71f00e9be302") },    
                new Module { OrderNo = 11, ModuleId = Guid.Parse("22a8eb15-168c-470a-a680-71e85b8514c6"), Menu = "App Setting", Icon = "mdi-cogs", Link = "/app-settings/unit-of-measurement", IsActive = true, IsParentMenu = false, ParentModuleId = Guid.Parse("f0fa95f2-2288-434f-8df6-71f00e9be302") },   
                new Module { OrderNo = 12, ModuleId = Guid.Parse("68b94e71-abf0-4989-aa29-9ec6d6419a9c"), Menu = "Signatories", Icon = "mdi-draw", Link = "/signatories", IsActive = true, IsParentMenu = false, ParentModuleId = Guid.Parse("f0fa95f2-2288-434f-8df6-71f00e9be302") }    
            );
        }
    }
}
