using Account.Contracts.Claims.Commands;
using Account.Contracts.Claims.Responses;
using Domain;
using FluentAssertions;
using System.Net.Http.Json;

namespace Application.IntegrationTests
{
    public class UserClaimsTest : SuperAdminTestBase
    {
        [Test]
        public async Task CreateUserClaimTest()
        {
            // Arrange
            CreateUserClaimCommand request = new() { ClaimType = "someType", UserId = _userId, ClaimValue = "someValue" };

            // Act
            HttpResponseMessage result = await _httpClient.PostAsJsonAsync("userclaims", request);

            // Assert
            result.EnsureSuccessStatusCode();
            var loginResponse = await result.Content.ReadFromJsonAsync<SingleClaimResponse>();
            loginResponse.ClaimType.Should().Be(request.ClaimType);
            loginResponse.ClaimValue.Should().Be(request.ClaimValue);
            loginResponse.Slug.Should().Be($"{_userName}-{request.ClaimType}");

            var claimInDb = await GetSingleEntityAsync<ApplicationUserClaim>(s => s.Slug == $"{_userName}-{request.ClaimType}");
            claimInDb.Should().NotBeNull();
        }

        [Test]
        public async Task GetUserClaimsTest()
        {
            // Arrange
            var newUserClaim = await CreateUserAndClaimAsync();

            // Act
            var claims = await _httpClient.GetFromJsonAsync<List<SingleClaimResponse>>("userclaims");

            // Assert
            claims.Count.Should().BeGreaterThanOrEqualTo(1);
            claims.Any(s => s.Slug == newUserClaim.Slug).Should().BeTrue();
        }

        [Test]
        public async Task DeleteUserClaimTest()
        {
            // Arrange
            var newUserClaim = await CreateUserAndClaimAsync();

            // Act
            var responseMessage = await _httpClient.DeleteAsync($"userclaims/{newUserClaim.Slug}");

            // Assert
            responseMessage.EnsureSuccessStatusCode();
            var deletedEntity = await GetSingleEntityAsync<ApplicationUserClaim>(s => s.Slug == newUserClaim.Slug);
            deletedEntity.Should().BeNull();
        }

        async Task<ApplicationUserClaim> CreateUserAndClaimAsync()
        {
            var newUser = await AddUserToDb("someusername", "1234$5gSDFSwQWe");
            var userClaim = new ApplicationUserClaim()
            {
                UserId = newUser.Id,
                Slug = $"{newUser.UserName}-newType",
                ClaimType = "newType",
                ClaimValue = "newValue"
            };
            await CreateEntityAsync(userClaim);
            return userClaim;
        }
    }
}
