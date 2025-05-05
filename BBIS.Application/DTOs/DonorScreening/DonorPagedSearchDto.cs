using BBIS.Application.DTOs.Common;

namespace BBIS.Application.DTOs.DonorScreening
{
    public class DonorPagedSearchDto : PagedSearchDto
    {
        public string Name { get; set; }
        public string RegistrationNumber { get; set; }
        public string BloodType { get; set; }
        public string DonorStatus { get; set; }
    }
}
