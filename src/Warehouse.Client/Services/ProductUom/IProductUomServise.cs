using System.Collections.Generic;
using System.Threading.Tasks;
using Warehouse.Contracts.ProductUOM;
using Warehouse.Contracts.Warehouse;

namespace Warehouse.Client.Services.ProductUom
{
    public interface IProductUomServise
    {
        Task<ApiResponse<List<SingleProductUomResponse>>> GetAllAsync(GetProductsUomQuery getProductUomQuery);

        Task<ApiResponse<SingleProductUomResponse>> CreateAsync(CreateProductUOMRequest request);

        Task<ApiResponse<SingleProductUomResponse>> UpdateAsync(UpdateProductUomRequest request);

        Task<ApiResponse> DeleteAsync(string slug);
    }
}
