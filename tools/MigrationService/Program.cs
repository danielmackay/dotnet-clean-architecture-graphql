using CA.GraphQL.Application.Common.Interfaces;
using CA.GraphQL.Infrastructure.Persistence;
using CA.GraphQL.Infrastructure.Persistence.Interceptors;
using CA.GraphQL.Infrastructure.Services;
using MigrationService;
using MigrationService.Initialisers;

var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddHostedService<Worker>();


builder.Services
    .AddOpenTelemetry()
    .WithTracing(tracing => tracing.AddSource(Worker.ActivitySourceName));

builder.Services.AddApplicationServices();
builder.Services.AddScoped<ApplicationDbContextInitialiser>();
builder.Services.AddScoped<AuditableEntitySaveChangesInterceptor>();
builder.Services.AddScoped<ICurrentUserService, MigrationUserService>();
builder.Services.AddScoped<IDateTime, DateTimeService>();

builder.AddSqlServerDbContext<ApplicationDbContext>("graphql-db",
    null,
    options =>
    {
        var serviceProvider = builder.Services.BuildServiceProvider();
        options.AddInterceptors(serviceProvider.GetRequiredService<AuditableEntitySaveChangesInterceptor>());
    });


var host = builder.Build();

await host.RunAsync();