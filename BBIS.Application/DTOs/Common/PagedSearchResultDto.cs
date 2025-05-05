namespace BBIS.Application.DTOs.Common
{
    public class PagedSearchResultDto<T> 
    {
        public int TotalCount { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public List<T> Results { get; set; }

        public PagedSearchResultDto() { }

        public PagedSearchResultDto(PagedSearchDto searchDto)
        {
            CurrentPage = searchDto.PageNumber;
            PageSize = searchDto.PageSize;
            Results = new List<T>();
            TotalCount = 0;
        }
    }
}
