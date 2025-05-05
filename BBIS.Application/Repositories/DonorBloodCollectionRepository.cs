using BBIS.Database;
using BBIS.Domain.Contracts;
using BBIS.Domain.Models;

namespace BBIS.Application.Repositories
{
    public class DonorBloodCollectionRepository: RepositoryBase<DonorBloodCollection>, IDonorBloodCollectionRepository
    {
        private BBDbContext dbContext;
        public DonorBloodCollectionRepository(BBDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
