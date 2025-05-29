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
               new Role { RoleId = Guid.Parse("c3f8818a-7a67-4d54-ba8e-3a5bd247e730"), RoleName = "MedTech" }
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
                  QuestionTagalogText = "Do you feel well and healthy today?",
                  OrderNo = 1

              },
              new MedicalQuestionnaire
              {
                  MedicalQuestionnaireId = 2,
                  QuestionTagalogText = "Have you been refused as a blood donor or told not to donate blood?",
                  OrderNo = 2

              },
              new MedicalQuestionnaire
              {
                  MedicalQuestionnaireId = 3,
                  QuestionTagalogText = "Will you voluntary allow your blood to be extracted for testing of HIV/AIDS, Hepatitis virus or other contagious diseases?",
                  OrderNo = 3

              },
              new MedicalQuestionnaire
              {
                  MedicalQuestionnaireId = 4,
                  QuestionTagalogText = "Are you aware that a person with HIV/Hepatitis can still infect other people even if the HIV/AIDS/Hepatitis test result is negative?",
                  OrderNo = 4
              },
              new MedicalQuestionnaire
              {
                  MedicalQuestionnaireId = 5,
                  QuestionTagalogText = "Have you within the last 12 hours had alcohol?",
                  OrderNo = 5
              },
              new MedicalQuestionnaire
              {
                  MedicalQuestionnaireId = 6,
                  QuestionTagalogText = "Have you taken aspirin within the last 3 days?",
                  OrderNo = 6
              },
              new MedicalQuestionnaire
              {
                  MedicalQuestionnaireId = 7,
                  QuestionTagalogText = "Have you taken any medication or have been vaccinated within the last 4 weeks?",
                  OrderNo = 7
              },
              new MedicalQuestionnaire
              {
                  MedicalQuestionnaireId = 8,
                  QuestionTagalogText = "Have you donated blood, platelets or plasma for the last 3 months?",
                  OrderNo = 8
              },
              new MedicalQuestionnaire
              {
                  MedicalQuestionnaireId = 9,
                  HeaderText = "FOR THE PAST 6 MONTHS HAVE YOU:",
                  QuestionTagalogText = "Gone to places in the Philippines or other countries known to have ZIKA virus?",
                  OrderNo = 9
              },
              new MedicalQuestionnaire
              {
                  MedicalQuestionnaireId = 10,
                  QuestionTagalogText = "Had intercourse with a person known and confirmed to be infected with ZIKA virus?",
                  OrderNo = 10

              },
              new MedicalQuestionnaire
              {
                  MedicalQuestionnaireId = 11,
                  QuestionTagalogText = "Had intercource with a person who had gone to places in the Philippines or other countries known to have ZIKA virus?",
                  OrderNo = 11

              },
              new MedicalQuestionnaire
              {
                  MedicalQuestionnaireId = 12,
                  HeaderText = "FOR THE PAST 12 MONTHS HAVE YOU:",
                  QuestionTagalogText = "Been a recipient of donated blood for hemophilia or have been operated on or a recipient of organ transplant?",
                  OrderNo = 12

              },
              new MedicalQuestionnaire
              {
                  MedicalQuestionnaireId = 13,
                  QuestionTagalogText = "Been medically operated on or had tooth extraction?",
                  OrderNo = 13
              },
              new MedicalQuestionnaire
              {
                  MedicalQuestionnaireId = 14,
                  QuestionTagalogText = "Had tattoo, ear or body piercing, acupuncture, pricked by a needle or accidentally came in contact with blood?",
                  OrderNo = 14
              },
              new MedicalQuestionnaire
              {
                  MedicalQuestionnaireId = 15,
                  QuestionTagalogText = "Had intercourse with a person with high possibility of not being safe or in exchange for monetary or material things?",
                  OrderNo = 15
              },
              new MedicalQuestionnaire
              {
                  MedicalQuestionnaireId = 16,
                  QuestionTagalogText = "Had intercourse with a person without protected or deemed not safe?",
                  OrderNo = 16
              },
              new MedicalQuestionnaire
              {
                  MedicalQuestionnaireId = 17,
                  QuestionTagalogText = "Had experience having jaundice, liver disease or mingled with those with liver diseases?",
                  OrderNo = 17
              },
              new MedicalQuestionnaire
              {
                  MedicalQuestionnaireId = 18,
                  QuestionTagalogText = "Been locked up or imprisoned?",
                  OrderNo = 18
              },
              new MedicalQuestionnaire
              {
                  MedicalQuestionnaireId = 19,
                  QuestionTagalogText = "Lived or had relatives in the United Kingdom or Europe?",
                  OrderNo = 19
              },
              new MedicalQuestionnaire
              {
                  MedicalQuestionnaireId = 20,
                  HeaderText = "HAVE YOU:",
                  QuestionTagalogText = "Travelled or lived apart from your current residence or outside the Philippines?",
                  OrderNo = 20
              },
              new MedicalQuestionnaire
              {
                  MedicalQuestionnaireId = 21,
                  QuestionTagalogText = "Used prohibited drugs? (inducing, sniffing or injecting)",
                  OrderNo = 21
              },
              new MedicalQuestionnaire
              {
                  MedicalQuestionnaireId = 22,
                  QuestionTagalogText = "Taken medications for stopping and treating abnormal bleeding?",
                  OrderNo = 22
              },
              new MedicalQuestionnaire
              {
                  MedicalQuestionnaireId = 23,
                  QuestionTagalogText = "Tested positive for HIV, Hepatitis, Syphilis or Malaria?",
                  OrderNo = 23
              },
              new MedicalQuestionnaire
              {
                  MedicalQuestionnaireId = 24,
                  QuestionTagalogText = "Had malaria or liver disease?",
                  OrderNo = 24
              },
              new MedicalQuestionnaire
              {
                  MedicalQuestionnaireId = 25,
                  QuestionTagalogText = "Had or treated warts in the sensitive parts of the body, syphilis, gonorrhea or any other sexually transmitted diseases?",
                  OrderNo = 25
              },
              new MedicalQuestionnaire
              {
                  MedicalQuestionnaireId = 26,
                  HeaderText = "HAVE YOU HAD THE FOLLOWING:",
                  QuestionTagalogText = "Cancer, blood disease or hemophilia?",
                  OrderNo = 26
              },
              new MedicalQuestionnaire
              {
                  MedicalQuestionnaireId = 27,
                  QuestionTagalogText = "Heart disease or chest pain?",
                  OrderNo = 27
              },
              new MedicalQuestionnaire
              {
                  MedicalQuestionnaireId = 28,
                  QuestionTagalogText = "Lung disease, tuberculosis or asthma?",
                  OrderNo = 28
              },
              new MedicalQuestionnaire
              {
                  MedicalQuestionnaireId = 29,
                  QuestionTagalogText = "Kidney dusease, diabetes or epilepsy?",
                  OrderNo = 29
              },
              new MedicalQuestionnaire
              {
                  MedicalQuestionnaireId = 30,
                  QuestionTagalogText = "Small pox, canker sore or ulcers?",
                  OrderNo = 30
              },
              new MedicalQuestionnaire
              {
                  MedicalQuestionnaireId = 31,
                  QuestionTagalogText = "Other diseases not mentioned or had an operation?",
                  OrderNo = 31
              },
              new MedicalQuestionnaire
              {
                  MedicalQuestionnaireId = 32,
                  QuestionTagalogText = "Rashes or fever? Is it simultaneous with body aches, rheumatism or eye redness?",
                  OrderNo = 32
              },
              new MedicalQuestionnaire
              {
                  MedicalQuestionnaireId = 33,
                  HeaderText = "FOR FEMALE ONLY:",
                  QuestionTagalogText = "Are you currently pregnant? Have you been pregnant in the past?",
                  OrderNo = 33,
                  GenderOption = "Female"
              },
              new MedicalQuestionnaire
              {
                  MedicalQuestionnaireId = 34,
                  QuestionTagalogText = "When was the last time you have given birth?",
                  OrderNo = 34,
                  GenderOption = "Female"
              },
              new MedicalQuestionnaire
              {
                  MedicalQuestionnaireId = 35,
                  QuestionTagalogText = "For the past year, have you had miscarriage or abortion?",
                  OrderNo = 35,
                  GenderOption = "Female"
              },
              new MedicalQuestionnaire
              {
                  MedicalQuestionnaireId = 36,
                  QuestionTagalogText = "Are you currently breast feeding?",
                  OrderNo = 36,
                  GenderOption = "Female"
              },
              new MedicalQuestionnaire
              {
                  MedicalQuestionnaireId = 37,
                  QuestionTagalogText = "When was the last time you had your menstruation?",
                  OrderNo = 37,
                  GenderOption = "Female"
              },
              new MedicalQuestionnaire
              {
                  MedicalQuestionnaireId = 38,
                  HeaderText = "ACCEPTANCE CRITERIA IN RELATION TO COVID-19:",
                  QuestionTagalogText = "Do you have colds, coughs and/or fever today?",
                  OrderNo = 38
              },
              new MedicalQuestionnaire
              {
                  MedicalQuestionnaireId = 39,
                  QuestionTagalogText = "Were you tested for COVID 19 infection? Result and date of last test?",
                  OrderNo = 39
              },
              new MedicalQuestionnaire
              {
                  MedicalQuestionnaireId = 40,
                  QuestionTagalogText = "Were you diagnosed with COVID 19 infection? Date of last test and treatment?",
                  OrderNo = 40
              },
              new MedicalQuestionnaire
              {
                  MedicalQuestionnaireId = 41,
                  QuestionTagalogText = "In the last 14 days, have you had close contact with a confirmed case of COVID-19 or cared for an infected patient?",
                  OrderNo = 41
              },
              new MedicalQuestionnaire
              {
                  MedicalQuestionnaireId = 42,
                  QuestionTagalogText = "In the last 14 days, have you travelled from areas with known community transmissions or any place outside the Philippines?",
                  OrderNo = 42
              },
              new MedicalQuestionnaire
              {
                  MedicalQuestionnaireId = 43,
                  QuestionTagalogText = "In the last 4 weeks have you come in close contact with wild or exotic animals including its products?",
                  OrderNo = 43
              },
              new MedicalQuestionnaire
              {
                  MedicalQuestionnaireId = 44,
                  QuestionTagalogText = "In the last 4 weeks, have you eaten raw or uncooked meat and drank unprocessed milk?",
                  OrderNo = 44
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
