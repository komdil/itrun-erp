using Account.Contracts.Requests.Auth;
using Account.Contracts.Response.Auth;

namespace Application.Abstractions.Services
{
    public interface IAccountSignUpService
    {
        Task<AccountSignUpResponse> SignUpAsync(AccountSignUpRequest model);
    }
}
