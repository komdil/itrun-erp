using Application.Common.Interfaces;
using Application.IntegrationTests;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Linq.Expressions;
using System.Net.Http.Headers;

namespace Application.IntergrationTests
{
    public abstract class TestBase
    {
        protected HttpClient _httpClient;
        protected IServiceScopeFactory _scopeFactory;
        internal CustomWebApplicationFactory _factory;
        protected TestJwtTokenService _tokenService;
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _factory = new CustomWebApplicationFactory();
            _scopeFactory = _factory.Services.GetRequiredService<IServiceScopeFactory>();

            using var scope = _scopeFactory.CreateScope();
            var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();
            _tokenService = new TestJwtTokenService(configuration);
        }

        [SetUp]
        public void Setup()
        {
            _httpClient = _factory.CreateClient();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _tokenService.GenerateToken("SuperAdmin"));
        }

        protected async Task<T> GetEntity<T>(Expression<Func<T, bool>> query) where T : class
        {
            using var scope = _scopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            return await dbContext.Set<T>().FirstOrDefaultAsync(query);
        }

        protected List<T> GetEntities<T>(Expression<Func<T, bool>> query = null) where T : class
        {
            using var scope = _scopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            IQueryable<T> queryForAll = dbContext.Set<T>();
            if (query != null)
            {
                queryForAll = queryForAll.Where(query);
            }
            return queryForAll.ToList();
        }

        protected async Task AddAsync<T>(T entity) where T : class
        {
            using var scope = _scopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            await dbContext.Set<T>().AddAsync(entity);
            await dbContext.SaveChangesAsync();
        }
    }
}
