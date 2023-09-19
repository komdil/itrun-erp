using Account.Contracts.Requests.Auth;

namespace Warehouse.Client.Services
{
    public interface IAuthService
    {
        Task LoginAsync(AccountSignInRequest accountSignInRequest);
    }
}
