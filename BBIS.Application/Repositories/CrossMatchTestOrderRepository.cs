using BBIS.Database;
using BBIS.Domain.Contracts;
using BBIS.Domain.Models;

namespace BBIS.Application.Repositories
{
    public class CrossMatchTestOrderRepository : RepositoryBase<TestOrder>, ICrossMatchTestOrderRepository
    {
        private BBDbContext dbContext;
        public CrossMatchTestOrderRepository(BBDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
