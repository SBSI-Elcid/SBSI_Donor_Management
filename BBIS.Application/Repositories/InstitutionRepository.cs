using BBIS.Database;
using BBIS.Domain.Contracts;
using BBIS.Domain.Models;

namespace BBIS.Application.Repositories
{
    public class InstitutionRepository : RepositoryBase<Institution>, IInstitutionRepository
    {
        private BBDbContext dbContext;
        public InstitutionRepository(BBDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
