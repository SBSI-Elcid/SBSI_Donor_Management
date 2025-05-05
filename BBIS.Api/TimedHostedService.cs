using BBIS.Common.Enums;
using BBIS.Common.Logging;
using BBIS.Database;
using BBIS.Domain.Contracts;

namespace BBIS.Api
{
    public class TimedHostedService : IHostedService, IDisposable
    {
        private readonly ILoggerManager _logger;
        private readonly IServiceScopeFactory _scopeFactory;

        private Timer _timer;

        public TimedHostedService(ILoggerManager loggerManager, IServiceScopeFactory scopeFactory)
        {
            _logger = loggerManager;
            _scopeFactory = scopeFactory;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromHours(24));
            return Task.CompletedTask;
        }

        private async void DoWork(object state)
        {
            try
            {
                await CancelExpiredReservation();
                await TagInventoryItemsStatus();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Timed Hosted Service encountered an error: {ex.Message}");
            }
        }

        private async Task CancelExpiredReservation()
        {
            using var scope = _scopeFactory.CreateScope();
            var repository = scope.ServiceProvider.GetRequiredService<IRepositoryWrapper>();
            var dbContext = scope.ServiceProvider.GetRequiredService<BBDbContext>();

            var expiredReservation = await repository.Reservation.FindByConditionAsync(x => DateTime.Now >= x.Expiration && (x.Status != RequisitionStatus.Done && x.Status != RequisitionStatus.Cancelled));
            if (expiredReservation.Any())
            {
                foreach (var reservation in expiredReservation)
                {
                    reservation.Status = RequisitionStatus.Cancelled;
                    repository.Reservation.Update(reservation);

                    var reservationItems = dbContext.ReservationItems.Where(x => x.ReservationId == reservation.ReservationId).ToList();
                    var inventoryItemIds = reservationItems.Select(x => x.InventoryItemId);

                    foreach (var id in inventoryItemIds)
                    {
                        var inventory = await repository.InventoryItem.FindOneByConditionAsync(x => x.InventoryItemId == id);
                        inventory.Status = InventoryStatus.InStock;
                        repository.InventoryItem.Update(inventory);
                    }
                }

                await repository.SaveAsync();
            }
        }

        private async Task TagInventoryItemsStatus()
        {
            bool hasChanges = false;
            using var scope = _scopeFactory.CreateScope();
            var repository = scope.ServiceProvider.GetRequiredService<IRepositoryWrapper>();

            var toBeTaggedAsExpired = await repository.InventoryItem.FindByConditionAsync(x => DateTime.Now >= x.ExpiryDate && (x.Status != InventoryStatus.Expired && x.Status != InventoryStatus.Acquired));
            var expiredItems = toBeTaggedAsExpired.Select(x => x.InventoryItemId).ToList();

            var excludeStatus = new List<string>() { InventoryStatus.NearExpiry, InventoryStatus.Reserved, InventoryStatus.Acquired, InventoryStatus.Expired };
            var toBeTaggedAsNearExpired = await repository.InventoryItem.FindByConditionAsync(x => DateTime.Now >= x.NotifyBeforeExpireOn.Date.AddDays(-1) && (!excludeStatus.Contains(x.Status)) && !expiredItems.Contains(x.InventoryItemId));

            if (toBeTaggedAsExpired.Any())
            {
                hasChanges = true;

                foreach (var item in toBeTaggedAsExpired)
                {
                    item.Status = InventoryStatus.Expired;
                    repository.InventoryItem.Update(item);
                }
            }

            if (toBeTaggedAsNearExpired.Any())
            {
                hasChanges = true;

                foreach (var item in toBeTaggedAsNearExpired)
                {
                    item.Status = InventoryStatus.NearExpiry;
                    repository.InventoryItem.Update(item);
                }
            }

            if (hasChanges)
            {
                await repository.SaveAsync();
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInfo("Timed Hosted Service is stopping");
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }
    }
}
