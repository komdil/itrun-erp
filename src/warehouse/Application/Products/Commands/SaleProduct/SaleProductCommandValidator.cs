using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
