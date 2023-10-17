using Account.Contracts.Auth;

namespace Application.Abstractions.Services
{
    public interface IAccountService
    {
        Task<AccountSignInResponse> SignInAsync(AccountSignInRequest request);

        Task<AccountSignUpResponse> SignUpAsync(AccountSignUpRequest request);
    }
}
