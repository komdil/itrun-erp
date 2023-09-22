using MediatR;

namespace Warehouse.Contracts.Product
{
    public record GetProductsQuery : IRequest<List<SingleProductResponse>>
    {
        public string Category { get; set; }
        public string Manufacturer { get; set; }
    }
}
