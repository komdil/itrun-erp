using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse.Contracts.Categories
{
    public record CreateCategoryResponse
    {
        public string Slug { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ParentCategory { get; set; }
        public string SubCategories { get; set; }
    }
}
