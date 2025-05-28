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
    public class PostDonationDetailRepository : RepositoryBase<PostDonationDetail>, IPostDonationDetailsRepository
    {
        private BBDbContext dbContext;
        public PostDonationDetailRepository(BBDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
