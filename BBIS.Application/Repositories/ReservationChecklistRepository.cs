using BBIS.Database;
using BBIS.Domain.Contracts;
using BBIS.Domain.Models;

namespace BBIS.Application.Repositories
{
    public class ReservationChecklistRepository : RepositoryBase<ReservationChecklist>, IReservationChecklistRepository
    {
        private BBDbContext dbContext;
        public ReservationChecklistRepository(BBDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
