using Application.Abstractions.Services;
using Application.Common;
using Domain;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;

namespace Application.IntegrationTests
{
    public abstract class SuperAdminTestBase : TestBase
    {
        protected override async Task TestSetupAsync()
        {
            await base.TestSetupAsync();
            using var scope = _scopeFactory.CreateScope();
            var tokenService = scope.ServiceProvider.GetRequiredService<ITokenService>();
            var superAdmin = await GetSingleEntityAsync<ApplicationUser>(s => s.UserName == Constants.SuperAdminUserName);
            string jwtToken = await tokenService.GenerateTokenAsync(superAdmin);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
        }
    }
}
