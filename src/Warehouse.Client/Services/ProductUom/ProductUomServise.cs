using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Collections.Generic;
using System.Threading.Tasks;
using Warehouse.Client.Services.HttpClients;
using Warehouse.Contracts.ProductUOM;
using Warehouse.Contracts.Warehouse;

namespace Warehouse.Client.Services.ProductUom
{
    public class ProductUomServise : IProductUomServise
    {
        private readonly IHttpClientService _httpClient;
        private readonly IConfiguration _configuration;
        public ProductUomServise(IHttpClientService httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<ApiResponse<List<SingleProductUomResponse>>> GetAllAsync(GetProductsUomQuery getProductUomQuery)
        {
            return await _httpClient.GetAsJsonAsync<List<SingleProductUomResponse>>($"{_configuration["WarehouseServiceUrl"]}/productuoms", getProductUomQuery);
        }

        public async Task<ApiResponse<SingleProductUomResponse>> CreateAsync(CreateProductUOMRequest request)
        {
            return await _httpClient.PostAsJsonAsync<SingleProductUomResponse>($"{_configuration["WarehouseServiceUrl"]}/productuoms", request);
        }

        public async Task<ApiResponse> DeleteAsync(string slug)
        {
            return await _httpClient.DeleteAsync($"{_configuration["WarehouseServiceUrl"]}/productuoms/{slug}");
        }

        public async Task<ApiResponse<SingleProductUomResponse>> UpdateAsync(UpdateProductUomRequest request)
        {
            return await _httpClient.PutAsJsonAsync<SingleProductUomResponse>($"{_configuration["WarehouseServiceUrl"]}/productuoms/{request.Id}", request);
        }
    }
}
