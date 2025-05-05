namespace BBIS.Application.DTOs.Requisition
{
    public class TransfusionDto : TransfusionViewDto
    {
        public Guid ReservationItemId { get; set; }
        public Guid ReservationId { get; set; }
    }
}