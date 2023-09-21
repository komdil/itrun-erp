using Account.Contracts.Requests.Auth;

namespace Warehouse.Client.Services
{
    public interface IAuthService
    {
        public const string TokenLocalStorageKey = "TokenLocalStorageKey";
        Task<bool> LoginAsync(AccountSignInRequest accountSignInRequest);
        Task LogoutAsync();
    }
}
