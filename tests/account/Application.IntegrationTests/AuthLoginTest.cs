﻿using System.Net.Http.Json;
using System.Text;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Identity;
using Domain;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using FluentAssertions;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Account.Contracts.Auth;

namespace Application.IntegrationTests
{
    public class AuthLoginTest : TestBase
    {
        [Test]
        public async Task Login_ShouldGiveAccessToken_WhenUserNameAndPasswordAreCorrect()
        {
            // Arrange
            AccountSignInRequest request = new() { Username = _userName, Password = _password };
            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            // Act
            HttpResponseMessage result = await _httpClient.PostAsync("auth/sign-in", content);

            // Assert
            var loginResponse = await result.Content.ReadFromJsonAsync<AccountSignInResponse>();
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
        public async Task AccessToken_ShouldBeValid()
        {
            // Arrange
            AccountSignInRequest request = new() { Username = _userName, Password = _password };
            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            // Act
            HttpResponseMessage result = await _httpClient.PostAsync("auth/sign-in", content);

            // Assert
            var loginResponse = await result.Content.ReadFromJsonAsync<AccountSignInResponse>();
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
            var loginResponse = await result.Content.ReadFromJsonAsync<AccountSignInResponse>();
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
            var loginResponse = await result.Content.ReadFromJsonAsync<AccountSignInResponse>();
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwt = tokenHandler.ReadJwtToken(loginResponse.Token);

            var roleClaim = jwt.Claims.FirstOrDefault(s => s.Type == ClaimTypes.Role);
            roleClaim.Should().NotBeNull();
            roleClaim.Value.Should().Be(_userRole);
        }
    }
}
