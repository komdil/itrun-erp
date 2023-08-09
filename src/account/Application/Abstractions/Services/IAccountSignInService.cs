using Contracts.Requests.Auth;
using Contracts.Response.Auth;

namespace Application.Abstractions.Services
{
    public interface IAccountSignInService
    {
        Task<AccountSigninResponse> SignInAsync(AccountSigninRequest model);
    }
}
