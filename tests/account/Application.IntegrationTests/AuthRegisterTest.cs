using System.Net.Http.Json;
using System.Text;
using Contracts.Requests.Auth;
using Contracts.Response.Auth;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Identity;
using Domain;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using FluentAssertions;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Application.IntegrationTests
{
    public class AuthRegisterTest
    {
        private const string _userName = "TestUserForRegister";
        private const string _password = "ABc12345678@J$@!1";
        private const string _organization = "TestOrganization";
        private static readonly Guid _userId = Guid.NewGuid();
        private HttpClient _httpClient;
        private const string _userRole = "Admin";
        private IServiceScopeFactory _scopeFactory;

        [OneTimeSetUp]
        public async Task OneTimeSetUp()
        {
            var factory = new CustomWebApplicationFactory();
            _scopeFactory = factory.Services.GetRequiredService<IServiceScopeFactory>();
            _httpClient = factory.CreateClient();
        }

        [TestCase("TestUserForRegister", "ABc12345678@J$@!1", "TestOrganization1")]
        [TestCase("TestUserForRegisterAndLogin", "ABc12345678@J$@!1", "TestOrganizationLogin")]
        [TestCase("TestUserForRegisterLoginToken", "ABc12345678@J$@!1", "TestOrganizationToken")]
        public async Task RegisterUser_ShouldGiveAccessTokenWhenUsernameAndPasswordAreCorrect(string username, string password, string organization)
        {
            // Arrange
            AccountSignUpRequest request = new() { Username = username, Password = password, OrganizationName = organization };
            AccountSignInRequest accountSignInRequest = new() { Username = request.Username, Password = request.Password };
            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            var signInContent = new StringContent(JsonConvert.SerializeObject(accountSignInRequest), Encoding.UTF8, "application/json");

            // Act
            HttpResponseMessage registerResult = await _httpClient.PostAsync("auth/sign-up", content);
            HttpResponseMessage loginResult = await _httpClient.PostAsync("auth/sign-in", signInContent);

            // Assert
            var registerResponse = await registerResult.Content.ReadFromJsonAsync<AccountSignUpResponse>();
            registerResult.EnsureSuccessStatusCode();
            registerResponse.Should().NotBeNull();

            var loginResponse = await loginResult.Content.ReadFromJsonAsync<AccountSignInResponse>();
            loginResult.EnsureSuccessStatusCode();
            loginResponse.Should().NotBeNull();
            loginResponse.Token.Should().NotBeNullOrEmpty();
        }
    }
}
