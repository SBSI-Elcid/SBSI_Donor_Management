namespace BBIS.Application.DTOs.DonorTestOrder
{
    public class CreateDonorTestOrderDto
    {
        public Guid DonorTransactionId { get; set; }
        public List<Guid> TestTypes { get; set; }
    }
}
