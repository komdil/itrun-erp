using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Warehouse.Contracts.Warehouse
{
    public record CreateWarehouseRequest : IRequest<CreateWarehouseResponse>
    {
        [Required]
        public string Name { get; set; }
        public string Details { get; set; }
        public string Location { get; set; }
    }
}
