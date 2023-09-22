namespace Warehouse.Contracts.Product
{
    public record GetProductListQueryResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ProductUom { get; set; }
        public string Category { get; set; }
        public string Manufacturer { get; set; }
    }
}
