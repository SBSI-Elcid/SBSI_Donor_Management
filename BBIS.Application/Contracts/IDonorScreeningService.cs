using BBIS.Application.DTOs.Common;
using BBIS.Application.DTOs.DonorRegistration;
using BBIS.Application.DTOs.DonorScreening;
namespace BBIS.Application.Contracts
{
    public interface IDonorScreeningService
    {
        Task<PagedSearchResultDto<DonorListDto>> GetDonors(DonorPagedSearchDto searchDto, List<string> roles);

        Task<DonorInitialScreeningDto> GetInitialScreeningInfo(Guid transactionId);
        Task<DonorVitalSignsDto> GetDonorVitalSignsInfo(Guid transactionId);

        Task<List<DonorBloodBagIssuanceDto>> GetDonorBloodBagIssuance(Guid transactionId);

        Task<DonorCounselingDto> GetDonorCounselingInfo(Guid transactionId);

        Task<List<DonorRecentDonationDto>> GetRecentDonations(Guid id);

        Task<DonorPhysicalExaminationDto> GetPhysicalExamInfo(Guid transactionId);

        Task<DonorBloodCollectionDto> GetBloodCollectionInfo(Guid transactionId);
        
        Task<Guid> CreateUpdateDonorInitialScreening(DonorInitialScreeningDto dto, Guid userId);
        Task<Guid> CreateUpdateDonorVitalSigns(DonorVitalSignsDto dto, Guid userId);
        Task<Guid> CreateUpdateDonorBloodBagIssuance(DonorBloodBagIssuanceDto dto, Guid userId);
        Task<Guid> UpdateDonorTransactionForBloodCollection(RegisteredDonorDto dto);

        Task<Guid> CreateUpdateDonorPhysicalExamination(DonorPhysicalExaminationDto dto, Guid userId);

        Task<Guid> CreateUpdateDonorCounseling(DonorCounselingDto dto, Guid userId);

        Task<Guid> CreateUpdateDonorBloodCollection(DonorBloodCollectionDto dto, Guid userId);

        
    }
}
