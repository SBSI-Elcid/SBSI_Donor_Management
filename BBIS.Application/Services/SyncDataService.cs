using BBIS.Application.Contracts;
using BBIS.Database;
using BBIS.Domain.Contracts;
using BBIS.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BBIS.Application.Services
{
    public class SyncDataService : ISyncDataService
    {
        private readonly BBDbContext dbContext;
        private readonly ExternalBBDbContext externalDb;
        private readonly IRepositoryWrapper repository;

        public SyncDataService(BBDbContext dbContext, ExternalBBDbContext externalDb, IRepositoryWrapper repository)
        {
            this.dbContext = dbContext;
            this.externalDb = externalDb;
            this.repository = repository;
        }

        public async Task<string> SyncDonors()
        {
            var message = "";
            var transaction = dbContext.Database.BeginTransaction();

            try
            {
                var offlineDonorTransactions = await dbContext.DonorTransactions
                    .Include(x => x.Donor)
                    .Include(x => x.DonorRegistration)
                    .Include(x => x.DonorInitialScreening)
                    .Include(x => x.DonorPhysicalExamination)
                    .Include(x => x.DonorBloodCollection)
                    .Include(x => x.DonorDeferral)
                    .Include(x => x.DonorTestOrder)
                    .Where(x => !x.IsSync && x.IsOffline
                        && x.DonorInitialScreening != null
                        && x.DonorPhysicalExamination != null
                        && x.DonorBloodCollection != null)
                    .Take(200)
                    .ToListAsync();

                if (offlineDonorTransactions.Any())
                {
                    foreach (var donorTransaction in offlineDonorTransactions)
                    {
                        donorTransaction.IsSync = true;

                        await externalDb.DonorTransactions.AddAsync(donorTransaction);
                    }

                    await dbContext.SaveChangesAsync();

                    await externalDb.SaveChangesAsync();

                    message = "Offline data was successfully sync.";

                    await transaction.CommitAsync();
                }
                else
                {
                    message = "No data to sync.";
                }

            }
            catch (Exception)
            {
                transaction.Rollback();

                throw;
            }
            
            return message;
        }
    }
}
