using CA.GraphQL.Application.Common.Interfaces;
using CA.GraphQL.Infrastructure.Persistence;
using CA.GraphQL.WebUI.Services;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddWebServices(this IServiceCollection services)
    {
        services.AddDatabaseDeveloperPageExceptionFilter();

        services.AddSingleton<ICurrentUserService, CurrentUserService>();

        services.AddCors();

        services.AddHttpContextAccessor();

        return services;
    }
}