using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Warehouse.Client.Services.HttpClients;
using Warehouse.Contracts.Warehouse;

namespace Warehouse.Client.Services.Warehouse
{
    public class WarehouseService : IWarehouseService
    {
        private readonly IHttpClientService _httpClient;
        private readonly IConfiguration _configuration;
        public WarehouseService(IHttpClientService httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<ApiResponse<List<SingleWarehouseResponse>>> GetAllAsync(GetWarehousesQuery getWarehousesQuery)
        {
            return await _httpClient.GetAsJsonAsync<List<SingleWarehouseResponse>>($"{_configuration["WarehouseServiceUrl"]}/warehouses", getWarehousesQuery);
        }

        public async Task<ApiResponse<SingleWarehouseResponse>> CreateAsync(CreateWarehouseRequest request)
        {
            return await _httpClient.PostAsJsonAsync<SingleWarehouseResponse>($"{_configuration["WarehouseServiceUrl"]}/warehouses", request);
        }

        public async Task<ApiResponse> DeleteAsync(DeleteWarehouseRequest request)
        {
            return await _httpClient.DeleteAsync($"{_configuration["WarehouseServiceUrl"]}/warehouses/{request.Id}");
        }

        public async Task<ApiResponse<SingleWarehouseResponse>> UpdateAsync(UpdateWarehouseRequest request)
        {
            return await _httpClient.PutAsJsonAsync<SingleWarehouseResponse>($"{_configuration["WarehouseServiceUrl"]}/warehouses/{request.Id}", request);
        }
    }
}
