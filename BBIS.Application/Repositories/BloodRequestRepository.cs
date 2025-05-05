using BBIS.Database;
using BBIS.Domain.Contracts;
using BBIS.Domain.Models;

namespace BBIS.Application.Repositories
{
    public class BloodRequestRepository : RepositoryBase<Reservation>, IBloodRequestRepository
    {
        private BBDbContext dbContext;
        public BloodRequestRepository(BBDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
