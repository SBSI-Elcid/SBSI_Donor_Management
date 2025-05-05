namespace BBIS.Application.DTOs.Inventory
{
    public class InventoryGroupDto
    {
        public string BloodType { get; set; }
        public string BloodRh { get; set; }
        public double TotalNumberOfUnits { get; set; }
        public int TotalExpiredCount { get; set; }

        public List<InventoryViewDto> InventoryItems { get; set; }
    }
}
