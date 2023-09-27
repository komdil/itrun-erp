using MediatR;
using System.Collections.Generic;

namespace Warehouse.Contracts.ProductUOM
{
    public record GetProductsUomQuery : IRequest<List<CreatProductUOMResponse>>
    {
        public string Name { get; set; }
        public string Abbreviation { get; set; }
    }
}
