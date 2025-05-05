using BBIS.Database;
using BBIS.Domain.Contracts;
using BBIS.Domain.Models;

namespace BBIS.Application.Repositories
{
    public class DonorRecentDonationRepository: RepositoryBase<DonorRecentDonation>, IDonorRecentDonationRepository
    {
        private BBDbContext dbContext;
        public DonorRecentDonationRepository(BBDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
