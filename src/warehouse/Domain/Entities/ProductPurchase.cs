using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ProductPurchase
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; }
        public string ProductUom { get; set; }
        public decimal Price { get; set; }
        public WareHouse WareHouse { get; set; }
        public int Quantity { get; set; }
        public string VendorName { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime Date { get; set; }
        public string Comment { get; set; }
    }
}
