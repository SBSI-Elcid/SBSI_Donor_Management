namespace BBIS.Domain.Models
{
    public class Reservation
    {
        public Guid ReservationId { get; set; }
        public Guid PatientId { get; set; }
        public string PatientType { get; set; }
        public string PriorityLevel { get; set; }
        public bool ForAdult { get; set; }
        public bool IsEmergency { get; set; }
        public string RoomNo { get; set; }
        public string Membership { get; set; }
        public string RequestedBy { get; set; }
        public DateTime RequestedDateTime { get; set; }
        public DateTime? PreviousTransfusionDate { get; set; }
        public int? PreviousNoOfUnitsTransfused { get; set; }
        public string Status { get; set; }

        public DateTime Expiration { get; set; }

        public Guid CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public Guid? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public virtual Patient Patient { get; set; }

        public virtual ICollection<ReservationItem> ReservationItems { get; set; }
        public virtual ICollection<TestOrder> TestOrders { get; set; }
        public virtual ICollection<Transfusion> Transfusions { get; set; }
    }
}
