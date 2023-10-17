using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Warehouse.Contracts.Categories
{    
    public record DeleteCategoryRequest : IRequest
    {
        public string Name { get; set; }
    }
}
