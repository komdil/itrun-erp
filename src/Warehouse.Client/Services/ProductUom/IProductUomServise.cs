using System.Collections.Generic;
using System.Threading.Tasks;
using Warehouse.Contracts.Warehouse;

namespace Warehouse.Client.Services.ProductUom
{
    public interface IProductUomServise
    {
        Task<ApiResponse<List<SingleProductUomResponse>>> GetAllAsync(GetProductUomQuery getProductUomQuery);

        Task<ApiResponse<SingleProductUomResponse>> CreateAsync(CreateProductUomRequest request);

        Task<ApiResponse<SingleProductUomResponse>> UpdateAsync(UpdateProductUomRequest request);

        Task<ApiResponse> DeleteAsync(DeleteProductUomRequest request);
    }
}
