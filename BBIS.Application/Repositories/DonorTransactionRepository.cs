using BBIS.Database;
using BBIS.Domain.Contracts;
using BBIS.Domain.Models;

namespace BBIS.Application.Repositories
{
    public class DonorTransactionRepository: RepositoryBase<DonorTransaction>, IDonorTransactionRepository
    {
        private BBDbContext dbContext;
        public DonorTransactionRepository(BBDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
