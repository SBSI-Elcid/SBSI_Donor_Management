using BBIS.Application.DTOs.Common;
using BBIS.Application.DTOs.Module;
using BBIS.Application.DTOs.UserAccount;

namespace BBIS.Application.Contracts
{
    public interface IUserAccountService
    {
        Task<Guid> CreateUserAccount(CreateUserAccountDto accountDto, Guid currentUserId);

        Task UpdateUserAccount(UpdateUserAccountDto accountDto, Guid currentUserId);

        Task DeleteUserAccount(Guid id, Guid currentUserId);

        Task<UserAccountViewDto> GetUserAccount(Guid id);

        Task<PagedSearchResultDto<UserAccountViewDto>> GetPagedListAsync(PagedSearchDto searchDto);

        Task<(string, string)> UpdateUserProfile(UserProfileUpdateDto dto);

        Task<List<ModuleDto>> GetUserModules(Guid userAccountId);

        Task<List<ModuleDto>> GetAllModules();
    }
}
