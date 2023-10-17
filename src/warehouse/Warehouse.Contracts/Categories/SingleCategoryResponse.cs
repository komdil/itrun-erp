namespace Warehouse.Contracts.Categories
{
    public record SingleCategoryResponse
    {
        public string Slug { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ParentCategory { get; set; }
        public string SubCategories { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
