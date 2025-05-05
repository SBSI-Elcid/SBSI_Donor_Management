using BBIS.Database;
using BBIS.Domain.Contracts;
using BBIS.Domain.Models;

namespace BBIS.Application.Repositories
{
    public class InventorySourceRepository : RepositoryBase<InventorySource>, IInventorySourceRepository
    {
        private BBDbContext dbContext;
        public InventorySourceRepository(BBDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
