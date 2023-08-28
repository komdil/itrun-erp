using MediatR;

namespace Contracts.Product
{
    public record GetProductListQueryRequest : IRequest<GetProductListQueryResponse>
    {
        public Guid ProductUomId { get; set; }
        public string Category { get; set; }
        public string Manufacturer { get; set; }
    }
}
