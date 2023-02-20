using Tickets.Application.Models;

namespace Tickets.Application.Features.Shared.Queries
{
    public class PaginationBaseQuery
    {
        public string? Search { get; set; }

        public string? Sort { get; set; }

        public int PageIndex { get; set; } = PaginationParams.PageIndex;

        private int _pageSize = PaginationParams.PageSize;

        private const int MaxPageSize = PaginationParams.MaxPageSize;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }

    }
}
