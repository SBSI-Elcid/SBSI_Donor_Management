using BBIS.Application.DTOs.Common;
using BBIS.Application.DTOs.TestOrder;

namespace BBIS.Application.Contracts
{
    public interface ITestOrderService
    {
        Task<Guid> CreateTestOrder(CreateTestOrderDto dto, Guid userId);

        Task<bool> UpdateTestOrder(UpdateTestOrderDto dto, Guid userId);

        Task DeleteTestOrder(Guid id);

        Task<TestOrderDto> GetTestOrder(Guid testOrderGroupId);

        Task<List<TestOrderTypeDto>> GetTestOrderTypes();

        Task<List<TypeAheadResultDto>> SearchDonors(string searchText, string bt);

        Task<List<TypeAheadResultDto>> SearchPatients(string searchText);

        Task<PagedSearchResultDto<RequestedTestOrderListDto>> SearchTestOrders(RequestedTestOrderPagedSearchDto searchDto);

        Task<bool> GetTestOrderStatus(Guid reservationId);

        //Task<List<TestOrderToLinkDto>> GetTestOrdersToLink(Guid patientId);

        //Task LinkReservation(TestOrderLinkUpdateDto dto);

    }
}
