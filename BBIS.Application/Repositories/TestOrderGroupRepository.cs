using BBIS.Database;
using BBIS.Domain.Contracts;
using BBIS.Domain.Models;

namespace BBIS.Application.Repositories
{
    public class TestOrderGroupRepository : RepositoryBase<TestOrder>, ITestOrderGroupRepository
    {
        private BBDbContext dbContext;
        public TestOrderGroupRepository(BBDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
