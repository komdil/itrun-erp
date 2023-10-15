using Warehouse.Contracts.SellProduct;
using Warehouse.Queries;

namespace Warehouse.Contracts.Warehouse
{
    public record GetSaleProductsQuery : PagedQuery<SingleProductSellResponse>
    {
		public string ProductName { get; init; }
		public string ProductUom { get; init; }
		public Guid WareHouseId { get; init; }
		public decimal Price { get; init; }
		public int Quantity { get; init; }
		public decimal TotalPrice { get; init; }
		public DateTime Date { get; init; }
		public string Comment { get; init; }

	}
}
