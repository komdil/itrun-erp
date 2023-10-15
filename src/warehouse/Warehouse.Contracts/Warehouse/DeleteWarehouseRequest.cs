using MediatR;

namespace Warehouse.Contracts.Warehouse
{
	public record DeleteWarehouseRequest : IRequest
    {
        public Guid Id { get; set; }
    }
}
