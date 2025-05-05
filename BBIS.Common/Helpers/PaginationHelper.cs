namespace BBIS.Common.Helpers
{
    public static class PaginationHelper<T>
    {
        public static List<T> GetPageList(IQueryable<T> query, int pageSize, int pageNumber)
        {
            if (pageSize <= 0) return query.ToList();

            var skip = (pageNumber - 1) * pageSize;
            return query.Skip(skip).Take(pageSize).ToList();
        }
    }
}
