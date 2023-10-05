namespace Warehouse.Client.Services.Categories
{
    public interface ICategoryService
    {
        Task<List<string>> GetAsync();
    }
}
