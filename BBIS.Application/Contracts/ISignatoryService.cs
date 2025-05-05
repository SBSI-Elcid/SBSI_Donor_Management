using BBIS.Application.DTOs.Common;
using BBIS.Application.DTOs.Signatory;

namespace BBIS.Application.Contracts
{
    public interface ISignatoryService
    {
        Task<Guid> UpsertSignatory(SignatoryDto dto);

        Task DeleteSignatory(Guid id);

        Task<SignatoryDto> GetSignatory(Guid id);

        Task<PagedSearchResultDto<SignatoryViewDto>> GetPagedListAsync(PagedSearchDto searchDto);

        Task<List<SignatoryOptionDto>> GetSignatoryOptions();
    }
}
