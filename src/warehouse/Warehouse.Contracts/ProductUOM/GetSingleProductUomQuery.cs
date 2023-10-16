using MediatR;
using System;

namespace Warehouse.Contracts.ProductUOM
{
    public record GetSingleProductUomQuery : IRequest<SingleProductUomResponse>
    {
        public Guid ProductUomId { get; set; }
    }
}
