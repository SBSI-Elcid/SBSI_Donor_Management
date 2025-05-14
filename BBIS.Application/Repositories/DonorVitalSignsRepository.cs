using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BBIS.Application.Repositories;
using BBIS.Database;
using BBIS.Domain.Contracts;
using BBIS.Domain.Models;

namespace BBIS.Application.Repositories
{
    public class DonorVitalSignsRepository : RepositoryBase<DonorVitalSigns>, IDonorVitalSignsRepository
    {
        private BBDbContext dbContext;
        public DonorVitalSignsRepository(BBDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
