using CA.GraphQL.Infrastructure.Persistence;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddWebUIServices(this IServiceCollection services)
    {
        services.AddDatabaseDeveloperPageExceptionFilter();

        services.AddHealthChecks()
            .AddDbContextCheck<ApplicationDbContext>();

        return services;
    }
}
