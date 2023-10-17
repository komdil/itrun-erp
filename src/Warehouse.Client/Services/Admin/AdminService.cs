using Application.Contract.ApplicationRoles.Commands;
using Application.Contract.ApplicationRoles.Queries;
using Application.Contract.ApplicationRoles.Responses;
using Account.Contracts.UserRoles.Commands;
using Account.Contracts.UserRoles.Queries;
using Account.Contracts.UserRoles.Responses;
using Warehouse.Client.Services.HttpClients;
using Account.Contracts.User.Response;
using Account.Contracts.User.Queries;

namespace Warehouse.Client.Services.Admin
{
    public class AdminService : IAdminService
    {
        private readonly IHttpClientService _httpClient;
        private readonly IConfiguration _configuration;
        public AdminService(IHttpClientService httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<ApiResponse<UserRolesResponse>> AssignRoleAsync(CreateUserRolesCommand request)
        {
            return await _httpClient.PostAsJsonAsync<UserRolesResponse>($"{_configuration["AccountServiceUrl"]}/userroles", request);
        }

        public async Task<ApiResponse<RoleNameResponse>> CreateRoleAsync(CreateRoleCommand request)
        {
            return await _httpClient.PostAsJsonAsync<RoleNameResponse>($"{_configuration["AccountServiceUrl"]}/roles", request);
        }

        public async Task<ApiResponse> DeleteRoleAsync(string name)
        {
            return await _httpClient.DeleteAsync($"{_configuration["AccountServiceUrl"]}/roles/{name}");
        }

        public async Task<ApiResponse> DeleteRoleFromUserAsync(string slug)
        {
            return await _httpClient.DeleteAsync($"{_configuration["AccountServiceUrl"]}/userroles/{slug}");
        }

        public async Task<ApiResponse<List<RoleNameResponse>>> GetRoles(GetRolesQuery query)
        {
            return await _httpClient.GetAsJsonAsync<List<RoleNameResponse>>($"{_configuration["AccountServiceUrl"]}/roles", query);
        }

        public async Task<ApiResponse<List<UserRolesResponse>>> GetUserRolesAsync(GetUserRolesQuery query)
        {
            return await _httpClient.GetAsJsonAsync<List<UserRolesResponse>>($"{_configuration["AccountServiceUrl"]}/userroles", query);
        }

        public async Task<ApiResponse<List<UserResponse>>> GetUsers(GetUsersQuery query)
        {
            return await _httpClient.GetAsJsonAsync<List<UserResponse>>($"{_configuration["AccountServiceUrl"]}/users", query);
        }
    }
}
