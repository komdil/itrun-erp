using MediatR;
using Warehouse.Queries;

namespace Warehouse.Contracts.Product
{
    public record GetProductsQuery : PagedQuery<SingleProductResponse>
    {
        public string Category { get; set; }
        public string Warehouse { get; set; }
        public string Manufacturer { get; set; }
    }
}
