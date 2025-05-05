using BBIS.Database;
using BBIS.Domain.Contracts;
using BBIS.Domain.Models;

namespace BBIS.Application.Repositories
{
    public class DonorPhysicalExaminationRepository: RepositoryBase<DonorPhysicalExamination>, IDonorPhysicalExaminationRepository
    {
        private BBDbContext dbContext;
        public DonorPhysicalExaminationRepository(BBDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
