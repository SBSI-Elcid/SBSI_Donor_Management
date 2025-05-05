using BBIS.Database;
using BBIS.Domain.Contracts;
using BBIS.Domain.Models;

namespace BBIS.Application.Repositories
{
    public class DonorInitialScreeningRepository: RepositoryBase<DonorInitialScreening>, IDonorInitialScreeningRepository
    {
        private BBDbContext dbContext;
        public DonorInitialScreeningRepository(BBDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
