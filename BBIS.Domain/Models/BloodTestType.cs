namespace BBIS.Domain.Models
{
    public class BloodTestType
    {
        public Guid BloodTestTypeId { get; set; }
        public string TypeName { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<DonorTestOrderTestType> DonorTestOrderTestTypes { get; set; }
    }
}
