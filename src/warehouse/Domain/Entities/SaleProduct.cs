
namespace Domain.Entities
{
	public class SaleProduct
	{
		public int Id { get; set; }
		public string ProductName { get; set; }
		public string ProductUom { get; set; }
		public Guid Warehouse { get; set; }
		public decimal Price { get; set; }
		public int Quantity { get; set; }
		public decimal TotalPrice { get; set; }
		public DateTime Date { get; set; }
		public string Comment { get; set; }
	}
}
