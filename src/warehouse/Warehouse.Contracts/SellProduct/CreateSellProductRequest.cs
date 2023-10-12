using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse.Contracts.SellProduct
{
	public class CreateSellProductRequest : IRequest<SingleProductSellResponse>
    {
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
