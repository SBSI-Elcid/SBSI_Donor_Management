namespace BBIS.Application.DTOs.Inventory
{
    public class InventoryItemDto
    {
        public Guid BloodComponentId { get; set; }
        public string BloodComponentName { get; set; }
        public string UnitSerialNumber { get; set; }
        public string BloodType { get; set; }
        public string BloodRh { get; set; }
        public double Volume { get; set; }
        public string UnitMeasure { get; set; }
        public DateTime CollectionDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public DateTime NotifyBeforeExpireOn { get; set; }
    }
}
