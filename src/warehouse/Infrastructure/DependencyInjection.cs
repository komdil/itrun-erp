using Application.Abstractions.Data;
using Application.Common.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IApplicationDbInitializer, ApplicationDbInitializer>();
           
            services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(options =>
            {
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                if (configuration["UseInMemoryDatabase"] == "true")
                    options.UseInMemoryDatabase("testDb");
                else
                    options.UseSqlServer(connectionString);
            });
            return services;
        }
    }
}