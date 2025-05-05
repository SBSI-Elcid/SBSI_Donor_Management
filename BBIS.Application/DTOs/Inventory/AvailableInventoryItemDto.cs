namespace BBIS.Application.DTOs.Inventory
{
    public class AvailableInventoryItemDto
    {
        public Guid BloodComponentId { get; set; }
        public List<InventoryUnitOptionsDto> AvailableOptions { get; set; }
    }

    public class InventoryUnitOptionsDto
    {
        public Guid InventoryItemId { get; set; }
        public string ItemText { get; set; }
    }
}
