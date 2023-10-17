using Account.Contracts.UserRoles.Responses;
using FluentAssertions;
using System.Net.Http.Json;

namespace Application.IntegrationTests
{
    public class UsersTest : SuperAdminTestBase
    {
        [Test]
        public async Task GetUsersTest()
        {
            // Act
            var users = await _httpClient.GetFromJsonAsync<List<UserRolesResponse>>("users");

            // Assert
            users.Count.Should().BeGreaterThanOrEqualTo(1);
        }
    }
}
