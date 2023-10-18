namespace Warehouse.Contracts.ProductPurchase
{
    public record SingleProductPurchaseResponse
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; }
        public string ProductUom { get; set; }
        public decimal Price { get; set; }
        public Guid WareHouseId { get; set; }
        public int Quantity { get; set; }
        public string VendorName { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime Date { get; set; }
        public string Comment { get; set; }
    }
}
