using BBIS.Database;
using BBIS.Domain.Contracts;
using BBIS.Domain.Models;

namespace BBIS.Application.Repositories
{
    public class SignatoryRepository : RepositoryBase<Signatory>, ISignatoryRepository
    {
        private BBDbContext dbContext;
        public SignatoryRepository(BBDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
