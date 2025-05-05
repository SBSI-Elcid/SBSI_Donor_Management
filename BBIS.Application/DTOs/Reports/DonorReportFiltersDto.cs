using BBIS.Application.DTOs.Common;

namespace BBIS.Application.DTOs.Reports
{
    public class DonorReportFiltersDto
    {
        public List<string> BloodTypes { get; set; }
        public List<string> Statuses { get; set; }
        public DateTime? DonationDateFrom { get; set; }
        public DateTime? DonationDateTo { get; set; }
    }
}
