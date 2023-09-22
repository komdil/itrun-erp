using Account.Contracts.Requests.Auth;
using Account.Contracts.Response.Auth;

namespace Warehouse.Client.Services
{
    public interface IAuthService
    {
        public const string TokenLocalStorageKey = "TokenLocalStorageKey";
        Task<bool> LoginAsync(AccountSignInRequest accountSignInRequest);
        Task<AccountSignUpResponse> SignUpAsync(AccountSignUpRequest accountSignInRequest);
        Task LogoutAsync();
    }
}
