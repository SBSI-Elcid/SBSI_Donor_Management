using BBIS.Database;
using BBIS.Domain.Contracts;
using BBIS.Domain.Models;

namespace BBIS.Application.Repositories
{
    public class TransfusionRepository : RepositoryBase<Transfusion>, ITransfusionRepository
    {
        private BBDbContext dbContext;
        public TransfusionRepository(BBDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
