
namespace BBIS.Common
{
    public class ApplicationRoles
    {
        /// --- Roles --- ///
        public const string AdminRole = "Admin";
        public const string DonorAdminRole = "DonorAdmin";
        public const string InitialScreenerRole = "InitialScreener";
        public const string PhysicalExamScreenerRole = "PhysicalExamScreener";
        public const string BloodCollectorRole = "BloodCollector";
        public const string InventoryUserRole = "InventoryUser";
        public const string RequisitionUserRole = "RequisitionUser";
        public const string MedTechRole = "MedTech";

        // New Roles
        public const string VitalSignsRole = "VitalSigns";
        public const string CounselorRole = "Counselor";
        public const string MethodBloodCollectionRole = "MethodBloodCollection";
        public const string IssuanceOfBloodBagRole = "IssuanceOfBloodBag";
        public const string PostDonationCareRole = "PostDonationCare";


        /// --- Policies --- ///
        public const string AdminPolicy = nameof(AdminPolicy);
        public const string TestOrderPolicy = nameof(TestOrderPolicy);
        public const string DonorAdminPolicy = nameof(DonorAdminPolicy);
        public const string DonorScreeningPolicy = nameof(DonorScreeningPolicy);
        public const string InventoryPolicy = nameof(InventoryPolicy);
        public const string RequisitionPolicy = nameof(RequisitionPolicy);
        public const string InitialScreeningPolicy = nameof(InitialScreeningPolicy);
        public const string PhysicalExaminationPolicy = nameof(PhysicalExaminationPolicy);
        public const string BloodCollectionPolicy = nameof(BloodCollectionPolicy);

        // New Policies (optional, if you want policy-based access control for these)
        public const string VitalSignsPolicy = nameof(VitalSignsPolicy);
        public const string CounselorPolicy = nameof(CounselorPolicy);
        public const string MethodBloodCollectionPolicy = nameof(MethodBloodCollectionPolicy);
        public const string IssuanceOfBloodBagPolicy = nameof(IssuanceOfBloodBagPolicy);
        public const string PostDonationCarePolicy = nameof(PostDonationCarePolicy);
    }
      
}
