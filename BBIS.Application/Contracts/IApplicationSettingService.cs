using BBIS.Application.DTOs.ApplicationSetting;
using BBIS.Application.DTOs.Common;
using BBIS.Application.DTOs.DonorRegistration;

namespace BBIS.Application.Contracts
{
    public interface IApplicationSettingService
    {
        Task<PagedSearchResultDto<RoleDto>> GetRoleSettings(PagedSearchDto searchDto);
        //GetAllUserRoleAccess
        Task<List<RoleDto>> GetAllRoles();
        Task<PagedSearchResultDto<MedicalQuestionnaireDto>> GetQuestionnaireSettings(PagedSearchDto searchDto);
        Task<PagedSearchResultDto<ApplicationSettingDto>> GetApplicationSettings(PagedSearchDto searchDto);
        Task<PagedSearchResultDto<BloodComponentSettingDto>> GetBloodComponentSettings(PagedSearchDto searchDto);
        Task<PagedSearchResultDto<InstitutionDto>> GetInstitutions(PagedSearchDto searchDto);
        Task<PagedSearchResultDto<BloodComponentChecklistListDto>> GetBloodComponentChecklists(PagedSearchDto searchDto);
        Task<PagedSearchResultDto<TestOrderTypeSettingDto>> GetTestOrderTypes(PagedSearchDto searchDto);
        Task<List<ApplicationSettingKeyValuePair>> GetApplicationSettingsByKey(List<string> settingKeys);
        Task<InstitutionDto> GetInstitutionById(Guid id);
        Task<List<BloodComponentDto>> GetBloodComponents();
        Task<BloodComponentChecklistDto> GetBloodComponentChecklistById(Guid id);
        Task<TestOrderTypeSettingDto> GetTestOrderTypeById(Guid id);
        Task<Guid> UpdateApplicationSetting(ApplicationSettingDto dto);
        Task<Guid> UpsertBloodComponentSetting(BloodComponentSettingDto dto);
        Task<Guid> UpsertLibrariesRole(RoleDto dto);
        Task<int> UpsertLibrariesQuestionnare(MedicalQuestionnaireDto dto);
        Task<Guid> UpsertInstitution(InstitutionDto dto);
        Task<Guid> UpsertBloodComponentChecklist(BloodComponentChecklistDto dto);
        Task<Guid> UpsertTestOrderType(TestOrderTypeSettingDto dto);
    }
}
