using MediatR;

namespace Warehouse.Contracts.Product
{
    public record GetProductDetailsQueryRequest : IRequest<GetProductDetailsQueryResponse>
    {
        public Guid ProductId { get; set; }
    }
}
