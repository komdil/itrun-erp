using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Warehouse.Contracts.Warehouse;

namespace Warehouse.Contracts.Categories
{
    public record CreateCategoryRequest : IRequest<SingleCategoryResponse>
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid ParentCategoryId { get; set; }
    }
}
