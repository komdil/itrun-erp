using Account.Contracts.UserRoles.Queries;
using Account.Contracts.UserRoles.Responses;
using Application.Contract.ApplicationRoles.Commands;
using Application.Contract.ApplicationRoles.Queries;
using Application.Contract.ApplicationRoles.Responses;
using Account.Contracts.UserRoles.Commands;
using Account.Contracts.User.Queries;
using Account.Contracts.User.Response;

namespace Warehouse.Client.Services.Admin
{
    public interface IAdminService
    {
        Task<ApiResponse<List<UserResponse>>> GetUsers(GetUsersQuery query);

        Task<ApiResponse<List<RoleNameResponse>>> GetRoles(GetRolesQuery query);

        Task<ApiResponse<RoleNameResponse>> CreateRoleAsync(CreateRoleCommand request);

        Task<ApiResponse> DeleteRoleAsync(string name);

        Task<ApiResponse<List<UserRolesResponse>>> GetUserRolesAsync(GetUserRolesQuery query);

        Task<ApiResponse<UserRolesResponse>> AssignRoleAsync(CreateUserRolesCommand request);

        Task<ApiResponse> DeleteRoleFromUserAsync(string slug);
    }
}
