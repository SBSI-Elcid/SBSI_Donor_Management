namespace BBIS.Application.DTOs.Lookup
{
    public class LookupDto
    {
        public int LookupId { get; set; }
        public string LookupKey { get; set; }
        public string Description { get; set; }

        public List<LookupOptionDto> LookupOptions { get; set; }
    }
}
