namespace BBIS.Application.DTOs.DonorScreening
{
    public class DonorDeferralDto
    {
        public Guid DonorTransactionId { get; set; }
        public string DeferralStatus { get; set; }
        public string Remarks { get; set; }
    }
}
