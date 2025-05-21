using BBIS.Common.Enums;

namespace BBIS.Common.Extensions
{
    public static class StringExtensions
    {
        public static List<string> GetDonorStatusByRoles(this List<string> roles)
        {
            var statuses = new List<string>();

            // If Admin or Donor Admin, return all the status.
            if (roles.Contains(Roles.Admin) || roles.Contains(Roles.DonorAdmin))
            {
                return new List<string>
                {
                    DonorStatus.ForInitialScreening,
                    DonorStatus.ForPhysicalExamination,
                    DonorStatus.ForBloodCollection,
                    DonorStatus.ForLaboratoryTest,
                    DonorStatus.Success,
                    DonorStatus.ForCounseling,
                    DonorStatus.ForVitalSigns,
                    DonorStatus.ForConsent,
                    DonorStatus.ForMethodBloodCollection,
                    DonorStatus.Deferred,
                };
            }

            if (roles.Contains(Roles.InitialScreener))
            {
                statuses.Add(DonorStatus.ForInitialScreening);
                statuses.Add(DonorStatus.ForVitalSigns);
                statuses.Add(DonorStatus.ForCounseling);
                statuses.Add(DonorStatus.ForConsent);
            }

            if (roles.Contains(Roles.PhysicalExamScreener))
            {
                statuses.Add(DonorStatus.ForPhysicalExamination);
            }

            if (roles.Contains(Roles.BloodCollector))
            {
                statuses.Add(DonorStatus.ForMethodBloodCollection);
                statuses.Add(DonorStatus.ForBloodCollection);
            }

            return statuses;
        }

        public static int CalculateAge(this DateTime birthDate)
        {
            var now = DateTime.UtcNow;
            int age = now.Year - birthDate.Year;

            // For leap years we need this
            if (birthDate > now.AddYears(-age))
                age--;

            return age;
        }
    }
}
