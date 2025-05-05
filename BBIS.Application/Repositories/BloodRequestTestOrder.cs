using BBIS.Database;
using BBIS.Domain.Contracts;
using BBIS.Domain.Models;

namespace BBIS.Application.Repositories
{
    public class BloodRequestTestOrderRepository : RepositoryBase<TestOrder>, IBloodRequestTestOrderRepository
    {
        private BBDbContext dbContext;
        public BloodRequestTestOrderRepository(BBDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
