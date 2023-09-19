using Account.Contracts.Requests.Auth;
using Microsoft.AspNetCore.Components.Authorization;
using Warehouse.Client.Auth;

namespace Warehouse.Client.Services
{
    public class AuthService : IAuthService
    {
        AuthenticationStateProvider AuthenticationStateProvider;
        public AuthService(AuthenticationStateProvider authenticationStateProvider)
        {
            AuthenticationStateProvider = authenticationStateProvider;
        }

        public async Task LoginAsync(AccountSignInRequest accountSignInRequest)
        {
            WarehouseAuthStateProvider.IsAuthenticated = true;
            //get and store token
            await AuthenticationStateProvider.GetAuthenticationStateAsync();
        }
    }
}
