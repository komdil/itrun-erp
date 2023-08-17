using Contracts.Requests.Auth;
using Contracts.Response.Auth;

namespace Application.Abstractions.Services
{
    public interface IAccountSignUpService
    {
        Task<AccountSignUpResponse> SignUpAsync(AccountSignUpRequest model);
    }
}
