using Application.Contract.ApplicationRoles.Commands;
using Application.Contract.ApplicationRoles.Responses;
using Domain;
using FluentAssertions;
using System.Net.Http.Json;

namespace Application.IntegrationTests
{
    public class RolesTest : SuperAdminTestBase
    {
        [Test]
        public async Task CreateRoleTest()
        {
            // Arrange
            CreateRoleCommand request = new() { Name = "MyRole" };

            // Act
            HttpResponseMessage result = await _httpClient.PostAsJsonAsync("roles", request);

            // Assert
            result.EnsureSuccessStatusCode();
            var loginResponse = await result.Content.ReadFromJsonAsync<RoleNameResponse>();
            loginResponse.Name.Should().Be(request.Name);

            var roleInDb = await GetSingleEntityAsync<ApplicationRole>(s => s.Name == request.Name);
            roleInDb.Should().NotBeNull();
            roleInDb.Name.Should().Be(request.Name);
        }

        [Test]
        public async Task GetRolesTest()
        {
            // Arrange
            var newRole = new ApplicationRole() { Name = "SomeRole" };
            await CreateEntityAsync(newRole);

            // Act
            var roles = await _httpClient.GetFromJsonAsync<List<RoleNameResponse>>("roles");

            // Assert
            roles.Count.Should().BeGreaterThanOrEqualTo(1);
            roles.Any(s => s.Name == newRole.Name).Should().BeTrue();
        }

        [Test]
        public async Task UpdateRoleTest()
        {
            // Arrange
            var newRole = new ApplicationRole() { Name = "RoleToUpdate" };
            await CreateEntityAsync(newRole);

            UpdateRoleCommand request = new() { Name = "RoleToUpdate", NewRoleName = "NewRole" };

            // Act
            HttpResponseMessage result = await _httpClient.PutAsJsonAsync("roles/RoleToUpdate", request);

            // Assert
            result.EnsureSuccessStatusCode();
            var roleInDb = await GetSingleEntityAsync<ApplicationRole>(s => s.Name == request.NewRoleName);
            roleInDb.Should().NotBeNull();
        }

        [Test]
        public async Task DeleteRoleTest()
        {
            // Arrange
            var newRoleToDelete = new ApplicationRole() { Name = "RoleToDelete" };
            await CreateEntityAsync(newRoleToDelete);

            // Act
            HttpResponseMessage result = await _httpClient.DeleteAsync("roles/RoleToDelete");

            // Assert
            result.EnsureSuccessStatusCode();
            var roleInDb = await GetSingleEntityAsync<ApplicationRole>(s => s.Name == newRoleToDelete.Name);
            roleInDb.Should().BeNull();
        }
    }
}
