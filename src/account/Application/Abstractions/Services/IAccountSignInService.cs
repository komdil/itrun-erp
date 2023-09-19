using Account.Contracts.Requests.Auth;
using Account.Contracts.Response.Auth;

namespace Application.Abstractions.Services
{
    public interface IAccountSignInService
    {
        Task<AccountSignInResponse> SignInAsync(AccountSignInRequest model);
    }
}
