namespace BBIS.Application.DTOs.Inventory
{
    public class InventoryFilterDto
    {
        public List<string> BloodTypes { get; set; }
        public List<string> Statuses { get; set; }
        public string DateFilterType { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
    }
}
