using MediatR;

namespace Warehouse.Queries
{
    public abstract record PagedQuery<T> : IRequest<List<T>>
    {
        public int Skip { get; set; } = 0;

        public int PageSize { get; set; } = 20;
    }
}
