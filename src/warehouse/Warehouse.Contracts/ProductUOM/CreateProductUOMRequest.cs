using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse.Contracts.ProductUOM
{
    public record CreateProductUOMRequest : IRequest<SingleProductUomResponse>
    {
        public string Name { get; init; }
        public string Details { get; init; }
        public string Abbreviation { get; init; }
    }
}
