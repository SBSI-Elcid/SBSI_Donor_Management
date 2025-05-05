using BBIS.Database;
using BBIS.Domain.Contracts;
using BBIS.Domain.Models;

namespace BBIS.Application.Repositories
{
    public class TestOrderRepository : RepositoryBase<TestOrder>, ITestOrderRepository
    {
        private BBDbContext dbContext;
        public TestOrderRepository(BBDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
