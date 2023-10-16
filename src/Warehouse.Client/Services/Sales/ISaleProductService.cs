using Warehouse.Contracts.SellProduct;
using Warehouse.Contracts.Warehouse;

namespace Warehouse.Client.Services.Sales
{
	public interface ISaleProductService
	{
		Task<ApiResponse<List<SingleProductSellResponse>>> GetAllAsync(GetSaleProductsQuery request);

		Task<ApiResponse<SingleProductSellResponse>> CreateAsync(CreateSellProductRequest request);

		Task<ApiResponse> DeleteAsync(DeleteSaleProductRequest request);
	}
}
