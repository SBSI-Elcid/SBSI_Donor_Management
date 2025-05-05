namespace BBIS.Application.DTOs.Inventory
{
    public class InventoryViewDto
    {
        public Guid InventoryItemId { get; set; }
        public string Component { get; set; }
        public string NoOfUnit { get; set; } //combination of Volume & unit measure i.e 100mL
        public string CollectionDate { get; set; } // formatted date
        public string ExpiryDate { get; set; } // formatted date
        public string Status { get; set; }
    }
}
