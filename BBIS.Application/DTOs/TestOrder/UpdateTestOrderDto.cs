namespace BBIS.Application.DTOs.TestOrder
{
    public class UpdateTestOrderDto
    {
        public Guid TestOrderId { get; set; }
        public Guid PatientId { get; set; }
        public Guid ReservationId { get; set; }
        public bool TestCompleted { get; set; }
        public string Remarks { get; set; }
        public Guid PerformedBy { get; set; }
        public Guid ReviewedBy { get; set; }
        public Guid ValidatedBy { get; set; }
        public Guid PathologistId { get; set; }

        public CoombsTestOrderDto CoombsTestOrder { get; set; }
        public BloodScreeningTestOrderDto BloodScreeningTestOrder { get; set; }
        public BloodTypingTestOrderDto BloodTypingTestOrder { get; set; }

        public List<UpdateCrossMatchTestOrderDto> CrossMatchTestOrders { get; set; }
    }
}
