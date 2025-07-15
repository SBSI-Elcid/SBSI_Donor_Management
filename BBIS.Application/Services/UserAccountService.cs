using AutoMapper;
using BBIS.Application.Contracts;
using BBIS.Application.DTOs.Common;
using BBIS.Application.DTOs.Module;
using BBIS.Application.DTOs.UserAccount;
using BBIS.Common;
using BBIS.Common.Encryption;
using BBIS.Common.Exceptions;
using BBIS.Database;
using BBIS.Domain.Contracts;
using BBIS.Domain.Models;
using Microsoft.EntityFrameworkCore;
using NinjaNye.SearchExtensions;
using System.Linq.Dynamic.Core;
using Module = BBIS.Domain.Models.Module;

namespace BBIS.Application.Services
{
    public class UserAccountService: IUserAccountService
    {
        private readonly IRepositoryWrapper repository;
        private readonly IMapper mapper;
        private readonly IEncryptionHandler encryptionHandler;
        private readonly BBDbContext dbContext;

        public UserAccountService(
            IRepositoryWrapper repository, 
            IEncryptionHandler encryptionHandler, 
            IMapper mapper,
            BBDbContext dbContext
            )
        {
            this.repository = repository;
            this.mapper = mapper;
            this.encryptionHandler = encryptionHandler;
            this.dbContext = dbContext;
        }

        public async Task<Guid> CreateUserAccount(CreateUserAccountDto accountDto, Guid currentUserId)
        {
            // Check if Username is unique
            var user = await GetUserAccountByUserName(accountDto.Username, null);
            if(user != null)
            {
                throw new RecordExistException($"Username '{accountDto.Username}' already exist.");
            }

            // Check if EmailAddress is unique
            user = await GetUserAccountByEmail(accountDto.EmailAddress, null);
            if (user != null)
            {
                throw new RecordExistException($"Email address '{accountDto.EmailAddress}' already exist.");
            }

            var userAccount = mapper.Map<UserAccount>(accountDto);
            userAccount.IsActive = true;
            userAccount.IsDeleted = false;
            userAccount.UpdatedAt = DateTime.UtcNow;
            userAccount.UpdatedBy = currentUserId;

            if(!string.IsNullOrEmpty(accountDto.Password))
            {
                var passwordSalt = encryptionHandler.CreatSalt();
                var hashedPassword = encryptionHandler.HashPassword(accountDto.Password, passwordSalt);

                userAccount.Password = hashedPassword;
                userAccount.PasswordSalt = passwordSalt;
            }

            if(accountDto.RoleIds != null && accountDto.RoleIds.Any())
            {
                userAccount.UserRoles = new List<UserRole>();

                foreach (var roleId in accountDto.RoleIds)
                {
                    if (roleId == Guid.Empty)
                    {
                        continue;
                    }

                    userAccount.UserRoles.Add(new UserRole { RoleId = roleId, UserAccountId = userAccount.UserAccountId });
                }
            }

            if (accountDto.UserModuleIds.Any())
            {
                userAccount.UserModules = new List<UserModule>();

                foreach (var moduleId in accountDto.UserModuleIds)
                {
                    if (moduleId == Guid.Empty)
                    {
                        continue;
                    }

                    userAccount.UserModules.Add(new UserModule { ModuleId = moduleId, UserAccountId = userAccount.UserAccountId });
                }
            }

            repository.UserAccount.Create(userAccount);
            await repository.SaveAsync();

            return userAccount.UserAccountId;
        }

        public async Task DeleteUserAccount(Guid id, Guid currentUserId)
        {
            var userAccount = await repository.UserAccount.FindOneByConditionAsync(x => x.UserAccountId == id && x.IsDeleted == false);
            if (userAccount == null)
            {
                throw new RecordNotFoundException("User account not found.");
            }

            userAccount.IsDeleted = true; // Soft delete record
            userAccount.UpdatedAt = DateTime.UtcNow;
            userAccount.UpdatedBy = currentUserId;

            repository.UserAccount.Update(userAccount);

            await repository.SaveAsync();
        }

        public async Task<UserAccountViewDto> GetUserAccount(Guid id)
        {
            var user = await repository.UserAccount.FindOneByConditionAsync(x => x.UserAccountId == id && x.IsDeleted == false);
            if(user == null)
            {
                throw new RecordNotFoundException("User account not found.");
            }

            var roleIds = await dbContext.UserRoles
                .Where(x => x.UserAccountId == id)
                .Select(x=> x.RoleId)
                .ToListAsync();

            var userModules = await dbContext.UserModules
                .Include(x => x.Module)
                .Where(x => x.UserAccountId == id)
                .Select(x => x.Module)
                .ToListAsync();

            var result = mapper.Map<UserAccountViewDto>(user);
            result.RoleIds = roleIds;
            
            if (userModules != null)
            {
                result.UserModules = ConvertToModuleDtoList(userModules, true);
            }
           
            return result;
        }

        public async Task UpdateUserAccount(UpdateUserAccountDto accountDto, Guid currentUserId)
        {
            var userAccount = await dbContext.UserAccounts
                .Include(x => x.UserRoles)
                .Include(x => x.UserModules)
                .FirstOrDefaultAsync(x => x.UserAccountId == accountDto.UserAccountId && !x.IsDeleted);
           
            if (userAccount == null)
            {
                throw new RecordNotFoundException("User account not found.");
            }

            // Check if Username is unique
            var checkuser = await GetUserAccountByUserName(accountDto.Username, accountDto.UserAccountId);
            if (checkuser != null)
            {
                throw new RecordExistException($"Username '{accountDto.Username}' already exist.");
            }

            // Check if EmailAddress is unique
            checkuser = await GetUserAccountByEmail(accountDto.EmailAddress, accountDto.UserAccountId);
            if (checkuser != null)
            {
                throw new RecordExistException($"Email address '{accountDto.EmailAddress}' already exist.");
            }

            userAccount.Username = accountDto.Username;
            userAccount.Firstname = accountDto.Firstname;
            userAccount.Lastname = accountDto.Lastname;
            userAccount.EmailAddress = accountDto.EmailAddress;

            userAccount.IsActive = accountDto.IsActive;
            userAccount.UpdatedAt = DateTime.UtcNow;
            userAccount.UpdatedBy = currentUserId;
          
            // Update password if it was change
            if (accountDto.PasswordHasChange)
            {
                var hashedPassword = encryptionHandler.HashPassword(accountDto.UpdatedPassword, userAccount.PasswordSalt);
                userAccount.Password = hashedPassword;
            }

            // Roles
            // Get the roles that needs to be added
            var newRoleIds = accountDto.RoleIds.Where(id => !userAccount.UserRoles.Any(cur => cur.RoleId == id)).ToList();
            // Get the roles that has been removed from the list
            var toRemoveRoles = userAccount.UserRoles.Where(x => !accountDto.RoleIds.Any(id => id == x.RoleId)).ToList();

            if (newRoleIds.Any())
            {
                foreach (var roleId in newRoleIds)
                {
                    userAccount.UserRoles.Add(new UserRole { RoleId = roleId, UserAccountId = userAccount.UserAccountId });
                }
            }

            if (toRemoveRoles.Any())
            {
                dbContext.UserRoles.RemoveRange(toRemoveRoles);
            }

            // User modules
            //  Get the user modules that needs to be added
            var newUserModuleIds = accountDto.UserModuleIds.Where(id => !userAccount.UserModules.Any(cur => cur.ModuleId == id)).ToList();
            //  Get the user modules that has bee removed from the list
            var toRemoveModules = userAccount.UserModules.Where(x => !accountDto.UserModuleIds.Any(id => id == x.ModuleId)).ToList();

            if (newUserModuleIds.Any())
            {
                foreach (var moduleId in newUserModuleIds)
                {
                    userAccount.UserModules.Add(new UserModule { ModuleId = moduleId, UserAccountId = userAccount.UserAccountId });
                }
            }

            if (toRemoveModules.Any())
            {
                dbContext.UserModules.RemoveRange(toRemoveModules);
            }

            await dbContext.SaveChangesAsync();
        }

        public async Task<PagedSearchResultDto<UserAccountViewDto>> GetPagedListAsync(PagedSearchDto searchDto)
        {
            if(searchDto == null)
            {
                throw new ArgumentNullException(nameof(PagedSearchDto));
            }

            var sortBy = string.IsNullOrEmpty(searchDto.SortBy) ? "Firstname asc" : searchDto.SortBy + (searchDto.SortDesc ? " desc" : " asc");
            var pagedResult = new PagedSearchResultDto<UserAccountViewDto>(searchDto);

            var searchText = searchDto.SearchText;

            var query = dbContext.UserAccounts
                .Where(x => x.IsDeleted == false)
                .Search(t => t.Username, t => t.Firstname, t => t.Lastname, t => t.EmailAddress);

            // If the items per page selected is "All" the underlying value of it is -1, so use the total count of data as page size.
            if (searchDto.PageSize == -1)
            {
                searchDto.PageSize = query.Count();
            }

            var results = await query
                .Containing(searchText)
                    .Skip((searchDto.PageNumber - 1) * searchDto.PageSize)
                    .Take(searchDto.PageSize)
                    .Select(x =>
                        new UserAccountViewDto
                        {
                            Username = x.Username,
                            Firstname = x.Firstname,
                            Lastname = x.Lastname,
                            EmailAddress = x.EmailAddress,
                            IsActive = x.IsActive,
                            UserAccountId = x.UserAccountId
                        }
                    ).ToListAsync();

            var totalRecords = query.Count();

            if (results == null || !results.Any()) return pagedResult;
                        
            pagedResult.TotalCount = totalRecords;
            pagedResult.Results = results.AsQueryable()
                .OrderBy(sortBy)
                .ToList();

            return pagedResult;
        }

        public async Task<(string, string)> UpdateUserProfile(UserProfileUpdateDto dto)
        {
            string field = null;
            string error = null;
            var hasChanges = false;

            var user = await repository.UserAccount.FindOneByConditionAsync(x => x.UserAccountId == dto.UserId && x.IsDeleted == false);
            if (user == null)
            {
                error = "User not found.";
                return (field, error);
            }

            // check username
            if (user.Username != dto.Username && !string.IsNullOrEmpty(dto.Username) && string.IsNullOrEmpty(error))
            {
                var existingUser = await GetUserAccountByUserName(dto.Username, dto.UserId);
                if (existingUser == null)
                {
                    user.Username = dto.Username;
                    hasChanges = true;
                }
                else
                {
                    field = "Username";
                    error = "Username already exists.";
                }
            }

            // verify current password
            if (!string.IsNullOrEmpty(dto.CurrentPassword) && !string.IsNullOrEmpty(dto.NewPassword) && string.IsNullOrEmpty(error))
            {
                if (!VerifyPassword(user, dto.CurrentPassword))
                {
                    field = "CurrentPassword";
                    error = "Invalid current password.";
                }
                else
                {
                    //hashed the new password
                    var salt = this.encryptionHandler.CreatSalt();
                    var newHashedPassword = this.encryptionHandler.HashPassword(dto.NewPassword, salt);

                    user.Password = newHashedPassword;
                    user.PasswordSalt = salt;
                    hasChanges = true;
                }
            }

            if (string.IsNullOrEmpty(error) && hasChanges)
            {
                user.UpdatedAt = DateTime.UtcNow;
                user.UpdatedBy = dto.UserId;
                repository.UserAccount.Update(user);
                await repository.SaveAsync();
            }

            return (field, error);
        }

        public async Task<List<ModuleDto>> GetUserModules(Guid userAccountId)
        {
            var result = new List<ModuleDto>();

            var userAccount = await dbContext.UserAccounts
                .Include(x => x.UserModules).ThenInclude(x => x.Module)
                .Include(x => x.UserRoles).ThenInclude(x => x.Role)
                .FirstOrDefaultAsync(x => x.UserAccountId == userAccountId && !x.IsDeleted);

            if(userAccount == null)
            {
                throw new RecordNotFoundException($"User not found for Id: {userAccountId}");
            }

            var modules = new List<Module>();

            // Check if user has admin role then load all modules
            var hasAdminAccess = false;
            if (userAccount.UserRoles != null  && userAccount.UserRoles.Any())
            {
                hasAdminAccess = userAccount.UserRoles.Any(x => x.Role.RoleName == ApplicationRoles.AdminRole);
                 
                if (hasAdminAccess) {
                    modules = await dbContext.Modules.Where(x => x.IsActive).ToListAsync();
                }
            }

            if (!hasAdminAccess && userAccount.UserModules.Any())
            {
                modules = userAccount.UserModules.Where(x => x.Module.IsActive)
                    .Select(x => x.Module)
                    .ToList();
            }

            return ConvertToModuleDtoList(modules);
        }

        private List<ModuleDto> GetSubMenuItems(Guid parentId, List<Module> modules)
        {
            var result = new List<ModuleDto>();
            var childItems = modules.Where(x => x.ParentModuleId== parentId).ToList();

            if (childItems.Any())
            {
                foreach (var item in childItems)
                {
                    result.Add(
                        new ModuleDto
                        {
                            OrderNo = item.OrderNo,
                            Icon = item.Icon,
                            Link = item.Link,
                            Menu = item.Menu,
                            IsParentMenu = item.IsParentMenu,
                            ModuleId = item.ModuleId,
                            SubMenuItems = new List<ModuleDto>(),
                            ParentModuleId = item.ParentModuleId
                        });
                }
            }

            return result.OrderBy(x => x.OrderNo).ToList();
        }

        public async Task<List<ModuleDto>> GetAllModules()
        {
            var modules = await dbContext.Modules
                .Where(x => x.IsActive)
                .AsNoTracking()
                .ToListAsync();

            return ConvertToModuleDtoList(modules);
        }

        private List<ModuleDto> ConvertToModuleDtoList(List<Module> modules, bool skipCheckForSubItems = false)
        {
            var result = new List<ModuleDto>();

            if (modules.Any())
            {
                foreach (var module in modules)
                {
                    if (module.ParentModuleId.HasValue && !skipCheckForSubItems)
                    {
                        continue;
                    }

                    var dto = new ModuleDto
                    {
                        Icon = module.Icon,
                        IsParentMenu = module.IsParentMenu,
                        Link = module.Link,
                        Menu = module.Menu,
                        ModuleId = module.ModuleId,
                        OrderNo = module.OrderNo,
                        SubMenuItems = new List<ModuleDto>(),
                        ParentModuleId = module.ParentModuleId
                    };

                    if (module.IsParentMenu)
                    {
                        dto.SubMenuItems = GetSubMenuItems(module.ModuleId, modules);
                    }

                    result.Add(dto);
                }
            }
            Console.WriteLine(result);
            return result.OrderBy(x => x.OrderNo).ToList();

        }

        #region private methods

        private async Task<UserAccount> GetUserAccountByUserName(string userName, Guid? id)
        {
            var user = await repository.UserAccount.FindOneByConditionAsync(x => x.Username == userName && x.UserAccountId != id);
            return user;
        }

        private async Task<UserAccount> GetUserAccountByEmail(string email, Guid? id)
        {
            var user = await repository.UserAccount.FindOneByConditionAsync(x => x.EmailAddress == email && x.UserAccountId != id);
            return user;
        }

        private bool VerifyPassword(UserAccount user, string givenPassword)
        {
            var hashedGivenPassword = this.encryptionHandler.HashPassword(givenPassword, user.PasswordSalt);
            return hashedGivenPassword == user.Password;
        }

        #endregion
    }
}
