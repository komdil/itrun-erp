using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ProductUOM Uom { get; set; }
        public string Category {get; set;}
        public string Manufacturer {get; set;}
        public decimal Price { get; set;}
        public string Description {get; set;}
        [ConcurrencyCheck]
        public int Quantity { get; set;}
    }
}