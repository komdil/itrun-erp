using Warehouse.Client.Services;
using Warehouse.Contracts.Product;

namespace Warehouse.Client;

public interface IProductService
{
    Task<ApiResponse<List<SingleProductResponse>>> GetAllAsync(GetSingleProductQuery getProductQuery);

    Task<ApiResponse<SingleProductResponse>> CreateAsync(CreateProductRequest request);

    Task<ApiResponse<SingleProductResponse>> UpdateAsync(UpdateProductRequest request);

    Task<ApiResponse> DeleteAsync(DeleteProductRequest request);
}
