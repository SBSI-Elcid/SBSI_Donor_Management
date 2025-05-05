using BBIS.Application.DTOs.Inventory;
using BBIS.Application.DTOs.Reports;

namespace BBIS.Application.Contracts
{
    public interface IInventoryService
    {
        Task<List<InventoryItemDto>> PrepareInventoryItems(Guid transactionId);
        Task AddToInventory(AddInventoryDto dto, Guid userId);
        Task<List<InventoryGroupDto>> GetInventoryStatus();

        Task<List<InventoryListDto>> GetInventoryItems(InventoryFilterDto filter);
        Task<ExportReportDto<InventoryListDto>> GetInventoryItemsForExport(InventoryFilterDto filter);
        Task<List<AvailableInventoryItemDto>> GetAvailableInventoryUnits(string bloodType, string bloodRh);
    }
}
