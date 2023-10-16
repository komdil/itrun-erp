using Application.Abstractions.Data;
using Application.Abstractions.Services;
using Domain;
using Infrastructure.Data;
using Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                if (configuration["UseInMemoryDatabase"] == "true")
                    options.UseInMemoryDatabase("testDb");
                else
                    options.UseSqlServer(connectionString);
            });
            services.AddScoped<IApplicationDbInitializer, ApplicationDbInitializer>();
            services
                .AddIdentityCore<ApplicationUser>()
                .AddRoles<IdentityRole<Guid>>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddScoped<IAccountService, AccountService>();

            return services;
        }
    }
}
