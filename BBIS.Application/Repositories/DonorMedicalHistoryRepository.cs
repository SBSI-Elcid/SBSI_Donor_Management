using BBIS.Database;
using BBIS.Domain.Contracts;
using BBIS.Domain.Models;

namespace BBIS.Application.Repositories
{
    public class DonorMedicalHistoryRepository: RepositoryBase<DonorMedicalHistory>, IDonorMedicalHistoryRepository
    {
        private BBDbContext dbContext;
        public DonorMedicalHistoryRepository(BBDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
