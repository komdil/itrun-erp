using MediatR;

namespace Warehouse.Queries
{
    public abstract record PagedQuery<T> : IRequest<List<T>>
    {
        public int StartIndex { get; set; } = 0;

        public int EndIndex { get; set; } = 20;
    }
}
