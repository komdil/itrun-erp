using Account.Contracts.Auth;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Warehouse.Client.Services.HttpClients;

namespace Warehouse.Client.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly IHttpClientService _httpClient;
        private readonly ILocalStorageService _localStorageService;
        private readonly IConfiguration _configuration;
        public AuthService(AuthenticationStateProvider authenticationStateProvider,
            IHttpClientService httpClient, ILocalStorageService localStorageService, IConfiguration configuration)
        {
            _authenticationStateProvider = authenticationStateProvider;
            _httpClient = httpClient;
            _localStorageService = localStorageService;
            _configuration = configuration;
        }

        public async Task<ApiResponse<AccountSignInResponse>> LoginAsync(AccountSignInRequest accountSignInRequest)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync<AccountSignInResponse>($"{_configuration["AccountServiceUrl"]}/Auth/sign-in", accountSignInRequest);
                if (response.Success)
                {
                    await _localStorageService.SetItemAsync(IAuthService.TokenLocalStorageKey, response.Result.Token);
                    await _authenticationStateProvider.GetAuthenticationStateAsync();
                }
                else if (response.HttpStatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    response.Error = "Username or password is incorrect!";
                }

                return response;
            }
            catch (Exception ex)
            {
                return ApiResponse<AccountSignInResponse>.BuildFailed($"An unexpected error occurred. Please contact customer support. {ex.Message}");
            }
        }

        public async Task LogoutAsync()
        {
            await _localStorageService.RemoveItemAsync(IAuthService.TokenLocalStorageKey);
            await _authenticationStateProvider.GetAuthenticationStateAsync();
        }

        public async Task<ApiResponse<AccountSignUpResponse>> SignUpAsync(AccountSignUpRequest accountSignUpRequest)
        {
            try
            {
                var tokenResponse = await _httpClient.PostAsJsonAsync<AccountSignUpResponse>($"{_configuration["AccountServiceUrl"]}/Auth/sign-up", accountSignUpRequest);
                if (tokenResponse.Success)
                {
                    await _localStorageService.SetItemAsync(IAuthService.TokenLocalStorageKey, tokenResponse.Result.Token);
                    await _authenticationStateProvider.GetAuthenticationStateAsync();
                }
                else if (tokenResponse.Result != null)
                {
                    tokenResponse.Error = tokenResponse.Result.Message;
                }

                return tokenResponse;
            }
            catch (Exception ex)
            {
                return ApiResponse<AccountSignUpResponse>.BuildFailed($"An unexpected error occurred. Please contact customer support. {ex.Message}");
            }
        }
    }
}
