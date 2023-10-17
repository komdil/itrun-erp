using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Account.Application;

public static class DependencyInitializer
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddOptions();
        services.AddMediatR(s => s.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddAutoMapper(typeof(DependencyInitializer).Assembly);
    }
}