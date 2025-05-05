using BBIS.Domain.Models;

namespace BBIS.Application.DTOs.Inventory
{
    public class AddInventoryDto
    {
        public Guid? DonorTranctionId { get; set; }
        public Guid? InstitutionId { get; set; }

        public List<InventoryItemDto> InventoryItems { get; set; }
    }
}
