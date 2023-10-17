using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Warehouse.Contracts.Categories
{
    public class UpdateCategoryRequest : IRequest<SingleCategoryResponse>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid? ParentCategoryId { get; set; }
        public List<Guid> SubCategories { get; set; } = new List<Guid>();
    }
}
