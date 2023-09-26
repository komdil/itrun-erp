using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse.Contracts.Warehouse
{
    public record GetSingleWarehouseQuery : IRequest<SingleWarehouseResponse>
    {
        public Guid Id { get; set; }
    }
}
