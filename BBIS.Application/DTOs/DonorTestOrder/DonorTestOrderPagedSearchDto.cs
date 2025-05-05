using BBIS.Application.DTOs.Common;

namespace BBIS.Application.DTOs.DonorTestOrder
{
    public class DonorTestOrderPagedSearchDto : PagedSearchDto
    {
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
    }
}
