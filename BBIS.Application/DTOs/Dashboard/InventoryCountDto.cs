
namespace BBIS.Application.DTOs.Dashboard
{
    public class InventoryCountDto
    {
        public int TotalItems { get; set; }
        public int TotalNearlyExpiredItems { get; set; }
        public int TotalExpiredItems { get; set; }
    }
}
