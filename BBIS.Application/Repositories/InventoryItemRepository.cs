using BBIS.Database;
using BBIS.Domain.Contracts;
using BBIS.Domain.Models;

namespace BBIS.Application.Repositories
{
    public class InventoryItemRepository : RepositoryBase<InventoryItem>, IInventoryItemRepository
    {
        private BBDbContext dbContext;
        public InventoryItemRepository(BBDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
