using BBIS.Database;
using BBIS.Domain.Contracts;
using BBIS.Domain.Models;

namespace BBIS.Application.Repositories
{
    public class DonorDeferralRepository : RepositoryBase<DonorDeferral>, IDonorDeferralRepository
    {
        private BBDbContext dbContext;
        public DonorDeferralRepository(BBDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
