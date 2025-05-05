namespace BBIS.Application.DTOs.Requisition
{
    public class ReservationListingDto
    {
        public Guid ReservationId { get; set; }
        public Guid PatientId { get; set; }
        public string PatientName { get; set; }

        public int Age { get; set; }
        public string BloodType { get; set; }
        public List<string> Components { get; set; }
        public List<RequestedTestOrderSummaryDto> TestOrderSummary { get; set; }
        public DateTime RequestedDate { get; set; }
        public string Status { get; set; }
    }

    public class RequestedTestOrderSummaryDto
    {
        public Guid TestOrderId { get; set; }
        public List<string> TestOrders { get; set; }
        public List<string> CrossMatchTestOrders { get; set; }
        public bool TestCompleted { get; set; }
    }
}
