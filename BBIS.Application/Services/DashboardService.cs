using BBIS.Application.Contracts;
using BBIS.Application.DTOs.Dashboard;
using BBIS.Common.Enums;
using BBIS.Common.Extensions;
using BBIS.Database;
using Microsoft.EntityFrameworkCore;

namespace BBIS.Application.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly BBDbContext dbContext;

        private DateTime startYear;
        private DateTime endYear;

        public DashboardService(BBDbContext dbContext)
        {
            this.dbContext = dbContext;

            DateTime now = DateTime.UtcNow;
            startYear = new DateTime(now.Year, 1, 1).ToUniversalTime();
            endYear = new DateTime(now.Year, 12, 31).ToUniversalTime().AddDays(1).AddTicks(-1);
        }

        public async Task<int> GetDonorCount()
        {
            return await dbContext.DonorTransactions.AsNoTracking().CountAsync(x => x.DateOfDonation.HasValue);
        }

        public async Task<InventoryCountDto> GetInventoryItemsCount()
        {
            var inventoryCount = new InventoryCountDto { TotalItems = 0, TotalNearlyExpiredItems = 0, TotalExpiredItems = 0 };

            var items = await dbContext.InventoryItems
                .Select(x => new
                {
                    InventoryItemId = x.InventoryItemId,
                    Status = x.Status
                }).AsNoTracking().ToListAsync();
            
            if (items.Any())
            {
                var totalAvailableItems = items.Where(x => x.Status != InventoryStatus.Expired && x.Status != InventoryStatus.Acquired).Count();
                var toExpiredItems = items.Where(x => x.Status == InventoryStatus.NearExpiry).Count();
                var expiredItems = items.Where(x => x.Status == InventoryStatus.Expired).Count();

                inventoryCount.TotalItems = totalAvailableItems;
                inventoryCount.TotalNearlyExpiredItems = toExpiredItems;
                inventoryCount.TotalExpiredItems = expiredItems;
            }

            return inventoryCount;
        }

        public async Task<List<BloodTypeCountDto>> GetBloodTypeCount()
        {
            var bloodTypeCountDto = new List<BloodTypeCountDto>();

            var items = await dbContext.InventoryItems
                .Where(x => x.CollectionDate.Date >= startYear && x.CollectionDate.Date <= endYear)
                .Select(x => new
                {
                    x.InventorySourceId,
                    x.BloodType
                }).AsNoTracking().ToListAsync();

            if (items.Any())
            {
                var groupedByType = items.GroupBy(x => x.BloodType);
                foreach(var group in groupedByType)
                {
                    var bloodTypeCount = new BloodTypeCountDto();
                    bloodTypeCount.BloodType = group.Key;
                    bloodTypeCount.Count = group.DistinctBy(x => x.InventorySourceId).Count();

                    bloodTypeCountDto.Add(bloodTypeCount);
                }
            }

            return bloodTypeCountDto;
        }
        
        public async Task<DonorCountDto> GetMonthlyDonorCount()
        {
            var donorCount = new DonorCountDto { Donors = new List<MonthlyDonorCountDto>(), DeferredDonors = new List<MonthlyDonorCountDto>() };
            var months = new List<string> { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };

            var successfulDonors = new List<string>() { DonorStatus.Success, DonorStatus.Inventory };
            var donors = await dbContext.DonorTransactions
                .Where(x => x.DateOfDonation.HasValue && x.DateOfDonation.Value >= startYear && x.DateOfDonation.Value <= endYear && successfulDonors.Contains(x.DonorStatus))
                .Select(x => new
                {
                    Month = x.DateOfDonation.Value.GetPHDateTimeFromUtc().ToString("MMMM")
                })
                .AsNoTracking()
                .ToListAsync();

            if (donors != null)
            {
                var monthlyDonors = new List<MonthlyDonorCountDto>();

                foreach (var month in months)
                {                  
                    var donorsByMonth = donors.Where(x => x.Month == month).ToList();

                    var monthlyCount = new MonthlyDonorCountDto();
                    monthlyCount.Month = month;
                    monthlyCount.Count = donorsByMonth.Any() ? donorsByMonth.Count() : 0;

                    monthlyDonors.Add(monthlyCount);
                }

                donorCount.Donors = monthlyDonors;
            }

            var deferred = await dbContext.DonorTransactions
                .Where(x => x.DateOfDonation.HasValue && x.DateOfDonation.Value >= startYear && x.DateOfDonation.Value <= endYear && x.DonorStatus == DonorStatus.Deferred)
                .Select(x => new
                {
                    Month = x.DateOfDonation.Value.GetPHDateTimeFromUtc().ToString("MMMM")
                })
                .AsNoTracking()
                .ToListAsync();

            if (deferred != null)
            {
                var monthlyDonors = new List<MonthlyDonorCountDto>();

                foreach (var month in months)
                {
                    var deferredByMonth = deferred.Where(x => x.Month == month).ToList();

                    var monthlyCount = new MonthlyDonorCountDto();
                    monthlyCount.Month = month;
                    monthlyCount.Count = deferredByMonth.Any() ? deferredByMonth.Count() : 0;

                    monthlyDonors.Add(monthlyCount);
                }

                donorCount.DeferredDonors = monthlyDonors;
            }

            return donorCount;
        }
    }
}
