using MediatR;

namespace Warehouse.Contracts.SellProduct
{
    public record DeleteProductPurchaseRequest : IRequest
    {
        public Guid Id { get; set; }
    }
}
