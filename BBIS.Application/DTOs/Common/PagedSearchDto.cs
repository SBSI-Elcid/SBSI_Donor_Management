namespace BBIS.Application.DTOs.Common
{
    public class PagedSearchDto
    {
        public string SearchText { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string SortBy { get; set; }
        public bool SortDesc { get; set; }
    }
}
