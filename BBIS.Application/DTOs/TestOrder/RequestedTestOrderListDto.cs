namespace BBIS.Application.DTOs.TestOrder
{
    public class RequestedTestOrderListDto
    {
        public Guid TestOrderId { get; set; }
        public Guid ReservationId { get; set; }

        public string TestOrderNumber { get; set; }
        public string PatientName { get; set; }
        public string Remarks { get; set; }
        public List<RequestedTestOrderDto> TestOrdersResult { get; set; }
        public List<CrossMatchTestOrderViewDto> CrossMatchTestOrdersResult { get; set; }

        public bool TestCompleted { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
