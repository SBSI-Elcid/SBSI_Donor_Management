
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
    }
      
}
