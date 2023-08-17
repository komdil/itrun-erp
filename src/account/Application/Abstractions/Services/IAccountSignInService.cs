using Contracts.Requests.Auth;
using Contracts.Response.Auth;

namespace Application.Abstractions.Services
{
    public interface IAccountSignInService
    {
        Task<AccountSignInResponse> SignInAsync(AccountSignInRequest model);
    }
}
