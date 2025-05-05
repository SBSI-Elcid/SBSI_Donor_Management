using BBIS.Application.DTOs.Common;
using BBIS.Application.DTOs.Requisition;
using BBIS.Application.DTOs.TestOrder;

namespace BBIS.Application.Contracts
{
    public interface IRequisitionService
    {
        Task<List<CheckListOptionDto>> GetReservationChecklist();

        Task<Guid> CreateReservation(ReservationDto dto, Guid userId);

        Task<Guid> UpdateReservation(UpdateReservationDto dto, Guid userId);

        Task<PagedSearchResultDto<ReservationListingDto>> GetReservations(RequisitionPagedSearchDto searchDto);

        Task<TransfusionViewDetailsDto> GetTransfusionDetails(Guid reservationId);

        Task<Guid> UpsertTransfusionDetails(List<TransfusionDto> transfusionDto);

        Task<List<GroupCrossMatchOrderDto>> GetReservationMatchingDonors(Guid reservationId);
    }
}
