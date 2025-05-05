using BBIS.Application.DTOs.Dashboard;

namespace BBIS.Application.Contracts
{
    public interface IDashboardService
    {
        Task<int> GetDonorCount();
        Task<InventoryCountDto> GetInventoryItemsCount();
        Task<List<BloodTypeCountDto>> GetBloodTypeCount();
        Task<DonorCountDto> GetMonthlyDonorCount();
    }
}
