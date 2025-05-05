using BBIS.Application.DTOs.Common;

namespace BBIS.Application.DTOs.Requisition
{
    public class RequisitionPagedSearchDto : PagedSearchDto
    {
        public List<string> Statuses { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
    }
}
