using Account.Contracts.Auth;

namespace Warehouse.Client.Services.Auth
{
    public interface IAuthService
    {
        public const string TokenLocalStorageKey = "TokenLocalStorageKey";
        Task<ApiResponse<AccountSignInResponse>> LoginAsync(AccountSignInRequest accountSignInRequest);
        Task<ApiResponse<AccountSignUpResponse>> SignUpAsync(AccountSignUpRequest accountSignInRequest);
        Task LogoutAsync();
    }
}
