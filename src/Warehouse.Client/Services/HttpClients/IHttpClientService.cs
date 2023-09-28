namespace Warehouse.Client.Services.HttpClients
{
    public interface IHttpClientService
    {
        Task<ApiResponse<T>> PostAsJsonAsync<T>(string url, object content);
    }
}
