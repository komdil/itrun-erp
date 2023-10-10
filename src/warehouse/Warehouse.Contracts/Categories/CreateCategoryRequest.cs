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
        //public string Slug { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ParentCategory { get; set; }
        public string SubCategories { get; set; }
    }
}
