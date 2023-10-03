using Warehouse.Queries;

namespace Warehouse.Contracts.Warehouse
{
    public record GetWarehouseQuery : PagedQuery<SingleWarehouseResponse>
    {
        public string Name { get; set; }
        public string Details { get; set; }
        public string Location { get; set; }
    }
}
