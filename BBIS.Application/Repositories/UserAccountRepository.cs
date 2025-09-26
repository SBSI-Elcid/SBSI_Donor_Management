using BBIS.Database;
using BBIS.Domain.Contracts;
using BBIS.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BBIS.Application.Repositories
{
    public class UserAccountRepository: RepositoryBase<UserAccount>, IUserAccountRepository
    {
        private BBDbContext dbContext;
        public UserAccountRepository(BBDbContext dbContext): base(dbContext) 
        {
            this.dbContext = dbContext;
        }

        public async Task<UserAccount> GetAuthUserAsync(string username)
        {
            var user = await dbContext.UserAccounts
                .Include(x => x.UserRefreshToken)
                .Include(x => x.UserRoles)
                    .ThenInclude(x => x.Role)
                     .ThenInclude(r => r.UserRoleScreeningAccesses)
                .FirstOrDefaultAsync(x => x.Username == username && x.IsDeleted == false && x.IsActive == true);
               
            return user;
        }

        public async Task<UserRefreshToken> GetRefreshToken(Guid userAccountId)
        {
            return await dbContext.UserRefreshTokens.FirstOrDefaultAsync(x => x.UserAccountId == userAccountId);
        }

        public void ClearUserRoles(Guid userAccountId)
        {
            var userRoles = dbContext.UserRoles.Where(x => x.UserAccountId == userAccountId);
            dbContext.UserRoles.RemoveRange(userRoles);
        }

        public async Task UpsertRefreshToken(UserRefreshToken userRefresh)
        {
            var currentRefreshToken = await GetRefreshToken(userRefresh.UserAccountId);

            // If does not exist, create a new one
            if (currentRefreshToken == null)
            {
                await dbContext.AddAsync(userRefresh);
            }
            else //update it
            {
                dbContext.Update(userRefresh);
            }

            await dbContext.SaveChangesAsync();
        }

        public void ClearUserModules(Guid userAccountId)
        {
            var records = dbContext.UserModules.Where(x => x.UserAccountId == userAccountId);
            dbContext.UserModules.RemoveRange(records);
        }
    }
}
