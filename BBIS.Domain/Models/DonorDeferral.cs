namespace BBIS.Domain.Models
{
    public class DonorDeferral
    {
        public Guid DonorDeferralId { get; set; }
        public Guid DonorTransactionId { get; set; }
        public string DeferralStatus { get; set; }
        public string Remarks { get; set; }

        public virtual DonorTransaction DonorTransaction { get; set; }
    }
}
