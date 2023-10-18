using Warehouse.Client.Services.HttpClients;
using Warehouse.Contracts.ProductPurchase;
using Warehouse.Contracts.Warehouse;

namespace Warehouse.Client.Services.Sales
{
    public class PurchaseService : IPurchaseService
    {
        private readonly IHttpClientService _httpClient;
        private readonly IConfiguration _configuration;
        public PurchaseService(IHttpClientService httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<ApiResponse<SingleProductPurchaseResponse>> CreateAsync(CreateProductPurchaseRequest request)
        {
            return await _httpClient.PostAsJsonAsync<SingleProductPurchaseResponse>($"{_configuration["WarehouseServiceUrl"]}/purchaseproduct", request);
        }

        public async Task<ApiResponse> DeleteAsync(Guid id)
        {
            return await _httpClient.DeleteAsync($"{_configuration["WarehouseServiceUrl"]}/purchaseproduct/{id}");

        }

        public async Task<ApiResponse<List<SingleProductPurchaseResponse>>> GetAllAsync(GetProductPurchasesQuery request)
        {
            return await _httpClient.GetAsJsonAsync<List<SingleProductPurchaseResponse>>($"{_configuration["WarehouseServiceUrl"]}/purchaseproduct", request);
        }
    }
}
