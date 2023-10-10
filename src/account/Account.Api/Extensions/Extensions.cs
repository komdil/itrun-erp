using Application.Abstractions.Data;

namespace Account.Api.Extensions
{
    public static class Extensions
    {
        public static void Migrate(this IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var dbInitializer = scope.ServiceProvider.GetRequiredService<IApplicationDbInitializer>();
            dbInitializer.Initialize();
        }
    }
}
