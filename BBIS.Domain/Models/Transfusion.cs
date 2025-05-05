namespace BBIS.Domain.Models
{
    public class Transfusion
    {
        public Guid TransfusionId { get; set; }
        public Guid ReservationId { get; set; }
        public Guid ReservationItemId { get; set; }
        public DateTime TransfusionStarted { get; set; }
        public DateTime TransfusionEnded { get; set; }
        public string MedicationGiven { get; set; }
        public string HookedBy { get; set; }
        public string RemovedBy { get; set; }
        public string TransfusionStatus { get; set; }
        public string TransfusionNotes { get; set; }

        public virtual Reservation Reservation { get; set; }
        public virtual ReservationItem ReservationItem { get; set; }

        public virtual ICollection<TransfusionVitalSign> TransfusionVitalSigns { get; set; }
    }
}

