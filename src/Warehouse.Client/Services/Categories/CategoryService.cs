using Warehouse.Client.Services.HttpClients;
using Warehouse.Contracts.Categories;

namespace Warehouse.Client.Services.ProductUom
{
    public class CategoryService : ICategoryService
    {
        private readonly IHttpClientService _httpClient;
        private readonly IConfiguration _configuration;
        public CategoryService(IHttpClientService httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<ApiResponse<List<SingleCategoryResponse>>> GetAllAsync(GetCategoryQuery query)
        {
            return await _httpClient.GetAsJsonAsync<List<SingleCategoryResponse>>($"{_configuration["WarehouseServiceUrl"]}/categories", query);
        }

        public async Task<ApiResponse<SingleCategoryResponse>> CreateAsync(CreateCategoryRequest request)
        {
            return await _httpClient.PostAsJsonAsync<SingleCategoryResponse>($"{_configuration["WarehouseServiceUrl"]}/categories", request);
        }

        public async Task<ApiResponse> DeleteAsync(string slug)
        {
            return await _httpClient.DeleteAsync($"{_configuration["WarehouseServiceUrl"]}/categories/{slug}");
        }

        public async Task<ApiResponse<SingleCategoryResponse>> UpdateAsync(UpdateCategoryRequest request)
        {
            return await _httpClient.PutAsJsonAsync<SingleCategoryResponse>($"{_configuration["WarehouseServiceUrl"]}/categories/{request.Id}", request);
        }
    }
}
