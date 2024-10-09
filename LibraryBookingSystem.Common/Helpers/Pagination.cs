namespace LibraryBookingSystem.Common.Helpers
{
    public class Pagination<T>
    {
        public int PageIndex { get;  set; }
        public int TotalPages { get; set; }
        public List<T> Result { get; set; }
        public int TotalCount { get; set; }

        public Pagination(List<T> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            Result = items;
            TotalCount = count;
        }

        public bool HasPreviousPage => (PageIndex > 1);

        public bool HasNextPage => (PageIndex < TotalPages);

        public static async Task<Pagination<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize)
        {
            var count =  source.Count();
            var items =  source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            var foo = new Pagination<T>(items, count, pageIndex, pageSize);
            return new Pagination<T>(items, count, pageIndex, pageSize);
        }

    }
}