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
    public class LibrariesQuestionnareRepository : RepositoryBase<MedicalQuestionnaire>, ILibraryQuestionnareRepository
    {
        private BBDbContext dbContext;
        public LibrariesQuestionnareRepository(BBDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
