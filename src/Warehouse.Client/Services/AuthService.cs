﻿using Account.Contracts.Requests.Auth;
using Account.Contracts.Response.Auth;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;

namespace Warehouse.Client.Services
{
    public class AuthService : IAuthService
    {
        const string loginAddress = "https://localhost:7012/Auth";

        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly HttpClient _httpClient;
        private ILocalStorageService _localStorageService;

        public AuthService(AuthenticationStateProvider authenticationStateProvider,
            HttpClient httpClient, ILocalStorageService localStorageService)
        {
            _authenticationStateProvider = authenticationStateProvider;
            _httpClient = httpClient;
            _localStorageService = localStorageService;
        }

        public async Task<bool> LoginAsync(AccountSignInRequest accountSignInRequest)
        {
            var response = await _httpClient.PostAsJsonAsync($"{loginAddress}/sign-in", accountSignInRequest);
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
            var response = await _httpClient.PostAsJsonAsync($"{loginAddress}/sign-up", accountSignUpRequest);
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