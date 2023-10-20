using Warehouse.Contracts.ProductPurchase;
using Warehouse.Contracts.Reports;
using Warehouse.Contracts.Warehouse;

namespace Warehouse.Client.Services.Reports
{
    public interface IReportService
    {
        Task<ApiResponse<ProductAggregatesResponse>> GetProductReports(GetProductAggregatesQuery request);
    }
}
