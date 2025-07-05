using CA.GraphQL.Application.Common.Interfaces;
using CA.GraphQL.Infrastructure.Persistence;
using CA.GraphQL.Infrastructure.Persistence.Interceptors;
using CA.GraphQL.Infrastructure.Services;
using Microsoft.Extensions.Hosting;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IHostApplicationBuilder AddInfrastructureServices(this IHostApplicationBuilder builder)
    {
        builder.AddSqlServerDbContext<ApplicationDbContext>("graphql-db",
            null,
            options =>
            {
                options.AddInterceptors(provider => provider.GetRequiredService<AuditableEntitySaveChangesInterceptor>());
            });

        builder.Services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

        builder.Services.AddScoped<AuditableEntitySaveChangesInterceptor>();
        builder.Services.AddTransient<IDateTime, DateTimeService>();

        return builder;
    }
}