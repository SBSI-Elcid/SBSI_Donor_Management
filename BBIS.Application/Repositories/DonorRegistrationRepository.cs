using BBIS.Database;
using BBIS.Domain.Contracts;
using BBIS.Domain.Models;

namespace BBIS.Application.Repositories
{
    public class DonorRepository: RepositoryBase<Donor>, IDonorRepository
    {
        private BBDbContext dbContext;
        public DonorRepository(BBDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
