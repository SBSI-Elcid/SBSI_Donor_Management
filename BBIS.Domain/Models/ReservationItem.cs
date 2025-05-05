namespace BBIS.Domain.Models
{
    public class ReservationItem
    {
        public Guid ReservationItemId { get; set; }
        public Guid ReservationId { get; set; }
        public Guid BloodComponentId { get; set; }
        public Guid InventoryItemId { get; set; }
        public Guid DonorTransactionId { get; set; }
        public string DonorUnitSerialNumber { get; set; }

        public double Volume { get; set; }
        public string OtherNotes { get; set; }
                            
        public virtual Reservation Reservation { get; set;}
        public virtual InventoryItem InventoryItem { get; set;}
        public virtual BloodComponent BloodComponent { get; set;}
        public virtual DonorTransaction DonorTransaction { get; set;}
        public virtual Transfusion Transfusion { get; set; }
      
        public virtual ICollection<ReservationChecklist> ReservationChecklists { get; set;}
    }
}
