using BBIS.Application.DTOs.Common;
using BBIS.Application.DTOs.DonorRegistration;

namespace BBIS.Application.Contracts
{
    public interface IDonorRegistrationService
    {
        Task<List<MedicalQuestionnaireDto>> GetMedicalQuestionnaires();

        Task<PagedSearchResultDto<RegisteredDonorDto>> GetRegisteredDonors(PagedSearchDto searchDto);

        Task<RegisteredDonorInfoDto> GetRegisteredDonorInfo(Guid regId);

        Task<string> RegisterDonor(RegisterDonorDto dto);

        Task<VerifyDonorResultDto> VerifyDonor(VerifyDonorDto dto);

        Task<Guid> UpdateDonorAndCreateTransaction(UpdateDonorInfoDto dto, Guid userId, bool isOffline);
    }
}
