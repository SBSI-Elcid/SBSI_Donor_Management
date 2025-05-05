namespace BBIS.Domain.Models
{
    public class LookupOption
    {
        public int LookupOptionId { get; set; }
        public int LookupId { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public bool IsActive { get; set; }

        public virtual Lookup Lookup { get; set; }
    }
}
