using MediatR;

namespace Contracts.Product
{
    public record GetProductDetailsQueryRequest : IRequest<GetProductDetailsQueryResponse>
    {
        public Guid ProductId { get; set; }
    }
}
