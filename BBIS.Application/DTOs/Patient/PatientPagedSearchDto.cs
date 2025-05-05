using BBIS.Application.DTOs.Common;

namespace BBIS.Application.DTOs.Patient
{
    public class PatientPagedSearchDto: PagedSearchDto
    {
        public string BloodType { get; set; }
        public string BloodRh { get; set; }
    }
}
