namespace Loza.API.Contracts.Shared.Requests
{
    public class QueriesRequest
    {
        public string? Category { get; set; }
        public string? SearchBy { get; set; }
        public int PageNumber { get; set; } = 1;
        const int maxPageSize = 20;
        private int _pageSize { get; set; } = 10;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value < maxPageSize) ? value : maxPageSize;
        }

    }
}
