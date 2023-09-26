namespace Warehouse.Contracts.Warehouse
{
    public record SingleWarehouseResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }
        public string Location { get; set; }
    }
}
