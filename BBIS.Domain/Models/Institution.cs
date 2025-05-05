namespace BBIS.Domain.Models
{
    public class Institution
    {
        public Guid InstitutionId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string ContactNo { get; set; }
        public string EmailAddress { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<InventorySource> InventorySources { get; set; }
    }
}
