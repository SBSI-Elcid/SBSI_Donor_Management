namespace BBIS.Application.DTOs.DonorTestOrder
{
    public class DonorTestOrderDto
    {
        public Guid DonorTestOrderId { get; set; }
        public Guid DonorTransactionId { get; set; }
        public bool TestCompleted { get; set; }
        public string FinalBloodType { get; set; }
        public string BloodRh { get; set; }

        public List<DonorTestOrderTypesDto> TestTypes { get; set; }
    }
}
