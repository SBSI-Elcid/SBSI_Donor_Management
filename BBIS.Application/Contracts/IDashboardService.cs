using BBIS.Application.DTOs.Dashboard;
using BBIS.Application.DTOs.Schedule;

namespace BBIS.Application.Contracts
{
    public interface IDashboardService
    {
        Task<int> GetDonorCount();
        Task<List<ScheduleDto>> GetSchedules();
        Task<InventoryCountDto> GetInventoryItemsCount();
        Task<List<BloodTypeCountDto>> GetBloodTypeCount();
        Task<DonorCountDto> GetMonthlyDonorCount();
    }
}
