using Warehouse.Queries;

namespace Warehouse.Contracts.Categories
{
    public record GetCategoryQuery : PagedQuery<SingleCategoryResponse>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
