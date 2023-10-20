using Account.Contracts.UserRoles.Commands;
using Account.Contracts.UserRoles.Responses;
using Domain;
using FluentAssertions;
using System.Net.Http.Json;

namespace Application.IntegrationTests
{
    public class UserRolesTest : SuperAdminTestBase
    {
        [Test]
        public async Task CreateUserRoleTest()
        {
            // Arrange
            var newRole = new ApplicationRole { Name = "someName" };
            await CreateEntityAsync(newRole);
            CreateUserRolesCommand request = new() { RoleId = newRole.Id, UserId = _userId };

            // Act
            HttpResponseMessage result = await _httpClient.PostAsJsonAsync("userroles", request);

            // Assert
            result.EnsureSuccessStatusCode();
            var loginResponse = await result.Content.ReadFromJsonAsync<UserRolesResponse>();
            loginResponse.UserId.Should().Be(_userId);
            loginResponse.RoleId.Should().Be(newRole.Id);
            loginResponse.Slug.Should().Be($"{_userName}-someName");

            var roleInDb = await GetSingleEntityAsync<ApplicationUserRole>(s => s.Slug == $"{_userName}-someName");
            roleInDb.Should().NotBeNull();
        }

        [Test]
        public async Task GetUserRolesTest()
        {
            // Arrange
            var newUserRole = await CreateUserAndRoleAsync();

            // Act
            var roles = await _httpClient.GetFromJsonAsync<List<UserRolesResponse>>("userroles");

            // Assert
            roles.Count.Should().BeGreaterThanOrEqualTo(1);
            roles.Any(s => s.Slug == newUserRole.Slug).Should().BeTrue();
        }

        [Test]
        public async Task DeleteUserRoleTest()
        {
            // Arrange
            var newUserRole = await CreateUserAndRoleAsync("forDelete");

            // Act
            var responseMessage = await _httpClient.DeleteAsync($"userroles/{newUserRole.Slug}");

            // Assert
            responseMessage.EnsureSuccessStatusCode();
            var deletedEntity = await GetSingleEntityAsync<ApplicationUserRole>(s => s.Slug == newUserRole.Slug);
            deletedEntity.Should().BeNull();
        }

        async Task<ApplicationUserRole> CreateUserAndRoleAsync(string rolePrefix = "")
        {
            var newUser = await AddUserToDb("someusername", "1234$5gSDFSwQWe");
            var newRole = new ApplicationRole
            {
                Name = "someName" + rolePrefix
            };
            await CreateEntityAsync(newRole);
            var userRole = new ApplicationUserRole()
            {
                UserId = newUser.Id,
                RoleId = newRole.Id,
                Slug = $"{newUser.UserName}-{newRole.Name}"
            };
            await CreateEntityAsync(userRole);
            return userRole;
        }
    }
}
