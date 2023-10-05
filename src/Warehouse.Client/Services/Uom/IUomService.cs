namespace Warehouse.Client.Services.Uom
{
    public interface IUomService
    {
        Task<List<string>> GetAsync();
    }
}
