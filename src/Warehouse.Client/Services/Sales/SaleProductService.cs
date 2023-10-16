using MediatR;
using Warehouse.Client.Services.HttpClients;
using Warehouse.Contracts.SellProduct;
using Warehouse.Contracts.Warehouse;

namespace Warehouse.Client.Services.Sales
{
	public class SaleProductService : ISaleProductService
	{
		private readonly IHttpClientService _httpClient;
		private readonly IConfiguration _configuration;
		public SaleProductService(IHttpClientService httpClient, IConfiguration configuration)
		{
			_httpClient = httpClient;
			_configuration = configuration;
		}

		public  async Task<ApiResponse<SingleProductSellResponse>> CreateAsync(CreateSellProductRequest request)
		{
			return await _httpClient.PostAsJsonAsync<SingleProductSellResponse>($"{_configuration["WarehouseServiceUrl"]}/saleProduct", request);
		}

		public async Task<ApiResponse> DeleteAsync(DeleteSaleProductRequest request)
		{
            return await _httpClient.DeleteAsync($"{_configuration["WarehouseServiceUrl"]}/saleProduct/{request.Id}");

		}

		public async Task<ApiResponse<List<SingleProductSellResponse>>> GetAllAsync(GetSaleProductsQuery request)
		{
			return await _httpClient.GetAsJsonAsync<List<SingleProductSellResponse>>($"{_configuration["WarehouseServiceUrl"]}/saleProduct", request);
		}
	}
}
