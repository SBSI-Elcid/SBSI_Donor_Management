using BBIS.Database;
using BBIS.Domain.Contracts;
using BBIS.Domain.Models;

namespace BBIS.Application.Repositories
{
    public class OtherTestOrderRepository : RepositoryBase<OtherTestOrder>, IOtherTestOrderRepository
    {
        private BBDbContext dbContext;
        public OtherTestOrderRepository(BBDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
