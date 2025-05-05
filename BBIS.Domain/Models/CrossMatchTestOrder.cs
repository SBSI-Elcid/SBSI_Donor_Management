namespace BBIS.Domain.Models
{
    public class CrossMatchTestOrder
    {
        public Guid CrossMatchTestOrderId { get; set; }
        public Guid DonorTransactionId { get; set; }
        public Guid TestOrderId { get; set; }
        public Guid BloodComponentId { get; set; }

        public string DonorUnitSerialNumber { get; set; }
        public string CrossMatchType { get; set; }
        public string Result { get; set; }
        public string Source { get; set; }
        public string LISS_AGH { get; set; }
        public DateTime CollectionDate { get; set; }
        public DateTime ExpiryDate { get; set; }

        public virtual BloodComponent BloodComponent { get; set; }
        public virtual DonorTransaction DonorTransaction { get; set; }
        public virtual TestOrder TestOrder { get; set; }
    }
}
