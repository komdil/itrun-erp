using Application.Common.Interfaces;
using Application.IntegrationTests;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq.Expressions;

namespace Application.IntergrationTests
{
    public abstract class TestBase
    {
        protected HttpClient _httpClient;
        protected IServiceScopeFactory _scopeFactory;
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var factory = new CustomWebApplicationFactory();
            _scopeFactory = factory.Services.GetRequiredService<IServiceScopeFactory>();
            _httpClient = factory.CreateClient();
        }

        protected async Task<T> GetEntity<T>(Expression<Func<T, bool>> query) where T : class
        {
            using var scope = _scopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            return await dbContext.Set<T>().FirstOrDefaultAsync(query);
        }
    }
}
