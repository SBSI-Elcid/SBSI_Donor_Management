namespace BBIS.Domain.Models
{
    public class BloodComponent
    {
        public Guid BloodComponentId { get; set; }
        public string ComponentName { get; set; }
        public int ExpiryInDays { get; set; }
        public int NotifyOnDaysBefore { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<InventoryItem> InventoryItems { get; set; }
        public virtual ICollection<ReservationItem> ReservationItems { get; set; }
        public virtual ICollection<CrossMatchTestOrder> CrossMatchTestOrders { get; set; }
        public virtual ICollection<BloodComponentChecklist> BloodComponentChecklists { get; set; }
    }
}
