namespace BBIS.Application.DTOs.TestOrder
{
    public class CreateTestOrderDto
    {
        public Guid PatientId { get; set; }
        public Guid ReservationId { get; set; }

        public List<Guid> TestOrders { get; set; }
        public List<CreateCrossMatchOrderDto> CrossMatchTestOrders { get; set; }
    }
        
}
