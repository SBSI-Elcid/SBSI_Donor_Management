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

    public class LibrariesRoleRepository : RepositoryBase<Role>, ILibrariesRole
    {
        private BBDbContext dbContext;
        public LibrariesRoleRepository(BBDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
