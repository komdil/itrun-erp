using Warehouse.Contracts.Warehouse;

namespace Warehouse.Client.Services.Warehouse
{
    public interface IWarehouseService
    {
        Task<ApiResponse<List<SingleWarehouseResponse>>> GetAllAsync(GetWarehousesQuery getWarehousesQuery);

        Task<ApiResponse<SingleWarehouseResponse>> CreateAsync(CreateWarehouseRequest request);

        Task<ApiResponse<SingleWarehouseResponse>> UpdateAsync(UpdateWarehouseRequest request);

        Task<ApiResponse> DeleteAsync(DeleteWarehouseRequest request);
    }
}
