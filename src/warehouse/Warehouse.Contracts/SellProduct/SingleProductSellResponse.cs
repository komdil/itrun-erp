namespace Warehouse.Contracts.SellProduct
{
	public class SingleProductSellResponse
	{
		public Guid Id { get; init; }
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
