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
    public class AuthLoginTest
    {
        private const string _userName = "TestUser";
        private const string _password = "ABc12345678@J$@!1";
        private const string _userRole = "Admin";
        private static readonly Guid _userId = Guid.NewGuid();
        private HttpClient _httpClient;
        private IServiceScopeFactory _scopeFactory;

        [OneTimeSetUp]
        public async Task OneTimeSetUp()
        {
            var factory = new CustomWebApplicationFactory();
            _scopeFactory = factory.Services.GetRequiredService<IServiceScopeFactory>();
            _httpClient = factory.CreateClient();
            await AddUserToDb(_userName, _password);
        }

        [Test]
        public async Task Login_ShouldGiveAccessToken_WhenUserNameAndPasswordAreCorrect()
        {
            // Arrange
            AccountSignInRequest request = new() { Username = _userName, Password = _password };
            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            // Act
            HttpResponseMessage result = await _httpClient.PostAsync("auth/sign-in", content);

            // Assert
            var loginResponse = await result.Content.ReadFromJsonAsync<AccountSigninResponse>();
            result.EnsureSuccessStatusCode();
            loginResponse.Should().NotBeNull();
            loginResponse.Token.Should().NotBeNullOrEmpty();
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
            result.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Test]
        public async Task AccessToken_ShouldbeValid()
        {
            // Arrange
            AccountSignInRequest request = new() { Username = _userName, Password = _password };
            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            // Act
            HttpResponseMessage result = await _httpClient.PostAsync("auth/sign-in", content);

            // Assert
            var loginResponse = await result.Content.ReadFromJsonAsync<AccountSigninResponse>();
            var tokenHandler = new JwtSecurityTokenHandler();
            Assert.DoesNotThrow(() => { tokenHandler.ReadJwtToken(loginResponse.Token); });
        }

        [Test]
        public async Task AccessToken_ShouldContainsUsernameAndIdClaims()
        {
            // Arrange
            AccountSignInRequest request = new() { Username = _userName, Password = _password };
            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            // Act
            HttpResponseMessage result = await _httpClient.PostAsync("auth/sign-in", content);

            // Assert
            var loginResponse = await result.Content.ReadFromJsonAsync<AccountSigninResponse>();
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwt = tokenHandler.ReadJwtToken(loginResponse.Token);

            var nameClaim = jwt.Claims.FirstOrDefault(s => s.Type == ClaimTypes.Name);
            nameClaim.Should().NotBeNull();
            nameClaim.Value.Should().Be(_userName);

            var idClaim = jwt.Claims.FirstOrDefault(s => s.Type == ClaimTypes.PrimarySid);
            idClaim.Should().NotBeNull();
            idClaim.Value.Should().Be(_userId.ToString());
        }

        [Test]
        public async Task AccessToken_ShouldContainsRoles()
        {
            // Arrange
            AccountSignInRequest request = new() { Username = _userName, Password = _password };
            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            // Act
            HttpResponseMessage result = await _httpClient.PostAsync("auth/sign-in", content);

            // Assert
            var loginResponse = await result.Content.ReadFromJsonAsync<AccountSigninResponse>();
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwt = tokenHandler.ReadJwtToken(loginResponse.Token);

            var roleClaim = jwt.Claims.FirstOrDefault(s => s.Type == ClaimTypes.Role);
            roleClaim.Should().NotBeNull();
            roleClaim.Value.Should().Be(_userRole);
        }

        async Task AddUserToDb(string userName, string password)
        {
            using var scope = _scopeFactory.CreateScope();

            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

            var addNewRoleResult = await roleManager.CreateAsync(new IdentityRole<Guid>
            {
                Name = _userRole
            });
            addNewRoleResult.Succeeded.Should().BeTrue();

            var user = new ApplicationUser
            {
                Id = _userId,
                UserName = userName,
            };

            IdentityResult result = await userManager.CreateAsync(user, password);
            result.Succeeded.Should().BeTrue();

            var assignRoleToUserResult = await userManager.AddToRoleAsync(user, _userRole);
            assignRoleToUserResult.Succeeded.Should().BeTrue();
        }
    }
}
