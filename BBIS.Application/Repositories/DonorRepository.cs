using BBIS.Database;
using BBIS.Domain.Contracts;
using BBIS.Domain.Models;

namespace BBIS.Application.Repositories
{
    public class DonorRegistrationRepository: RepositoryBase<DonorRegistration>, IDonorRegistrationRepository
    {
        private BBDbContext dbContext;
        public DonorRegistrationRepository(BBDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
