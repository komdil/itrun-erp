using Warehouse.Contracts.Categories;

namespace Warehouse.Client.Services.ProductUom
{
    public interface ICategoryService
    {
        Task<ApiResponse<List<SingleCategoryResponse>>> GetAllAsync(GetCategoryQuery query);

        Task<ApiResponse<SingleCategoryResponse>> CreateAsync(CreateCategoryRequest request);

        Task<ApiResponse<SingleCategoryResponse>> UpdateAsync(UpdateCategoryRequest request);

        Task<ApiResponse> DeleteAsync(string slug);
    }
}
