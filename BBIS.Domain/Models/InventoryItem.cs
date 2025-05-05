namespace BBIS.Domain.Models
{
    public class InventoryItem
    {
        public Guid InventoryItemId { get; set; }
        public Guid InventorySourceId { get; set; }
        public Guid BloodComponentId { get; set; }
        public string UnitSerialNumber { get; set; }
        public string BloodType { get; set; }
        public string BloodRh { get; set; }
        public double Volume { get; set; }
        public string UnitMeasure { get; set; }
        public DateTime CollectionDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public DateTime NotifyBeforeExpireOn { get; set; }
        public string Status { get; set; }
        public DateTime DateAdded { get; set; }
        public Guid AddedById { get; set; }

        public virtual InventorySource InventorySource { get; set; }
        public virtual BloodComponent BloodComponent { get; set; }
        public virtual ICollection<ReservationItem> ReservationItems { get; set; }
    }
}
