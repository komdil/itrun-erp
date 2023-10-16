
namespace Domain.Entities
{
	public class SaleProduct
	{
		public Guid Id { get; set; }
		public string ProductName { get; set; }
		public string ProductUom { get; set; }
		public Guid WareHouseId { get; set; }
		public decimal Price { get; set; }
		public int Quantity { get; set; }
		public decimal TotalPrice { get; set; }
		public DateTime Date { get; set; }
		public string Comment { get; set; }
		
	}
}
