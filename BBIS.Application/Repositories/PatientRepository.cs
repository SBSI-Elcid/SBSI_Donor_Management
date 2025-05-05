using BBIS.Database;
using BBIS.Domain.Contracts;
using BBIS.Domain.Models;

namespace BBIS.Application.Repositories
{
    public class PatientRepository : RepositoryBase<Patient>, IPatientRepository
    {
        private BBDbContext dbContext;
        public PatientRepository(BBDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
