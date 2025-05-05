using BBIS.Application.DTOs.Common;
using BBIS.Application.DTOs.DonorTestOrder;

namespace BBIS.Application.Contracts
{
    public interface IDonorTestOrderService
    {
        Task<List<BloodTestTypeDto>> GetBloodTestTypes();

        Task<PagedSearchResultDto<DonorTestOrderListDto>> GetDonorTestOrders(DonorTestOrderPagedSearchDto searchDto);

        Task<DonorTestOrderDto> GetDonorTestOrder(Guid transactionId);

        Task<Guid> CreateDonorTestOrder(CreateDonorTestOrderDto dto, Guid userId);

        Task<bool> UpdateDonorTestOrder(DonorTestOrderDto dto, Guid userId);
    }
}
