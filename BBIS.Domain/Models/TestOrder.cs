namespace BBIS.Domain.Models
{
    public class TestOrder
    {
        public Guid TestOrderId { get; set; }
        public Guid PatientId { get; set; }
        public Guid ReservationId { get; set; }
        public bool TestCompleted { get; set; }
        public string TestOrderNumber { get; set; }

        public Guid? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Guid? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public Guid? PerformedBy { get; set; }
        public Guid? ReviewedBy { get; set; }
        public Guid? ValidatedBy { get; set; }
        public Guid? PhatologistId { get; set; }

        public string Remarks { get; set; }

        public virtual Reservation Reservation { get; set; }
        public virtual Patient Patient { get; set; }

        public virtual ICollection<CrossMatchTestOrder> CrossMatchTestOrders { get; set; }
        public virtual BloodScreeningTestOrder BloodScreeningTestOrder { get; set; }
        public virtual CoombsTestOrder CoombsTestOrder { get; set; }
        public virtual BloodTypingTestOrder BloodTypingTestOrder { get; set; }
    }
}
