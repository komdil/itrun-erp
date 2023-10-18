using Warehouse.Contracts.ProductPurchase;
using Warehouse.Contracts.Warehouse;

namespace Warehouse.Client.Services.Sales
{
    public interface IPurchaseService
    {
        Task<ApiResponse<List<SingleProductPurchaseResponse>>> GetAllAsync(GetProductPurchasesQuery request);

        Task<ApiResponse<SingleProductPurchaseResponse>> CreateAsync(CreateProductPurchaseRequest request);

        Task<ApiResponse> DeleteAsync(Guid id);
    }
}
