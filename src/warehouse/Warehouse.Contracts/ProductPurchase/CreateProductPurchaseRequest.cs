using MediatR;
using Warehouse.Contracts.Dtos;

namespace Warehouse.Contracts.ProductPurchase
{
    public record CreateProductPurchaseRequest : IRequest<SingleProductPurchaseResponse>
    {
        public string ProductName { get; init; }
        public string ProductUom { get; init; }
        public decimal Price { get; init; }
        public WareHouse WareHouse { get; init; }
        public int Quantity { get; init; }
        public string VendorName { get; init; }
        public DateTime Date { get; init; }
        public string Comment { get; init; }
    }
}
