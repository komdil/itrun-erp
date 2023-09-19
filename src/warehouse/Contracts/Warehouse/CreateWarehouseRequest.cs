using MediatR;

namespace Contracts.Warehouse
{
    public record CreateWarehouseRequest : IRequest<CreateWarehouseResponse>
    {
        public string Name { get; set; }
        public string Details { get; set; }
        public string Location { get; set; }
    }
}


