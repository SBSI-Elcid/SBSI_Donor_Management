using BBIS.Database;
using BBIS.Domain.Contracts;
using BBIS.Domain.Models;

namespace BBIS.Application.Repositories
{
    public class TestOrderTypeRepository : RepositoryBase<TestOrderType>, ITestOrderTypeRepository
    {
        private BBDbContext dbContext;
        public TestOrderTypeRepository(BBDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
