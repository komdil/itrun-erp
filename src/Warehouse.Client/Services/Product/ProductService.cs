using Warehouse.Client.Services.HttpClients;
using Warehouse.Contracts.Product;

namespace Warehouse.Client.Services.Product
{
    public class ProductService : IProductService
    {
        private readonly IHttpClientService _httpClient;
        private readonly IConfiguration _configuration;
        public ProductService(IHttpClientService httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<ApiResponse<List<SingleProductResponse>>> GetAllAsync(GetProductsQuery getProductQuery)
        {
            return await _httpClient.GetAsJsonAsync<List<SingleProductResponse>>($"{_configuration["WarehouseServiceUrl"]}/products", getProductQuery);
        }

        public async Task<ApiResponse<SingleProductResponse>> CreateAsync(CreateProductRequest request)
        {
            return await _httpClient.PostAsJsonAsync<SingleProductResponse>($"{_configuration["WarehouseServiceUrl"]}/products", request);
        }

        public async Task<ApiResponse> DeleteAsync(DeleteProductRequest request)
        {
            return await _httpClient.DeleteAsync($"{_configuration["WarehouseServiceUrl"]}/products/{request.Slug}");
        }

        public async Task<ApiResponse<SingleProductResponse>> UpdateAsync(UpdateProductRequest request)
        {
            return await _httpClient.PutAsJsonAsync<SingleProductResponse>($"{_configuration["WarehouseServiceUrl"]}/products/{request.Id}", request);
        }
    }
}