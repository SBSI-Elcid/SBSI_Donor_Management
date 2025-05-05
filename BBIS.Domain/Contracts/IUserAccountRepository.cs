using BBIS.Domain.Models;

namespace BBIS.Domain.Contracts
{
    public interface IUserAccountRepository: IRepositoryBase<UserAccount>
    {
        void ClearUserRoles(Guid userAccountId);

        void ClearUserModules(Guid userAccountId);

        Task<UserAccount> GetAuthUserAsync(string username);

        Task UpsertRefreshToken(UserRefreshToken userRefresh);

        Task<UserRefreshToken> GetRefreshToken(Guid userAccountId);
    }
}
