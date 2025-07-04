using CA.GraphQL.Application.Common.Interfaces;
// using CA.GraphQL.Infrastructure.Identity;
using CA.GraphQL.Infrastructure.Persistence;
using CA.GraphQL.Infrastructure.Persistence.Interceptors;
using CA.GraphQL.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
                var serviceProvider = builder.Services.BuildServiceProvider();
                options.AddInterceptors(serviceProvider.GetRequiredService<AuditableEntitySaveChangesInterceptor>());
            });

        builder.Services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

        // builder.Services
        //     .AddDefaultIdentity<ApplicationUser>()
        //     .AddRoles<IdentityRole>()
        //     .AddEntityFrameworkStores<ApplicationDbContext>();

        // builder.Services.AddIdentityServer()
        //     .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();

        builder.Services.AddScoped<AuditableEntitySaveChangesInterceptor>();
        builder.Services.AddTransient<IDateTime, DateTimeService>();
        // builder.Services.AddTransient<IIdentityService, IdentityService>();

        // services.Scan(scan => scan
        //     .FromEntryAssembly()
        //     .AddClasses(filter => filter.InNamespaceOf<TodoItemRepository>())
        //     .AsImplementedInterfaces());

        //services.AddAuthentication()
        //    .AddIdentityServerJwt();

        //services.AddAuthorization(options =>
        //    options.AddPolicy("CanPurge", policy => policy.RequireRole("Administrator")));

        return builder;
    }
}