using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BBIS.Database.Migrations
{
    public partial class Create_Database : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ApplicationSetting",
                columns: table => new
                {
                    ApplicationSettingId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    SettingKey = table.Column<string>(type: "varchar(100)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SettingValue = table.Column<string>(type: "varchar(100)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationSetting", x => x.ApplicationSettingId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "BloodComponent",
                columns: table => new
                {
                    BloodComponentId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ComponentName = table.Column<string>(type: "varchar(60)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ExpiryInDays = table.Column<int>(type: "int", nullable: false),
                    NotifyOnDaysBefore = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BloodComponent", x => x.BloodComponentId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "BloodTestType",
                columns: table => new
                {
                    BloodTestTypeId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    TypeName = table.Column<string>(type: "varchar(80)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BloodTestType", x => x.BloodTestTypeId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Donor",
                columns: table => new
                {
                    DonorId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Lastname = table.Column<string>(type: "varchar(90)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Firstname = table.Column<string>(type: "varchar(90)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Middlename = table.Column<string>(type: "varchar(90)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BirthDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CivilStatus = table.Column<string>(type: "varchar(20)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Gender = table.Column<string>(type: "varchar(10)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AddressNo = table.Column<string>(type: "varchar(20)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AddressStreet = table.Column<string>(type: "varchar(50)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AddressBarangay = table.Column<string>(type: "varchar(60)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AddressMunicipality = table.Column<string>(type: "varchar(50)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AddressProvinceOrCity = table.Column<string>(type: "varchar(70)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AddressZipcode = table.Column<string>(type: "varchar(10)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OfficeAddress = table.Column<string>(type: "varchar(250)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Religion = table.Column<string>(type: "varchar(60)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Nationality = table.Column<string>(type: "varchar(50)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Education = table.Column<string>(type: "varchar(70)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    WorkName = table.Column<string>(type: "varchar(90)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TelNo = table.Column<string>(type: "varchar(30)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MobileNo = table.Column<string>(type: "varchar(20)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EmailAddress = table.Column<string>(type: "varchar(60)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SchoolIdNo = table.Column<string>(type: "varchar(20)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CompanyIdNo = table.Column<string>(type: "varchar(20)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PRCNo = table.Column<string>(type: "varchar(30)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DriverLicenseNo = table.Column<string>(type: "varchar(30)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SssGsisBirNo = table.Column<string>(type: "varchar(40)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OtherNo = table.Column<string>(type: "varchar(20)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Donor", x => x.DonorId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DonorRegistration",
                columns: table => new
                {
                    DonorRegistrationId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    RegistrationNumber = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Lastname = table.Column<string>(type: "varchar(90)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Firstname = table.Column<string>(type: "varchar(90)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Middlename = table.Column<string>(type: "varchar(90)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BirthDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CivilStatus = table.Column<string>(type: "varchar(20)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Gender = table.Column<string>(type: "varchar(10)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AddressNo = table.Column<string>(type: "varchar(20)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AddressStreet = table.Column<string>(type: "varchar(50)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AddressBarangay = table.Column<string>(type: "varchar(60)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AddressMunicipality = table.Column<string>(type: "varchar(50)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AddressProvinceOrCity = table.Column<string>(type: "varchar(70)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AddressZipcode = table.Column<string>(type: "varchar(10)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OfficeAddress = table.Column<string>(type: "varchar(250)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Religion = table.Column<string>(type: "varchar(60)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Nationality = table.Column<string>(type: "varchar(50)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Education = table.Column<string>(type: "varchar(70)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    WorkName = table.Column<string>(type: "varchar(90)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TelNo = table.Column<string>(type: "varchar(30)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MobileNo = table.Column<string>(type: "varchar(20)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EmailAddress = table.Column<string>(type: "varchar(60)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SchoolIdNo = table.Column<string>(type: "varchar(20)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CompanyIdNo = table.Column<string>(type: "varchar(20)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PRCNo = table.Column<string>(type: "varchar(30)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DriverLicenseNo = table.Column<string>(type: "varchar(30)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SssGsisBirNo = table.Column<string>(type: "varchar(40)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OtherNo = table.Column<string>(type: "varchar(20)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RegistrationDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonorRegistration", x => x.DonorRegistrationId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Institution",
                columns: table => new
                {
                    InstitutionId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "varchar(100)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Address = table.Column<string>(type: "varchar(200)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ContactNo = table.Column<string>(type: "varchar(20)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EmailAddress = table.Column<string>(type: "varchar(80)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Institution", x => x.InstitutionId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Lookup",
                columns: table => new
                {
                    LookupId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    LookupKey = table.Column<string>(type: "varchar(30)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "varchar(100)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lookup", x => x.LookupId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "MedicalQuestionnaire",
                columns: table => new
                {
                    MedicalQuestionnaireId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    HeaderText = table.Column<string>(type: "varchar(200)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    QuestionTagalogText = table.Column<string>(type: "varchar(500)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    QuestionEnglishText = table.Column<string>(type: "varchar(500)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OrderNo = table.Column<int>(type: "int", nullable: false),
                    GenderOption = table.Column<string>(type: "varchar(10)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalQuestionnaire", x => x.MedicalQuestionnaireId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Patient",
                columns: table => new
                {
                    PatientId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    BloodType = table.Column<string>(type: "varchar(4)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Rh = table.Column<string>(type: "varchar(13)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PatientIdNo = table.Column<string>(type: "varchar(15)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastName = table.Column<string>(type: "varchar(50)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FirstName = table.Column<string>(type: "varchar(50)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MiddleName = table.Column<string>(type: "varchar(50)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Gender = table.Column<string>(type: "varchar(7)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CivilStatus = table.Column<string>(type: "varchar(15)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DateOfBirth = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ContactNumber = table.Column<string>(type: "varchar(25)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Address = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedBy = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CreatedOn = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patient", x => x.PatientId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    RoleId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    RoleName = table.Column<string>(type: "VARCHAR(50)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.RoleId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Signatory",
                columns: table => new
                {
                    SignatoryId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    FirstName = table.Column<string>(type: "varchar(50)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MiddleName = table.Column<string>(type: "varchar(50)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastName = table.Column<string>(type: "varchar(50)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Position = table.Column<string>(type: "varchar(50)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PositionPrefix = table.Column<string>(type: "varchar(10)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LicenseNo = table.Column<string>(type: "varchar(30)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Signatory", x => x.SignatoryId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TestOrderType",
                columns: table => new
                {
                    TestOrderTypeId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Code = table.Column<string>(type: "varchar(10)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "varchar(100)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "varchar(200)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestOrderType", x => x.TestOrderTypeId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "UserAccount",
                columns: table => new
                {
                    UserAccountId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Username = table.Column<string>(type: "varchar(45)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Firstname = table.Column<string>(type: "varchar(50)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Lastname = table.Column<string>(type: "varchar(50)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EmailAddress = table.Column<string>(type: "varchar(150)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Password = table.Column<string>(type: "varchar(250)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PasswordSalt = table.Column<string>(type: "varchar(200)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ChangePasswordOnLogin = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAccount", x => x.UserAccountId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "BloodComponentChecklist",
                columns: table => new
                {
                    BloodComponentChecklistId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    BloodComponentId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ChecklistDescription = table.Column<string>(type: "varchar(500)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsAdult = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BloodComponentChecklist", x => x.BloodComponentChecklistId);
                    table.ForeignKey(
                        name: "FK_BloodComponentChecklist_BloodComponent_BloodComponentId",
                        column: x => x.BloodComponentId,
                        principalTable: "BloodComponent",
                        principalColumn: "BloodComponentId");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DonorRecentDonation",
                columns: table => new
                {
                    DonorRecentDonationId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    DonorId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    NumberOfDonation = table.Column<int>(type: "int", nullable: false),
                    DateOfRecentDonation = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    PlaceOfRecentDonation = table.Column<string>(type: "varchar(90)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Agency = table.Column<string>(type: "varchar(30)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonorRecentDonation", x => x.DonorRecentDonationId);
                    table.ForeignKey(
                        name: "FK_DonorRecentDonation_Donor_DonorId",
                        column: x => x.DonorId,
                        principalTable: "Donor",
                        principalColumn: "DonorId");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DonorTransaction",
                columns: table => new
                {
                    DonorTransactionId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    DonorRegistrationId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    DonorId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    PRCBloodDonorNumber = table.Column<string>(type: "varchar(30)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DOHNBBNetsBarcode = table.Column<string>(type: "varchar(30)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DateOfDonation = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UnitSerialNumber = table.Column<string>(type: "varchar(30)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BloodIsSafeToTransfuse = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DonorStatus = table.Column<string>(type: "varchar(25)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FinalBloodType = table.Column<string>(type: "varchar(4)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BloodRh = table.Column<string>(type: "varchar(15)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsOffline = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsSync = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonorTransaction", x => x.DonorTransactionId);
                    table.ForeignKey(
                        name: "FK_DonorTransaction_Donor_DonorId",
                        column: x => x.DonorId,
                        principalTable: "Donor",
                        principalColumn: "DonorId");
                    table.ForeignKey(
                        name: "FK_DonorTransaction_DonorRegistration_DonorRegistrationId",
                        column: x => x.DonorRegistrationId,
                        principalTable: "DonorRegistration",
                        principalColumn: "DonorRegistrationId");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "LookupOption",
                columns: table => new
                {
                    LookupOptionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    LookupId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "varchar(50)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Value = table.Column<string>(type: "varchar(50)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LookupOption", x => x.LookupOptionId);
                    table.ForeignKey(
                        name: "FK_LookupOption_Lookup_LookupId",
                        column: x => x.LookupId,
                        principalTable: "Lookup",
                        principalColumn: "LookupId");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DonorMedicalHistory",
                columns: table => new
                {
                    DonorMedicalHistoryId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    DonorRegistrationId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    MedicalQuestionnaireId = table.Column<int>(type: "int", nullable: false),
                    Answer = table.Column<string>(type: "varchar(10)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Remarks = table.Column<string>(type: "varchar(100)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonorMedicalHistory", x => x.DonorMedicalHistoryId);
                    table.ForeignKey(
                        name: "FK_DonorMedicalHistory_DonorRegistration_DonorRegistrationId",
                        column: x => x.DonorRegistrationId,
                        principalTable: "DonorRegistration",
                        principalColumn: "DonorRegistrationId");
                    table.ForeignKey(
                        name: "FK_DonorMedicalHistory_MedicalQuestionnaire_MedicalQuestionnair~",
                        column: x => x.MedicalQuestionnaireId,
                        principalTable: "MedicalQuestionnaire",
                        principalColumn: "MedicalQuestionnaireId");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Reservation",
                columns: table => new
                {
                    ReservationId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    PatientId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    PatientType = table.Column<string>(type: "varchar(15)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PriorityLevel = table.Column<string>(type: "varchar(15)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ForAdult = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsEmergency = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    RoomNo = table.Column<string>(type: "varchar(10)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Membership = table.Column<string>(type: "varchar(10)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RequestedBy = table.Column<string>(type: "varchar(55)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RequestedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    PreviousTransfusionDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    PreviousNoOfUnitsTransfused = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<string>(type: "varchar(15)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Expiration = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CreatedOn = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservation", x => x.ReservationId);
                    table.ForeignKey(
                        name: "FK_Reservation_Patient_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patient",
                        principalColumn: "PatientId");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "UserRole",
                columns: table => new
                {
                    UserRoleId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    RoleId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    UserAccountId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => x.UserRoleId);
                    table.ForeignKey(
                        name: "FK_UserRole_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "RoleId");
                    table.ForeignKey(
                        name: "FK_UserRole_UserAccount_UserAccountId",
                        column: x => x.UserAccountId,
                        principalTable: "UserAccount",
                        principalColumn: "UserAccountId");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DonorBloodCollection",
                columns: table => new
                {
                    DonorBloodCollectionId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    DonorTransactionId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CollectionType = table.Column<string>(type: "varchar(15)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CollectionSubType = table.Column<string>(type: "varchar(15)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CollectedBloodAmount = table.Column<double>(type: "double", nullable: false),
                    UnitOfMeasurement = table.Column<string>(type: "varchar(10)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Success = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    UnwellReason = table.Column<string>(type: "varchar(100)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MedicationGiven = table.Column<string>(type: "varchar(100)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FacilitatedById = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    StartTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonorBloodCollection", x => x.DonorBloodCollectionId);
                    table.ForeignKey(
                        name: "FK_DonorBloodCollection_DonorTransaction_DonorTransactionId",
                        column: x => x.DonorTransactionId,
                        principalTable: "DonorTransaction",
                        principalColumn: "DonorTransactionId");
                    table.ForeignKey(
                        name: "FK_DonorBloodCollection_FacilitatedById",
                        column: x => x.FacilitatedById,
                        principalTable: "UserAccount",
                        principalColumn: "UserAccountId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DonorDeferral",
                columns: table => new
                {
                    DonorDeferralId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    DonorTransactionId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    DeferralStatus = table.Column<string>(type: "varchar(15)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Remarks = table.Column<string>(type: "varchar(100)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonorDeferral", x => x.DonorDeferralId);
                    table.ForeignKey(
                        name: "FK_DonorDeferral_DonorTransaction_DonorTransactionId",
                        column: x => x.DonorTransactionId,
                        principalTable: "DonorTransaction",
                        principalColumn: "DonorTransactionId");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DonorInitialScreening",
                columns: table => new
                {
                    DonorInitialScreeningId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    DonorTransactionId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Weight = table.Column<double>(type: "double", nullable: false),
                    SPGR = table.Column<double>(type: "double", nullable: false),
                    HGB = table.Column<double>(type: "double", nullable: false),
                    HCT = table.Column<double>(type: "double", nullable: false),
                    RBC = table.Column<double>(type: "double", nullable: false),
                    WBC = table.Column<double>(type: "double", nullable: false),
                    PLTCount = table.Column<double>(type: "double", nullable: false),
                    BloodType = table.Column<string>(type: "varchar(10)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DonationType = table.Column<string>(type: "varchar(25)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    InHouseTypeValue = table.Column<string>(type: "varchar(20)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MobileBloodDonationPlace = table.Column<string>(type: "varchar(100)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MobileBloodDonationOrganizer = table.Column<string>(type: "varchar(50)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NameOfPatient = table.Column<string>(type: "varchar(100)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PatientHospital = table.Column<string>(type: "varchar(100)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PatientBloodType = table.Column<string>(type: "varchar(5)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PatientWBOrComponent = table.Column<string>(type: "varchar(10)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PatientNoOfUnits = table.Column<string>(type: "varchar(10)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PrcOffice = table.Column<string>(type: "varchar(70)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ScreenById = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ScreenDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonorInitialScreening", x => x.DonorInitialScreeningId);
                    table.ForeignKey(
                        name: "FK_DonorInitialScreening_DonorTransaction_DonorTransactionId",
                        column: x => x.DonorTransactionId,
                        principalTable: "DonorTransaction",
                        principalColumn: "DonorTransactionId");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DonorPhysicalExamination",
                columns: table => new
                {
                    DonorPhysicalExaminationId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    DonorTransactionId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    BloodPressure = table.Column<string>(type: "varchar(10)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Pulse = table.Column<double>(type: "double", nullable: false),
                    Temperature = table.Column<double>(type: "double", nullable: false),
                    GeneralStatus = table.Column<string>(type: "varchar(50)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Skin = table.Column<string>(type: "varchar(50)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    HEENT = table.Column<string>(type: "varchar(50)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    HeartAndLungs = table.Column<string>(type: "varchar(50)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ResultStatus = table.Column<string>(type: "varchar(20)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FailedRemarks = table.Column<string>(type: "varchar(200)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BloodBagType = table.Column<string>(type: "varchar(15)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DoctorName = table.Column<string>(type: "varchar(60)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FacilitatedById = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    DateOfExamination = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonorPhysicalExamination", x => x.DonorPhysicalExaminationId);
                    table.ForeignKey(
                        name: "FK_DonorPhysicalExamination_DonorTransaction_DonorTransactionId",
                        column: x => x.DonorTransactionId,
                        principalTable: "DonorTransaction",
                        principalColumn: "DonorTransactionId");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DonorTestOrder",
                columns: table => new
                {
                    DonorTestOrderId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    DonorTransactionId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    TestCompleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatedById = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonorTestOrder", x => x.DonorTestOrderId);
                    table.ForeignKey(
                        name: "FK_DonorTestOrder_DonorTransaction_DonorTransactionId",
                        column: x => x.DonorTransactionId,
                        principalTable: "DonorTransaction",
                        principalColumn: "DonorTransactionId");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "InventorySource",
                columns: table => new
                {
                    InventorySourceId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    DonorTranctionId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    IsExternalSource = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    InstitutionId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventorySource", x => x.InventorySourceId);
                    table.ForeignKey(
                        name: "FK_InventorySource_DonorTranctionId",
                        column: x => x.DonorTranctionId,
                        principalTable: "DonorTransaction",
                        principalColumn: "DonorTransactionId");
                    table.ForeignKey(
                        name: "FK_InventorySource_Institution_InstitutionId",
                        column: x => x.InstitutionId,
                        principalTable: "Institution",
                        principalColumn: "InstitutionId");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TestOrder",
                columns: table => new
                {
                    TestOrderId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    PatientId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ReservationId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    TestCompleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    TestOrderNumber = table.Column<string>(type: "varchar(15)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UpdatedBy = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    PerformedBy = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    ReviewedBy = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    ValidatedBy = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    Remarks = table.Column<string>(type: "varchar(250)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestOrder", x => x.TestOrderId);
                    table.ForeignKey(
                        name: "FK_TestOrder_Patient_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patient",
                        principalColumn: "PatientId");
                    table.ForeignKey(
                        name: "FK_TestOrder_Reservation_ReservationId",
                        column: x => x.ReservationId,
                        principalTable: "Reservation",
                        principalColumn: "ReservationId");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DonorTestOrderTestType",
                columns: table => new
                {
                    DonorTestOrderTestTypeId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    DonorTestOrderId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    BloodTestTypeId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    IsReactive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Remarks = table.Column<string>(type: "varchar(75)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DateUpdated = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdatedById = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonorTestOrderTestType", x => x.DonorTestOrderTestTypeId);
                    table.ForeignKey(
                        name: "FK_DonorTestOrderTestType_BloodTestType_BloodTestTypeId",
                        column: x => x.BloodTestTypeId,
                        principalTable: "BloodTestType",
                        principalColumn: "BloodTestTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DonorTestOrderTestType_DonorTestOrder_DonorTestOrderId",
                        column: x => x.DonorTestOrderId,
                        principalTable: "DonorTestOrder",
                        principalColumn: "DonorTestOrderId");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "InventoryItem",
                columns: table => new
                {
                    InventoryItemId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    InventorySourceId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    BloodComponentId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    UnitSerialNumber = table.Column<string>(type: "varchar(30)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BloodType = table.Column<string>(type: "varchar(5)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BloodRh = table.Column<string>(type: "varchar(15)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Volume = table.Column<double>(type: "double", nullable: false),
                    UnitMeasure = table.Column<string>(type: "varchar(5)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CollectionDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    NotifyBeforeExpireOn = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Status = table.Column<string>(type: "varchar(15)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DateAdded = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    AddedById = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryItem", x => x.InventoryItemId);
                    table.ForeignKey(
                        name: "FK_InventoryItem_BloodComponent_BloodComponentId",
                        column: x => x.BloodComponentId,
                        principalTable: "BloodComponent",
                        principalColumn: "BloodComponentId");
                    table.ForeignKey(
                        name: "FK_InventoryItem_InventorySource_InventorySourceId",
                        column: x => x.InventorySourceId,
                        principalTable: "InventorySource",
                        principalColumn: "InventorySourceId");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "BloodScreeningTestOrder",
                columns: table => new
                {
                    BloodScreeningTestOrderId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    TestOrderId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Result = table.Column<string>(type: "varchar(15)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DateUpdated = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BloodScreeningTestOrder", x => x.BloodScreeningTestOrderId);
                    table.ForeignKey(
                        name: "FK_BloodScreeningTestOrder_TestOrder_TestOrderId",
                        column: x => x.TestOrderId,
                        principalTable: "TestOrder",
                        principalColumn: "TestOrderId");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "BloodTypingTestOrder",
                columns: table => new
                {
                    BloodTypingTestOrderId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    TestOrderId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    FTAntiA = table.Column<string>(type: "varchar(10)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FTAntiB = table.Column<string>(type: "varchar(10)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FTAntiAB = table.Column<string>(type: "varchar(10)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FTAntiD = table.Column<string>(type: "varchar(10)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FTAntiD2 = table.Column<string>(type: "varchar(10)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RTACells = table.Column<string>(type: "varchar(10)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RTBCells = table.Column<string>(type: "varchar(10)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Control = table.Column<string>(type: "varchar(10)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BloodType = table.Column<string>(type: "varchar(4)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BloodRh = table.Column<string>(type: "varchar(15)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DateUpdated = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BloodTypingTestOrder", x => x.BloodTypingTestOrderId);
                    table.ForeignKey(
                        name: "FK_BloodTypingTestOrder_TestOrder_TestOrderId",
                        column: x => x.TestOrderId,
                        principalTable: "TestOrder",
                        principalColumn: "TestOrderId");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CoombsTestOrder",
                columns: table => new
                {
                    CoombsTestOrderId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    TestOrderId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    DATResult = table.Column<string>(type: "varchar(15)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IATResult = table.Column<string>(type: "varchar(15)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DateUpdated = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoombsTestOrder", x => x.CoombsTestOrderId);
                    table.ForeignKey(
                        name: "FK_CoombsTestOrder_TestOrder_TestOrderId",
                        column: x => x.TestOrderId,
                        principalTable: "TestOrder",
                        principalColumn: "TestOrderId");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CrossMatchTestOrder",
                columns: table => new
                {
                    CrossMatchTestOrderId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    DonorTransactionId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    TestOrderId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    BloodComponentId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    DonorUnitSerialNumber = table.Column<string>(type: "varchar(30)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CrossMatchType = table.Column<string>(type: "varchar(20)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Result = table.Column<string>(type: "varchar(15)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Source = table.Column<string>(type: "varchar(50)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LISS_AGH = table.Column<string>(type: "varchar(5)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CollectionDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CrossMatchTestOrder", x => x.CrossMatchTestOrderId);
                    table.ForeignKey(
                        name: "FK_CrossMatchTestOrder_BloodComponent_BloodComponentId",
                        column: x => x.BloodComponentId,
                        principalTable: "BloodComponent",
                        principalColumn: "BloodComponentId");
                    table.ForeignKey(
                        name: "FK_CrossMatchTestOrder_DonorTransaction_DonorTransactionId",
                        column: x => x.DonorTransactionId,
                        principalTable: "DonorTransaction",
                        principalColumn: "DonorTransactionId");
                    table.ForeignKey(
                        name: "FK_CrossMatchTestOrder_TestOrder_TestOrderId",
                        column: x => x.TestOrderId,
                        principalTable: "TestOrder",
                        principalColumn: "TestOrderId");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ReservationItem",
                columns: table => new
                {
                    ReservationItemId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ReservationId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    BloodComponentId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    InventoryItemId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    DonorTransactionId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    DonorUnitSerialNumber = table.Column<string>(type: "varchar(30)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Volume = table.Column<double>(type: "double", nullable: false),
                    OtherNotes = table.Column<string>(type: "varchar(200)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservationItem", x => x.ReservationItemId);
                    table.ForeignKey(
                        name: "FK_ReservationItem_BloodComponent_BloodComponentId",
                        column: x => x.BloodComponentId,
                        principalTable: "BloodComponent",
                        principalColumn: "BloodComponentId");
                    table.ForeignKey(
                        name: "FK_ReservationItem_DonorTransaction_DonorTransactionId",
                        column: x => x.DonorTransactionId,
                        principalTable: "DonorTransaction",
                        principalColumn: "DonorTransactionId");
                    table.ForeignKey(
                        name: "FK_ReservationItem_InventoryItem_InventoryItemId",
                        column: x => x.InventoryItemId,
                        principalTable: "InventoryItem",
                        principalColumn: "InventoryItemId");
                    table.ForeignKey(
                        name: "FK_ReservationItem_Reservation_ReservationId",
                        column: x => x.ReservationId,
                        principalTable: "Reservation",
                        principalColumn: "ReservationId");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ReservationChecklist",
                columns: table => new
                {
                    ReservationChecklistId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    BloodComponentChecklistId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ReservationItemId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservationChecklist", x => x.ReservationChecklistId);
                    table.ForeignKey(
                        name: "FK_ReservationChecklist_BloodComponentChecklist_BloodComponentC~",
                        column: x => x.BloodComponentChecklistId,
                        principalTable: "BloodComponentChecklist",
                        principalColumn: "BloodComponentChecklistId");
                    table.ForeignKey(
                        name: "FK_ReservationChecklist_ReservationItem_ReservationItemId",
                        column: x => x.ReservationItemId,
                        principalTable: "ReservationItem",
                        principalColumn: "ReservationItemId");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Transfusion",
                columns: table => new
                {
                    TransfusionId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ReservationId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ReservationItemId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    TransfusionStarted = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    TransfusionEnded = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    MedicationGiven = table.Column<string>(type: "varchar(100)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    HookedBy = table.Column<string>(type: "varchar(100)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RemovedBy = table.Column<string>(type: "varchar(100)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TransfusionStatus = table.Column<string>(type: "varchar(15)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TransfusionNotes = table.Column<string>(type: "varchar(250)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transfusion", x => x.TransfusionId);
                    table.ForeignKey(
                        name: "FK_Transfusion_Reservation_ReservationId",
                        column: x => x.ReservationId,
                        principalTable: "Reservation",
                        principalColumn: "ReservationId");
                    table.ForeignKey(
                        name: "FK_Transfusion_ReservationItem_ReservationItemId",
                        column: x => x.ReservationItemId,
                        principalTable: "ReservationItem",
                        principalColumn: "ReservationItemId");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TransfusionVitalSign",
                columns: table => new
                {
                    VitalSignId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    TransfusionId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    VitalSignType = table.Column<string>(type: "varchar(20)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Temperature = table.Column<double>(type: "double", nullable: false),
                    BloodPressure = table.Column<string>(type: "varchar(10)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RespiratoryRate = table.Column<double>(type: "double", nullable: false),
                    PulseRate = table.Column<double>(type: "double", nullable: false),
                    Remarks = table.Column<string>(type: "varchar(250)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransfusionVitalSign", x => x.VitalSignId);
                    table.ForeignKey(
                        name: "FK_TransfusionVitalSign_Transfusion_TransfusionId",
                        column: x => x.TransfusionId,
                        principalTable: "Transfusion",
                        principalColumn: "TransfusionId");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_BloodComponentChecklist_BloodComponentId",
                table: "BloodComponentChecklist",
                column: "BloodComponentId");

            migrationBuilder.CreateIndex(
                name: "IX_BloodScreeningTestOrder_TestOrderId",
                table: "BloodScreeningTestOrder",
                column: "TestOrderId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BloodTypingTestOrder_TestOrderId",
                table: "BloodTypingTestOrder",
                column: "TestOrderId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CoombsTestOrder_TestOrderId",
                table: "CoombsTestOrder",
                column: "TestOrderId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CrossMatchTestOrder_BloodComponentId",
                table: "CrossMatchTestOrder",
                column: "BloodComponentId");

            migrationBuilder.CreateIndex(
                name: "IX_CrossMatchTestOrder_DonorTransactionId",
                table: "CrossMatchTestOrder",
                column: "DonorTransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_CrossMatchTestOrder_TestOrderId",
                table: "CrossMatchTestOrder",
                column: "TestOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_DonorBloodCollection_DonorTransactionId",
                table: "DonorBloodCollection",
                column: "DonorTransactionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DonorBloodCollection_FacilitatedById",
                table: "DonorBloodCollection",
                column: "FacilitatedById");

            migrationBuilder.CreateIndex(
                name: "IX_DonorDeferral_DonorTransactionId",
                table: "DonorDeferral",
                column: "DonorTransactionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DonorInitialScreening_DonorTransactionId",
                table: "DonorInitialScreening",
                column: "DonorTransactionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DonorMedicalHistory_DonorRegistrationId",
                table: "DonorMedicalHistory",
                column: "DonorRegistrationId");

            migrationBuilder.CreateIndex(
                name: "IX_DonorMedicalHistory_MedicalQuestionnaireId",
                table: "DonorMedicalHistory",
                column: "MedicalQuestionnaireId");

            migrationBuilder.CreateIndex(
                name: "IX_DonorPhysicalExamination_DonorTransactionId",
                table: "DonorPhysicalExamination",
                column: "DonorTransactionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DonorRecentDonation_DonorId",
                table: "DonorRecentDonation",
                column: "DonorId");

            migrationBuilder.CreateIndex(
                name: "IX_DonorTestOrder_DonorTransactionId",
                table: "DonorTestOrder",
                column: "DonorTransactionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DonorTestOrderTestType_BloodTestTypeId",
                table: "DonorTestOrderTestType",
                column: "BloodTestTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_DonorTestOrderTestType_DonorTestOrderId",
                table: "DonorTestOrderTestType",
                column: "DonorTestOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_DonorTransaction_DonorId",
                table: "DonorTransaction",
                column: "DonorId");

            migrationBuilder.CreateIndex(
                name: "IX_DonorTransaction_DonorRegistrationId",
                table: "DonorTransaction",
                column: "DonorRegistrationId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryItem_BloodComponentId",
                table: "InventoryItem",
                column: "BloodComponentId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryItem_InventorySourceId",
                table: "InventoryItem",
                column: "InventorySourceId");

            migrationBuilder.CreateIndex(
                name: "IX_InventorySource_DonorTranctionId",
                table: "InventorySource",
                column: "DonorTranctionId");

            migrationBuilder.CreateIndex(
                name: "IX_InventorySource_InstitutionId",
                table: "InventorySource",
                column: "InstitutionId");

            migrationBuilder.CreateIndex(
                name: "IX_LookupOption_LookupId",
                table: "LookupOption",
                column: "LookupId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_PatientId",
                table: "Reservation",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationChecklist_BloodComponentChecklistId",
                table: "ReservationChecklist",
                column: "BloodComponentChecklistId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationChecklist_ReservationItemId",
                table: "ReservationChecklist",
                column: "ReservationItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationItem_BloodComponentId",
                table: "ReservationItem",
                column: "BloodComponentId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationItem_DonorTransactionId",
                table: "ReservationItem",
                column: "DonorTransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationItem_InventoryItemId",
                table: "ReservationItem",
                column: "InventoryItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationItem_ReservationId",
                table: "ReservationItem",
                column: "ReservationId");

            migrationBuilder.CreateIndex(
                name: "IX_TestOrder_PatientId",
                table: "TestOrder",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_TestOrder_ReservationId",
                table: "TestOrder",
                column: "ReservationId");

            migrationBuilder.CreateIndex(
                name: "IX_Transfusion_ReservationId",
                table: "Transfusion",
                column: "ReservationId");

            migrationBuilder.CreateIndex(
                name: "IX_Transfusion_ReservationItemId",
                table: "Transfusion",
                column: "ReservationItemId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TransfusionVitalSign_TransfusionId",
                table: "TransfusionVitalSign",
                column: "TransfusionId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_RoleId",
                table: "UserRole",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_UserAccountId",
                table: "UserRole",
                column: "UserAccountId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationSetting");

            migrationBuilder.DropTable(
                name: "BloodScreeningTestOrder");

            migrationBuilder.DropTable(
                name: "BloodTypingTestOrder");

            migrationBuilder.DropTable(
                name: "CoombsTestOrder");

            migrationBuilder.DropTable(
                name: "CrossMatchTestOrder");

            migrationBuilder.DropTable(
                name: "DonorBloodCollection");

            migrationBuilder.DropTable(
                name: "DonorDeferral");

            migrationBuilder.DropTable(
                name: "DonorInitialScreening");

            migrationBuilder.DropTable(
                name: "DonorMedicalHistory");

            migrationBuilder.DropTable(
                name: "DonorPhysicalExamination");

            migrationBuilder.DropTable(
                name: "DonorRecentDonation");

            migrationBuilder.DropTable(
                name: "DonorTestOrderTestType");

            migrationBuilder.DropTable(
                name: "LookupOption");

            migrationBuilder.DropTable(
                name: "ReservationChecklist");

            migrationBuilder.DropTable(
                name: "Signatory");

            migrationBuilder.DropTable(
                name: "TestOrderType");

            migrationBuilder.DropTable(
                name: "TransfusionVitalSign");

            migrationBuilder.DropTable(
                name: "UserRole");

            migrationBuilder.DropTable(
                name: "TestOrder");

            migrationBuilder.DropTable(
                name: "MedicalQuestionnaire");

            migrationBuilder.DropTable(
                name: "BloodTestType");

            migrationBuilder.DropTable(
                name: "DonorTestOrder");

            migrationBuilder.DropTable(
                name: "Lookup");

            migrationBuilder.DropTable(
                name: "BloodComponentChecklist");

            migrationBuilder.DropTable(
                name: "Transfusion");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "UserAccount");

            migrationBuilder.DropTable(
                name: "ReservationItem");

            migrationBuilder.DropTable(
                name: "InventoryItem");

            migrationBuilder.DropTable(
                name: "Reservation");

            migrationBuilder.DropTable(
                name: "BloodComponent");

            migrationBuilder.DropTable(
                name: "InventorySource");

            migrationBuilder.DropTable(
                name: "Patient");

            migrationBuilder.DropTable(
                name: "DonorTransaction");

            migrationBuilder.DropTable(
                name: "Institution");

            migrationBuilder.DropTable(
                name: "Donor");

            migrationBuilder.DropTable(
                name: "DonorRegistration");
        }
    }
}
