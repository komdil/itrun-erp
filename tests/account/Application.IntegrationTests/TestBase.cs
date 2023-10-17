using Domain;
using FluentAssertions;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq.Expressions;

namespace Application.IntegrationTests
{
    public abstract class TestBase
    {
        protected const string _userName = "TestUserForReg";
        protected const string _password = "ABc12345678@J$@!1";
        protected const string _organization = "TestOrganization";
        protected static readonly Guid _userId = Guid.NewGuid();
        protected HttpClient _httpClient;
        protected const string _userRole = "Admin";

        public static IServiceScopeFactory _scopeFactory;
        internal static CustomWebApplicationFactory factory;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            factory = new CustomWebApplicationFactory();
            _scopeFactory = factory.Services.GetRequiredService<IServiceScopeFactory>();
        }

        [SetUp]
        public async Task Setup()
        {
            await TestSetupAsync();
        }

        protected virtual async Task TestSetupAsync()
        {
            _httpClient = factory.CreateClient();
            await AddUserToDb(_userName, _password);
        }

        protected async Task<ApplicationUser> AddUserToDb(string userName, string password, string role = "")
        {
            using var scope = _scopeFactory.CreateScope();
            if (role == string.Empty)
                role = _userRole;
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
            var existing = userManager.Users.FirstOrDefault(s => s.Id == _userId);
            if (existing != null)
                return existing;
            var addNewRoleResult = await roleManager.CreateAsync(new ApplicationRole
            {
                Name = role
            });
            addNewRoleResult.Succeeded.Should().BeTrue();

            var user = new ApplicationUser
            {
                Id = _userId,
                UserName = userName,
            };

            IdentityResult result = await userManager.CreateAsync(user, password);
            result.Succeeded.Should().BeTrue();

            var assignRoleToUserResult = await userManager.AddToRoleAsync(user, role);
            assignRoleToUserResult.Succeeded.Should().BeTrue();
            return user;
        }

        protected async Task<T> GetSingleEntityAsync<T>(Expression<Func<T, bool>> expression) where T : class
        {
            using var scope = _scopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            return await dbContext.Set<T>().FirstOrDefaultAsync(expression);
        }

        protected async Task CreateEntityAsync<T>(T entity) where T : class
        {
            using var scope = _scopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            await dbContext.Set<T>().AddAsync(entity);
            await dbContext.SaveChangesAsync();
        }
    }
}
