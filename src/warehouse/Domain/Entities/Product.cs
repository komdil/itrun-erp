using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public Guid UomId { get; set; }
        public virtual ProductUOM Uom { get; set; }

        public Guid CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public string Manufacturer { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public Guid WarehouseId { get; set; }
        public virtual WareHouse Warehouse { get; set; }

        [ConcurrencyCheck]
        public int Quantity { get; set; }
    }
}