using Account.Contracts.Requests.Auth;
using Account.Contracts.Response.Auth;

namespace Application.Abstractions.Services
{
    public interface IAccountService
    {
        Task<AccountSignInResponse> SignInAsync(AccountSignInRequest request);

        Task<AccountSignUpResponse> SignUpAsync(AccountSignUpRequest request);
    }
}
