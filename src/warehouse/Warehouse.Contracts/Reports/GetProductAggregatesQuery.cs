using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse.Contracts.Reports
{
    public record GetProductAggregatesQuery : IRequest<ProductAggregatesResponse>
    {
    }
}
