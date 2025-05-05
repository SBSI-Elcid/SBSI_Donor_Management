namespace BBIS.Domain.Models
{
    public class DonorTestOrder
    {
        public Guid DonorTestOrderId { get; set; }
        public Guid DonorTransactionId { get; set; }
        public bool TestCompleted { get; set; }

        public DateTime DateCreated { get; set; }
        public Guid CreatedById { get; set; }

        public virtual DonorTransaction DonorTransaction { get; set; }
        public virtual ICollection<DonorTestOrderTestType> DonorTestOrderTestTypes { get; set; }
    }
}
