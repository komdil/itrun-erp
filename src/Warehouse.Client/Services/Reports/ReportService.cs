using Warehouse.Client.Services.HttpClients;
using Warehouse.Contracts.Reports;

namespace Warehouse.Client.Services.Reports
{
    public class ReportService : IReportService
    {
        private readonly IHttpClientService _httpClient;
        private readonly IConfiguration _configuration;
        public ReportService(IHttpClientService httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<ApiResponse<ProductAggregatesResponse>> GetProductReports(GetProductAggregatesQuery request)
        {
            return await _httpClient.GetAsJsonAsync<ProductAggregatesResponse>($"{_configuration["WarehouseServiceUrl"]}/reports", request);
        }
    }
}
