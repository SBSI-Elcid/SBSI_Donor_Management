using AutoMapper;
using AutoMapper.QueryableExtensions;
using BBIS.Application.Contracts;
using BBIS.Application.DTOs.ApplicationSetting;
using BBIS.Application.DTOs.Common;
using BBIS.Application.DTOs.DonorRegistration;
using BBIS.Application.DTOs.DonorScreening;
using BBIS.Common.Enums;
using BBIS.Common.Exceptions;
using BBIS.Database;
using BBIS.Domain.Contracts;
using BBIS.Domain.Models;
using Microsoft.EntityFrameworkCore;
using NinjaNye.SearchExtensions;
using System.Linq.Dynamic.Core;

namespace BBIS.Application.Services
{
    public class ApplicationSettingService : IApplicationSettingService
    {
        private readonly BBDbContext dbContext;
        private readonly IRepositoryWrapper repository;
        private readonly IMapper mapper;

        public ApplicationSettingService(BBDbContext dbContext, IRepositoryWrapper repository, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<PagedSearchResultDto<RoleDto>> GetRoleSettings(PagedSearchDto searchDto)
        {
            if (searchDto == null)
            {
                throw new ArgumentNullException(nameof(PagedSearchDto));
            }

            var sortBy = string.IsNullOrEmpty(searchDto.SortBy) ? "Rolename asc" : searchDto.SortBy + (searchDto.SortDesc ? " desc" : " asc");
            var pagedResult = new PagedSearchResultDto<RoleDto>(searchDto);

            var searchText = searchDto.SearchText;

            var query = dbContext.Roles
                .Search(t => t.RoleName)
                    .Containing(searchText);

            // If the items per page selected is "All" the underlying value of it is -1, so use the total count of data as page size.
            if (searchDto.PageSize == -1)
            {
                searchDto.PageSize = query.Count();
            }

                var resultsEntities = await query
            .Skip((searchDto.PageNumber - 1) * searchDto.PageSize)
            .Take(searchDto.PageSize)
            .ToListAsync();

            var totalRecords = query.Count();

            if (resultsEntities == null || !resultsEntities.Any()) return pagedResult;

            var results = mapper.Map<List<RoleDto>>(resultsEntities);


            var roleIds = results.Select(r => r.RoleId).ToList();

            var screeningAccesses = await dbContext.UserRoleScreeningAccess
            .Where(u => roleIds.Contains(u.RoleId))
            .ToListAsync();

            foreach (var roleDto in results)
            {
                var matchingAccesses = screeningAccesses
                    .Where(a => a.RoleId == roleDto.RoleId)
                    .ToList();

                roleDto.UserRoleAccesses = mapper.Map<List<UserRoleScreeningAccessDto>>(matchingAccesses);
            }


            pagedResult.TotalCount = totalRecords;
            pagedResult.Results = results.AsQueryable()
                .OrderBy(sortBy)
                .ToList();

            return pagedResult;
        }

        public async Task<PagedSearchResultDto<MedicalQuestionnaireDto>> GetQuestionnaireSettings(PagedSearchDto searchDto)
        {
            if (searchDto == null)
                throw new ArgumentNullException(nameof(PagedSearchDto));

            var sortBy = string.IsNullOrEmpty(searchDto.SortBy)
                ? "OrderNo asc" // Default sort by OrderNo ascending
                : searchDto.SortBy + (searchDto.SortDesc ? " desc" : " asc");

            var pagedResult = new PagedSearchResultDto<MedicalQuestionnaireDto>(searchDto);

            var searchText = searchDto.SearchText;

            var query = dbContext.MedicalQuestionnaires.AsQueryable();

            // Filter if searchText is provided
            if (!string.IsNullOrWhiteSpace(searchText))
            {
                query = query.Where(x =>
                    x.HeaderText.Contains(searchText) ||
                    x.QuestionEnglishText.Contains(searchText) ||
                    x.QuestionTagalogText.Contains(searchText) ||
                    x.QuestionOtherDialectText.Contains(searchText)
                );
            }

            var totalRecords = await query.CountAsync();

            // If "All" is selected (PageSize == -1), use total count
            if (searchDto.PageSize == -1)
            {
                searchDto.PageSize = totalRecords;
            }

            var results = await query
                .Skip((searchDto.PageNumber - 1) * searchDto.PageSize)
                .Take(searchDto.PageSize)
                .Select(x => new MedicalQuestionnaireDto
                {
                    HeaderText = x.HeaderText,
                    MedicalQuestionnaireId = x.MedicalQuestionnaireId,
                    OrderNo = x.OrderNo,
                    QuestionEnglishText = x.QuestionEnglishText,
                    QuestionOtherDialectText = x.QuestionOtherDialectText,
                    QuestionTagalogText = x.QuestionTagalogText,
                    GenderOption = x.GenderOption
                })
                .ToListAsync();

            if (results == null || !results.Any())
                return pagedResult;

            pagedResult.TotalCount = totalRecords;

            // Dynamic ordering on paged results
            pagedResult.Results = results.AsQueryable()
                                         .OrderBy(sortBy)
                                         .ToList();

            return pagedResult;
        }

        public async Task<List<RoleDto>> GetAllRoles()
        {
            var accessList = await dbContext.Roles
                .AsNoTracking()
                .ToListAsync();

            return mapper.Map<List<RoleDto>>(accessList);
        }


        public async Task<PagedSearchResultDto<ApplicationSettingDto>> GetApplicationSettings(PagedSearchDto searchDto)
        {
            if (searchDto == null)
            {
                throw new ArgumentNullException(nameof(PagedSearchDto));
            }

            var sortBy = string.IsNullOrEmpty(searchDto.SortBy) ? "SettingKey asc" : searchDto.SortBy + (searchDto.SortDesc ? " desc" : " asc");
            var pagedResult = new PagedSearchResultDto<ApplicationSettingDto>(searchDto);

            var searchText = searchDto.SearchText;

            var query = dbContext.ApplicationSettings
                .Search(t => t.SettingKey)
                    .Containing(searchText);

            // If the items per page selected is "All" the underlying value of it is -1, so use the total count of data as page size.
            if (searchDto.PageSize == -1)
            {
                searchDto.PageSize = query.Count();
            }

            var results = await query
                .Skip((searchDto.PageNumber - 1) * searchDto.PageSize)
                .Take(searchDto.PageSize)
                .Select(x =>
                        new ApplicationSettingDto
                        {
                            ApplicationSettingId = x.ApplicationSettingId,
                            SettingKey = x.SettingKey,
                            SettingValue = x.SettingValue,
                            IsActive = x.IsActive
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

        public async Task<PagedSearchResultDto<BloodComponentSettingDto>> GetBloodComponentSettings(PagedSearchDto searchDto)
        {
            if (searchDto == null)
            {
                throw new ArgumentNullException(nameof(PagedSearchDto));
            }

            var sortBy = string.IsNullOrEmpty(searchDto.SortBy) ? "ComponentName asc" : searchDto.SortBy + (searchDto.SortDesc ? " desc" : " asc");
            var pagedResult = new PagedSearchResultDto<BloodComponentSettingDto>(searchDto);

            var searchText = searchDto.SearchText;

            var query = dbContext.BloodComponents
                .Search(t => t.ComponentName)
                    .Containing(searchText);

            // If the items per page selected is "All" the underlying value of it is -1, so use the total count of data as page size.
            if (searchDto.PageSize == -1)
            {
                searchDto.PageSize = query.Count();
            }

            var results = await query
                .Skip((searchDto.PageNumber - 1) * searchDto.PageSize)
                .Take(searchDto.PageSize)
                .Select(x =>
                        new BloodComponentSettingDto
                        {
                            BloodComponentId = x.BloodComponentId,
                            ComponentName = x.ComponentName,
                            ExpiryInDays = x.ExpiryInDays,
                            NotifyOnDaysBefore = x.NotifyOnDaysBefore,
                            IsActive = x.IsActive
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

        public async Task<PagedSearchResultDto<InstitutionDto>> GetInstitutions(PagedSearchDto searchDto)
        {
            if (searchDto == null)
            {
                throw new ArgumentNullException(nameof(PagedSearchDto));
            }

            var sortBy = string.IsNullOrEmpty(searchDto.SortBy) ? "Name asc" : searchDto.SortBy + (searchDto.SortDesc ? " desc" : " asc");
            var pagedResult = new PagedSearchResultDto<InstitutionDto>(searchDto);

            var searchText = searchDto.SearchText;

            var query = dbContext.Institutions
                .Search(t => t.Name)
                    .Containing(searchText);

            // If the items per page selected is "All" the underlying value of it is -1, so use the total count of data as page size.
            if (searchDto.PageSize == -1)
            {
                searchDto.PageSize = query.Count();
            }

            var results = await query
                .Skip((searchDto.PageNumber - 1) * searchDto.PageSize)
                .Take(searchDto.PageSize)
                .Select(x =>
                        new InstitutionDto
                        {
                            InstitutionId = x.InstitutionId,
                            Name = x.Name,
                            Address = x.Address,
                            ContactNo = x.ContactNo,
                            EmailAddress = x.EmailAddress,
                            IsActive = x.IsActive
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

        public async Task<PagedSearchResultDto<BloodComponentChecklistListDto>> GetBloodComponentChecklists(PagedSearchDto searchDto)
        {
            if (searchDto == null)
            {
                throw new ArgumentNullException(nameof(PagedSearchDto));
            }

            var sortBy = string.IsNullOrEmpty(searchDto.SortBy) ? "BloodComponentName asc" : searchDto.SortBy + (searchDto.SortDesc ? " desc" : " asc");
            var pagedResult = new PagedSearchResultDto<BloodComponentChecklistListDto>(searchDto);

            var searchText = searchDto.SearchText;

            var query = dbContext.BloodComponentChecklists
                .Include(x => x.BloodComponent)
                .Search(t => t.BloodComponent.ComponentName)
                    .Containing(searchText);

            // If the items per page selected is "All" the underlying value of it is -1, so use the total count of data as page size.
            if (searchDto.PageSize == -1)
            {
                searchDto.PageSize = query.Count();
            }

            var results = await query
                .Skip((searchDto.PageNumber - 1) * searchDto.PageSize)
                .Take(searchDto.PageSize)
                .Select(x =>
                        new BloodComponentChecklistListDto
                        {
                            BloodComponentChecklistId = x.BloodComponentChecklistId,
                            BloodComponentName = x.BloodComponent.ComponentName,
                            ChecklistDescription = x.ChecklistDescription,
                            IsAdult = x.IsAdult,
                            IsActive = x.IsActive
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

        public async Task<PagedSearchResultDto<TestOrderTypeSettingDto>> GetTestOrderTypes(PagedSearchDto searchDto)
        {
            if (searchDto == null)
            {
                throw new ArgumentNullException(nameof(PagedSearchDto));
            }

            var sortBy = string.IsNullOrEmpty(searchDto.SortBy) ? "Code asc" : searchDto.SortBy + (searchDto.SortDesc ? " desc" : " asc");
            var pagedResult = new PagedSearchResultDto<TestOrderTypeSettingDto>(searchDto);

            var searchText = searchDto.SearchText;

            var query = dbContext.TestOrderTypes
                .Search(t => t.Name, t => t.Code)
                    .Containing(searchText);            
            
            // If the items per page selected is "All" the underlying value of it is -1, so use the total count of data as page size.
            if (searchDto.PageSize == -1)
            {
                searchDto.PageSize = query.Count();
            }

            var results = await query
                .Skip((searchDto.PageNumber - 1) * searchDto.PageSize)
                .Take(searchDto.PageSize)
                .Select(x =>
                        new TestOrderTypeSettingDto
                        {
                            TestOrderTypeId = x.TestOrderTypeId,
                            Code =  x.Code,
                            Name = x.Name,
                            Description = x.Description,
                            IsActive = x.IsActive
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

        public async Task<List<ApplicationSettingKeyValuePair>> GetApplicationSettingsByKey(List<string> settingKeys)
        {
            var lookups = await dbContext.ApplicationSettings
                .Where(x => x.IsActive && (settingKeys != null && settingKeys.Any() ? settingKeys.Any(l => l == x.SettingKey) : true))
                .AsNoTracking()
                .ToListAsync();

            var results = lookups.Select(x =>
                new ApplicationSettingKeyValuePair
                {
                    SettingKey = x.SettingKey,
                    SettingValue = x.SettingValue
                }).ToList();

            return results;
        }

        public async Task<InstitutionDto> GetInstitutionById(Guid id)
        {
            var institution = await repository.Institution.FindOneByConditionAsync(x => x.InstitutionId == id);
            if (institution == null)
            {
                throw new RecordNotFoundException("Institution not found.");
            }

            return mapper.Map<InstitutionDto>(institution);
        }

        public async Task<List<BloodComponentDto>> GetBloodComponents()
        {
            var lookups = await dbContext.BloodComponents
                .Where(x => x.IsActive)
                .AsNoTracking()
                .ToListAsync();

            var results = lookups.Select(x =>
                new BloodComponentDto
                {
                    BloodComponentId = x.BloodComponentId,
                    ComponentName = x.ComponentName
                }).ToList();

            return results;
        }

        public async Task<BloodComponentChecklistDto> GetBloodComponentChecklistById(Guid id)
        {
            var checklist = await repository.BloodComponentChecklist.FindOneByConditionAsync(x => x.BloodComponentChecklistId == id);
            if (checklist == null)
            {
                throw new RecordNotFoundException("Blood Component Checklist not found.");
            }

            return mapper.Map<BloodComponentChecklistDto>(checklist);
        }


        public async Task<TestOrderTypeSettingDto> GetTestOrderTypeById(Guid id)
        {
            var checklist = await repository.TestOrderType.FindOneByConditionAsync(x => x.TestOrderTypeId == id);
            if (checklist == null)
            {
                throw new RecordNotFoundException("Test Order Type not found.");
            }

            return mapper.Map<TestOrderTypeSettingDto>(checklist);
        }

        //public async Task<Guid> UpdateLibrariesRoles(RoleDto dto)
        //{
        //    try
        //    {
        //        if (dto == null) throw new ArgumentNullException(nameof(RoleDto));

        //        var librariesRole = await repository.ApplicationSetting.FindOneByConditionAsync(x => x.ApplicationSettingId == dto.ApplicationSettingId);
        //        if (applicationSetting == null) throw new RecordNotFoundException($"Application setting {dto.SettingKey} does not exist.");

        //        var setting = mapper.Map<ApplicationSetting>(dto);
        //        repository.ApplicationSetting.Update(setting);

        //        await repository.SaveAsync();

        //        return applicationSetting.ApplicationSettingId;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        public async Task<Guid> UpdateApplicationSetting(ApplicationSettingDto dto)
        {
            try
            {
                if (dto == null) throw new ArgumentNullException(nameof(ApplicationSettingDto));

                var applicationSetting = await repository.ApplicationSetting.FindOneByConditionAsync(x => x.ApplicationSettingId == dto.ApplicationSettingId);
                if (applicationSetting == null) throw new RecordNotFoundException($"Application setting {dto.SettingKey} does not exist.");

                var setting = mapper.Map<ApplicationSetting>(dto);
                repository.ApplicationSetting.Update(setting);

                await repository.SaveAsync();

                return applicationSetting.ApplicationSettingId;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Guid> UpsertBloodComponentSetting(BloodComponentSettingDto dto)
        {
            try
            {
                if (dto == null) throw new ArgumentNullException(nameof(BloodComponentSettingDto));
                
                var setting = mapper.Map<BloodComponent>(dto);

                if (dto.BloodComponentId.HasValue)
                {
                    var bloodComponent = await repository.BloodComponent.FindOneByConditionAsync(x => x.BloodComponentId == dto.BloodComponentId);
                    if (bloodComponent == null) throw new RecordNotFoundException($"Blood component setting {dto.ComponentName} does not exist.");

                    repository.BloodComponent.Update(setting);
                }
                else
                {
                    var bloodComponent = await repository.BloodComponent.FindOneByConditionAsync(x => x.ComponentName == dto.ComponentName);
                    if (bloodComponent != null) throw new RecordExistException($"Blood component setting {dto.ComponentName} already exist.");

                    repository.BloodComponent.Create(setting);
                }

                await repository.SaveAsync();

                return setting.BloodComponentId;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //public async Task<Guid> UpsertLibrariesRole(RoleDto dto)
        //{
        //    if (dto == null) throw new ArgumentNullException(nameof(dto));

        //    var librariesDto = await repository.Role.FindOneByConditionAsync(
        //        x => x.RoleId == dto.RoleId);


        //    var entity = mapper.Map<Role>(dto);

        //    if (dto.RoleId.HasValue)
        //    {
        //        repository.Role.Update(entity);
        //    }
        //    else
        //    {
        //        repository.Role.Create(entity);
        //    }

        //    await repository.SaveAsync();
        //    return entity.RoleId;
        //}
        public async Task<Guid> UpsertLibrariesRole(RoleDto dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            var existingRole = await repository.Role
                .FindOneByConditionAsync(x => x.RoleId == dto.RoleId);

            var entity = mapper.Map<Role>(dto);

            if (dto.RoleId.HasValue && existingRole != null)
            {
                repository.Role.Update(entity);

                // --- Update UserRoleScreeningAccess ---
                // Remove existing accesses
                var existingAccesses = await repository.UserRoleScreeningAccess
                    .FindByConditionAsync(x => x.RoleId == dto.RoleId.Value);
                repository.UserRoleScreeningAccess.RemoveRange(existingAccesses);

                // Add new accesses
                var newAccesses = dto.UserRoleAccesses?.Select(access => new UserRoleScreeningAccess
                {
                    RoleId = entity.RoleId,
                    ScreeningTabName = access.ScreeningTabName
                }).ToList();

                if (newAccesses?.Any() == true)
                {
                     repository.UserRoleScreeningAccess.AddRange(newAccesses);
                }
            }
            else
            {
                // Create new Role
                repository.Role.Create(entity);

                // Accesses will be added after save when RoleId is generated
            }

            await repository.SaveAsync();

            // In case of new insert, insert accesses after RoleId is available
            if (!dto.RoleId.HasValue && dto.UserRoleAccesses?.Any() == true)
            {
                var newAccesses = dto.UserRoleAccesses.Select(access => new UserRoleScreeningAccess
                {
                    RoleId = entity.RoleId,
                    ScreeningTabName = access.ScreeningTabName
                }).ToList();

                repository.UserRoleScreeningAccess.AddRange(newAccesses);
                await repository.SaveAsync();
            }

            return entity.RoleId;
        }


        public async Task<int> UpsertLibrariesQuestionnare(MedicalQuestionnaireDto dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));


            var entity = mapper.Map<MedicalQuestionnaire>(dto);

            if (dto.MedicalQuestionnaireId > 0)
            {
                repository.MedicalQuestionnare.Update(entity);
            }
            else
            {
                repository.MedicalQuestionnare.Create(entity);
            }

            await repository.SaveAsync();
            return entity.MedicalQuestionnaireId;
        }



        public async Task<Guid> UpsertInstitution(InstitutionDto dto)
        {
            try
            {
                if (dto == null) throw new ArgumentNullException(nameof(InstitutionDto));

                var setting = mapper.Map<Institution>(dto);

                if (dto.InstitutionId.HasValue)
                {
                    var institution = await repository.Institution.FindOneByConditionAsync(x => x.InstitutionId == dto.InstitutionId);
                    if (institution == null) throw new RecordNotFoundException($"Institution setting {dto.Name} does not exist.");

                    repository.Institution.Update(setting);
                }
                else
                {
                    var institution = await repository.Institution.FindOneByConditionAsync(x => x.Name == dto.Name);
                    if (institution != null) throw new RecordExistException($"Institution setting {dto.Name} already exist.");

                    repository.Institution.Create(setting);
                }

                await repository.SaveAsync();

                return setting.InstitutionId;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Guid> UpsertBloodComponentChecklist(BloodComponentChecklistDto dto)
        {
            try
            {
                if (dto == null) throw new ArgumentNullException(nameof(BloodComponentChecklistDto));

                var checklist = mapper.Map<BloodComponentChecklist>(dto);

                if (dto.BloodComponentChecklistId.HasValue)
                {
                    var institution = await repository.BloodComponentChecklist.FindOneByConditionAsync(x => x.BloodComponentChecklistId == dto.BloodComponentChecklistId);
                    if (institution == null) throw new RecordNotFoundException($"Checklist ID {dto.BloodComponentChecklistId} does not exist.");

                    repository.BloodComponentChecklist.Update(checklist);
                }
                else
                {
                    repository.BloodComponentChecklist.Create(checklist);
                }

                await repository.SaveAsync();

                return checklist.BloodComponentChecklistId;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Guid> UpsertTestOrderType(TestOrderTypeSettingDto dto)
        {
            try
            {
                if (dto == null) throw new ArgumentNullException(nameof(TestOrderTypeSettingDto));

                var orderType = mapper.Map<TestOrderType>(dto);

                if (dto.TestOrderTypeId.HasValue)
                {
                    var testOrderType = await repository.TestOrderType.FindOneByConditionAsync(x => x.TestOrderTypeId == dto.TestOrderTypeId);
                    if (testOrderType == null) throw new RecordNotFoundException($"Test Order Type with {dto.Code} and name {dto.Name} does not exist.");

                    repository.TestOrderType.Update(orderType);
                }
                else
                {
                    var testOrderType = await repository.TestOrderType.FindOneByConditionAsync(x => x.Name == dto.Name);
                    if (testOrderType != null) throw new RecordExistException($"Test Order Type {dto.Name} already exist.");

                    repository.TestOrderType.Create(orderType);
                }

                await repository.SaveAsync();

                return orderType.TestOrderTypeId;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
