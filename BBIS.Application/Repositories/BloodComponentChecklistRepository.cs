using BBIS.Database;
using BBIS.Domain.Contracts;
using BBIS.Domain.Models;

namespace BBIS.Application.Repositories
{
    internal class BloodComponentChecklistRepository : RepositoryBase<BloodComponentChecklist>, IBloodComponentChecklistRepository
    {
        private BBDbContext dbContext;
        public BloodComponentChecklistRepository(BBDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
