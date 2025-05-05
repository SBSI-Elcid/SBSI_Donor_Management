namespace BBIS.Application.DTOs.Inventory
{
    public class InventoryListDto
    {
        public Guid InventoryItemId { get; set; }
        public Guid BloodComponentId { get; set; }
        public string BloodType { get; set; }
        public string BloodRh { get; set; }
        public string ComponentName { get; set; }
        public string UnitSerialNumber { get; set; }
        public string NoOfUnit { get; set; } //combination of Volume & unit measure i.e 100mL
        public string CollectedBy { get; set; }
        public DateTime CollectionDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string Status { get; set; }
    }
}
