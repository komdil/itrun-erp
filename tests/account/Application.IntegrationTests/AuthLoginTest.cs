﻿using System.Net.Http.Json;
using System.Text;
using Contracts.Requests.Auth;
using Contracts.Response.Auth;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Identity;
using Domain;
using Microsoft.Extensions.DependencyInjection;
using System.Net;

namespace Application.IntegrationTests
{
    public class AuthLoginTest
    {
        private const string _userName = "TestUser";
        private const string _password = "ABc12345678@J$@!1";
        private HttpClient _httpClient;
        private IServiceScopeFactory _scopeFactory;
        [OneTimeSetUp]
        public async Task OneTimeSetUp()
        {
            var factory = new CustomWebApplicationFactory();
            _scopeFactory = factory.Services.GetRequiredService<IServiceScopeFactory>();
            _httpClient = factory.CreateClient();
            await AddUserToDbAsync(_userName, _password);
        }
        [Test]
        public async Task Login_ShouldGivAccessToken_WhenUserNameAndPasswordAreCorrect()
        {
            // Arrange
            AccountSignInRequest request = new() { Username = _userName, Password = _password };
            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            // Act
            HttpResponseMessage result = await _httpClient.PostAsync("auth/sign-in", content);

            // Assert
            var loginResponse = await result.Content.ReadFromJsonAsync<AccountSigninResponse>();
            result.EnsureSuccessStatusCode();
            Assert.That(loginResponse?.Token, Is.Not.Empty.And.Not.Null);
        }
        [Test]
        public async Task Login_ShouldGiveUnauthorized_WhenUserNameAndPasswordAreNotCorrect()
        {
            // Arrange
            AccountSignInRequest request = new() { Username = _userName, Password = "123ASd@_D!@#$" };
            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            // Act
            HttpResponseMessage result = await _httpClient.PostAsync("auth/sign-in", content);

            // Assert
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
        }

        async Task AddUserToDbAsync(string userName, string password)
        {
            using var scope = _scopeFactory.CreateScope();

            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            IdentityResult result = await userManager.CreateAsync(new ApplicationUser
            {
                UserName = userName,
            }, password);

            Assert.That(result.Succeeded, Is.True);
        }
    }
}
