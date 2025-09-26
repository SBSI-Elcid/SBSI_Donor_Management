using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BBIS.Database.Migrations
{
    public partial class DefaultValuesForAdminRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "UserRoleScreeningAccess",
                columns: new[] { "UserRoleScreeningAccessId", "CreatedAt", "RoleId", "ScreeningStatus", "ScreeningTabName" },
                values: new object[,]
                {
                    { new Guid("21c00507-acc4-44a5-b7e2-79e13e270de7"), new DateTime(2025, 9, 26, 8, 51, 43, 313, DateTimeKind.Local).AddTicks(3056), new Guid("489abc53-af8a-4914-bd21-7d99c26e2e33"), "ForPostDonationCare", "PostDonationCare" },
                    { new Guid("30b9b419-ceb8-4732-aed7-b02c5e24f149"), new DateTime(2025, 9, 26, 8, 51, 43, 313, DateTimeKind.Local).AddTicks(3016), new Guid("489abc53-af8a-4914-bd21-7d99c26e2e33"), "ForPhysicalExamination", "PhysicalExam" },
                    { new Guid("4cf3ce3e-6f24-480c-97a1-539a32b45076"), new DateTime(2025, 9, 26, 8, 51, 43, 313, DateTimeKind.Local).AddTicks(3049), new Guid("489abc53-af8a-4914-bd21-7d99c26e2e33"), "ForBloodCollection", "BloodCollection" },
                    { new Guid("7d0629f9-0741-4f17-9d5a-c728bf016467"), new DateTime(2025, 9, 26, 8, 51, 43, 313, DateTimeKind.Local).AddTicks(2976), new Guid("489abc53-af8a-4914-bd21-7d99c26e2e33"), "ForDonorInformation", "DonorInformation" },
                    { new Guid("80ad4294-fc61-436b-b507-d1d97b93ebe6"), new DateTime(2025, 9, 26, 8, 51, 43, 313, DateTimeKind.Local).AddTicks(3043), new Guid("489abc53-af8a-4914-bd21-7d99c26e2e33"), "ForBloodIssuance", "IssuanceOfBloodBag" },
                    { new Guid("cb2e08a7-68ba-46a7-bf82-4a513b46e407"), new DateTime(2025, 9, 26, 8, 51, 43, 313, DateTimeKind.Local).AddTicks(3023), new Guid("489abc53-af8a-4914-bd21-7d99c26e2e33"), "ForCounseling", "Counseling" },
                    { new Guid("dc94927e-0424-49c8-a684-bd31c8271718"), new DateTime(2025, 9, 26, 8, 51, 43, 313, DateTimeKind.Local).AddTicks(3036), new Guid("489abc53-af8a-4914-bd21-7d99c26e2e33"), "ForMethodBloodCollection", "MethodBloodCollection" },
                    { new Guid("ddbea6bb-e8ea-4c3a-95ed-f45ccd11023c"), new DateTime(2025, 9, 26, 8, 51, 43, 313, DateTimeKind.Local).AddTicks(3029), new Guid("489abc53-af8a-4914-bd21-7d99c26e2e33"), "ForConsent", "ConsentForm" },
                    { new Guid("e52c36fc-d441-43b0-866e-f996340b2220"), new DateTime(2025, 9, 26, 8, 51, 43, 313, DateTimeKind.Local).AddTicks(3069), new Guid("489abc53-af8a-4914-bd21-7d99c26e2e33"), "Deferred", "Deferred" },
                    { new Guid("f2541bd0-f3d1-46e5-845f-c135a2efe4a4"), new DateTime(2025, 9, 26, 8, 51, 43, 313, DateTimeKind.Local).AddTicks(2948), new Guid("489abc53-af8a-4914-bd21-7d99c26e2e33"), "ForVitalSigns", "DonorVitalSigns" },
                    { new Guid("ff6699d3-d066-4f77-8fef-3ea8a8af94fe"), new DateTime(2025, 9, 26, 8, 51, 43, 313, DateTimeKind.Local).AddTicks(3009), new Guid("489abc53-af8a-4914-bd21-7d99c26e2e33"), "ForInitialScreening", "InitialScreening" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserRoleScreeningAccess",
                keyColumn: "UserRoleScreeningAccessId",
                keyValue: new Guid("21c00507-acc4-44a5-b7e2-79e13e270de7"));

            migrationBuilder.DeleteData(
                table: "UserRoleScreeningAccess",
                keyColumn: "UserRoleScreeningAccessId",
                keyValue: new Guid("30b9b419-ceb8-4732-aed7-b02c5e24f149"));

            migrationBuilder.DeleteData(
                table: "UserRoleScreeningAccess",
                keyColumn: "UserRoleScreeningAccessId",
                keyValue: new Guid("4cf3ce3e-6f24-480c-97a1-539a32b45076"));

            migrationBuilder.DeleteData(
                table: "UserRoleScreeningAccess",
                keyColumn: "UserRoleScreeningAccessId",
                keyValue: new Guid("7d0629f9-0741-4f17-9d5a-c728bf016467"));

            migrationBuilder.DeleteData(
                table: "UserRoleScreeningAccess",
                keyColumn: "UserRoleScreeningAccessId",
                keyValue: new Guid("80ad4294-fc61-436b-b507-d1d97b93ebe6"));

            migrationBuilder.DeleteData(
                table: "UserRoleScreeningAccess",
                keyColumn: "UserRoleScreeningAccessId",
                keyValue: new Guid("cb2e08a7-68ba-46a7-bf82-4a513b46e407"));

            migrationBuilder.DeleteData(
                table: "UserRoleScreeningAccess",
                keyColumn: "UserRoleScreeningAccessId",
                keyValue: new Guid("dc94927e-0424-49c8-a684-bd31c8271718"));

            migrationBuilder.DeleteData(
                table: "UserRoleScreeningAccess",
                keyColumn: "UserRoleScreeningAccessId",
                keyValue: new Guid("ddbea6bb-e8ea-4c3a-95ed-f45ccd11023c"));

            migrationBuilder.DeleteData(
                table: "UserRoleScreeningAccess",
                keyColumn: "UserRoleScreeningAccessId",
                keyValue: new Guid("e52c36fc-d441-43b0-866e-f996340b2220"));

            migrationBuilder.DeleteData(
                table: "UserRoleScreeningAccess",
                keyColumn: "UserRoleScreeningAccessId",
                keyValue: new Guid("f2541bd0-f3d1-46e5-845f-c135a2efe4a4"));

            migrationBuilder.DeleteData(
                table: "UserRoleScreeningAccess",
                keyColumn: "UserRoleScreeningAccessId",
                keyValue: new Guid("ff6699d3-d066-4f77-8fef-3ea8a8af94fe"));
        }
    }
}
