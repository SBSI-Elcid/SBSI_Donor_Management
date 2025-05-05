namespace BBIS.Domain.Models
{
    public class DonorTestOrderTestType
    {
        public Guid DonorTestOrderTestTypeId { get; set; }
        public Guid DonorTestOrderId { get; set; }
        public Guid BloodTestTypeId { get; set; }
        public bool IsReactive { get; set; }
        public string Remarks { get; set; }

        public DateTime? DateUpdated { get; set; }
        public Guid UpdatedById { get; set; }

        public virtual BloodTestType BloodTestType { get; set; }
        public virtual DonorTestOrder DonorTestOrder { get; set; }
    }
}
