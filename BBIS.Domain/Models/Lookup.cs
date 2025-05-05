namespace BBIS.Domain.Models
{
    public class Lookup
    {
        public int LookupId { get; set; }
        public string LookupKey { get; set; }
        public string Description { get; set; }

        public bool IsActive { get; set; }

        public ICollection<LookupOption> LookupOptions { get; set; }
    }
}
