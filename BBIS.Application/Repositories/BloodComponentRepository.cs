using BBIS.Database;
using BBIS.Domain.Contracts;
using BBIS.Domain.Models;

namespace BBIS.Application.Repositories
{
    public class BloodComponentRepository : RepositoryBase<BloodComponent>, IBloodComponentRespository
    {
        private BBDbContext dbContext;
        public BloodComponentRepository(BBDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
