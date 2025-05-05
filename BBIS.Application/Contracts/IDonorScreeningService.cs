using BBIS.Application.DTOs.Common;
using BBIS.Application.DTOs.DonorScreening;
namespace BBIS.Application.Contracts
{
    public interface IDonorScreeningService
    {
        Task<PagedSearchResultDto<DonorListDto>> GetDonors(DonorPagedSearchDto searchDto, List<string> roles);

        Task<DonorInitialScreeningDto> GetInitialScreeningInfo(Guid transactionId);

        Task<List<DonorRecentDonationDto>> GetRecentDonations(Guid id);

        Task<DonorPhysicalExaminationDto> GetPhysicalExamInfo(Guid transactionId);

        Task<DonorBloodCollectionDto> GetBloodCollectionInfo(Guid transactionId);
        
        Task<Guid> CreateUpdateDonorInitialScreening(DonorInitialScreeningDto dto, Guid userId);

        Task<Guid> CreateUpdateDonorPhysicalExamination(DonorPhysicalExaminationDto dto, Guid userId);

        Task<Guid> CreateUpdateDonorBloodCollection(DonorBloodCollectionDto dto, Guid userId);

        
    }
}
