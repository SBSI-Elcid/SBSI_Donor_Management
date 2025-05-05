namespace BBIS.Domain.Models
{
    public class InventorySource
    {
        public Guid InventorySourceId { get; set; }
        public Guid? DonorTranctionId { get; set; }
        public bool IsExternalSource { get; set; }
        public Guid? InstitutionId { get; set; }

        public virtual ICollection<InventoryItem> InventoryItems { get; set; }

        public virtual Institution Institution { get; set; }
        public virtual DonorTransaction DonorTransaction { get; set; }
    }
}
