using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BBIS.Database;
using BBIS.Domain.Contracts;
using BBIS.Domain.Models;

namespace BBIS.Application.Repositories
{
    public class DonorPostDonationCareRepository : RepositoryBase<DonorPostDonationCare>, IDonorPostDonationCareRepository
    {
        private BBDbContext dbContext;
        public DonorPostDonationCareRepository(BBDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
