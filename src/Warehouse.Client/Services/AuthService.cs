using Account.Contracts.Requests.Auth;
using Account.Contracts.Response.Auth;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;
using Warehouse.Client.Pages.Auth;

namespace Warehouse.Client.Services
{
    public class AuthService : IAuthService
    {
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorageService;
        private readonly IConfiguration _configuration;
        public AuthService(AuthenticationStateProvider authenticationStateProvider,
            HttpClient httpClient, ILocalStorageService localStorageService, IConfiguration configuration)
        {
            _authenticationStateProvider = authenticationStateProvider;
            _httpClient = httpClient;
            _localStorageService = localStorageService;
            _configuration = configuration;
        }

        public async Task<bool> LoginAsync(AccountSignInRequest accountSignInRequest)
        {
            var response = await _httpClient.PostAsJsonAsync($"{_configuration["AccountServiceUrl"]}/sign-in", accountSignInRequest);
            if (response.IsSuccessStatusCode)
            {
                var tokenResponse = await response.Content.ReadFromJsonAsync<AccountSignInResponse>();
                await _localStorageService.SetItemAsync(IAuthService.TokenLocalStorageKey, tokenResponse.Token);
                await _authenticationStateProvider.GetAuthenticationStateAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task LogoutAsync()
        {
            await _localStorageService.RemoveItemAsync(IAuthService.TokenLocalStorageKey);
            await _authenticationStateProvider.GetAuthenticationStateAsync();
        }

        public async Task<AccountSignUpResponse> SignUpAsync(AccountSignUpRequest accountSignUpRequest)
        {
            var response = await _httpClient.PostAsJsonAsync($"{_configuration["AccountServiceUrl"]}/sign-up", accountSignUpRequest);
            var tokenResponse = await response.Content.ReadFromJsonAsync<AccountSignUpResponse>();
            if (response.IsSuccessStatusCode)
            {
                await _localStorageService.SetItemAsync(IAuthService.TokenLocalStorageKey, tokenResponse.Token);
                await _authenticationStateProvider.GetAuthenticationStateAsync();
            }
            return tokenResponse;
        }
    }
}
