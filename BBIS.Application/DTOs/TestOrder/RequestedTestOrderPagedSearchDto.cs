using BBIS.Application.DTOs.Common;

namespace BBIS.Application.DTOs.TestOrder
{
    public class RequestedTestOrderPagedSearchDto : PagedSearchDto
    {
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
    }
}
