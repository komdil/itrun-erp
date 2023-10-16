using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Warehouse.Contracts.Categories
{
    public record GetSingleCategoryQuery : IRequest<SingleCategoryResponse>
    {
        public Guid Id { get; set; }
    }
}
