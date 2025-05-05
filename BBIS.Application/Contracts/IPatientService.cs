using BBIS.Application.DTOs.Common;
using BBIS.Application.DTOs.Patient;

namespace BBIS.Application.Contracts
{
    public interface IPatientService
    {
        Task<PatientResult> CreatePatient(PatientDto dto, Guid userId);
        Task<PatientResult> UpdatePatient(PatientDto dto, Guid userId);

        Task<PagedSearchResultDto<PatientViewDto>> GetPagedListAsync(PatientPagedSearchDto searchDto);
        Task<PatientDto> GetPatientById(Guid id);
    }
}
