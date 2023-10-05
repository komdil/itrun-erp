using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Collections.Generic;
using System.Threading.Tasks;
using Warehouse.Client.Services.HttpClients;
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

        public async Task<ApiResponse<List<SingleProductUomResponse>>> GetAllAsync(GetProductUomQuery getProductUomQuery)
        {
            return await _httpClient.GetAsJsonAsync<List<SingleProductUomResponse>>($"{_configuration["ProductUomServiceUrl"]}/productuom", getProductUomQuery);
        }

        public async Task<ApiResponse<SingleProductUomResponse>> CreateAsync(CreateProductUomRequest request)
        {
            return await _httpClient.PostAsJsonAsync<SingleProductUomResponse>($"{_configuration["ProductUomServiceUrl"]}/productuom", request);
        }

        public async Task<ApiResponse> DeleteAsync(DeleteProductUomRequest request)
        {
            return await _httpClient.DeleteAsync($"{_configuration["ProductUomServiceUrl"]}/productuom/{request.Id}");
        }

        public async Task<ApiResponse<SingleProductUomResponse>> UpdateAsync(UpdateProductUomRequest request)
        {
            return await _httpClient.PutAsJsonAsync<SingleProductUomResponse>($"{_configuration["ProductUomServiceUrl"]}/productuom/{request.Id}", request);
        }
    }
}
