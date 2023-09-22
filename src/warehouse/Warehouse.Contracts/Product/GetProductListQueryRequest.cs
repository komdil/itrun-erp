using MediatR;

namespace Warehouse.Contracts.Product
{
    public record GetProductListQueryRequest : IRequest<ProductListResponse>
    {
        public string Category { get; set; }
        public string Manufacturer { get; set; }
    }
}
