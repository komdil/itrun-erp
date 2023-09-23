using System.ComponentModel.DataAnnotations;

namespace Application.Products.Commands.SaleProduct
{
	public class SaleProductCommandValidator
	{
		[Required]
		public string ProductName { get; set; }

		[Range(1, int.MaxValue)]
		public int Quantity { get; set; }

	}
}
