
using Microsoft.EntityFrameworkCore;


namespace Loza.Application.Helpers
{
    public  class PaginationHelper<T> : List<T>
    {
        public int TotalCount { get; private set; }

        public int CurrentPage { get; private set; }

        public int PageSize { get; private set; }

        public int TotalPages { get; private set; }

        public bool HasNext => CurrentPage < TotalPages;

        public bool HasPrevious => CurrentPage > 1;


        private PaginationHelper(List<T> items, int pageSize, int pageNumber, int count)
        {
            PageSize = pageSize;
            TotalCount = count;
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            AddRange(items);

        }

        public static async Task<PaginationHelper<T>> CreatePaginatedList(IQueryable<T> source, int pageSize, int pageNumber)
        {
            var count = source.Count();

            var list = await source.Skip(pageSize * (pageNumber - 1))
                                   .Take(pageSize).ToListAsync();

            return new PaginationHelper<T>(list, pageSize, pageNumber, count);
        }

    }
}
