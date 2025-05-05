using BBIS.Database;
using BBIS.Domain.Contracts;
using BBIS.Domain.Models;

namespace BBIS.Application.Repositories
{
    public class ApplicationSettingRepository : RepositoryBase<ApplicationSetting>, IApplicationSettingRepository
    {
        private BBDbContext dbContext;
        public ApplicationSettingRepository(BBDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
