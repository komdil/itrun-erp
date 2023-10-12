namespace Warehouse.Contracts.ProductPurchase
{
    public record SingleProductPurchaseResponse
    {
        public Guid Id { get; init; }
        public string ProductName { get; init; }
        public string ProductUom { get; init; }
        public decimal Price { get; init; }
        public Guid WareHouseId { get; init; }
        public int Quantity { get; init; }
        public string VendorName { get; init; }
        public decimal TotalPrice { get; init; }
        public DateTime Date { get; init; }
        public string Comment { get; init; }
    }
}
