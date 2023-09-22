using MediatR;

namespace Warehouse.Contracts.Product
{
    public record GetSingleProductQuery : IRequest<SingleProductResponse>
    {
        public Guid ProductId { get; set; }
    }
}
