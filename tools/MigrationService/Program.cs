using CA.GraphQL.Application.Common.Interfaces;
using CA.GraphQL.Infrastructure.Persistence;
using CA.GraphQL.Infrastructure.Persistence.Interceptors;
using MigrationService;
using MigrationService.Initialisers;

var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddHostedService<Worker>();

builder.Services
    .AddOpenTelemetry()
    .WithTracing(tracing => tracing.AddSource(Worker.ActivitySourceName));

builder.Services.AddScoped<ApplicationDbContextInitialiser>();
builder.Services.AddScoped<AuditableEntitySaveChangesInterceptor>();
builder.Services.AddScoped<ICurrentUserService, MigrationUserService>();
builder.Services.AddSingleton(TimeProvider.System);

builder.AddSqlServerDbContext<ApplicationDbContext>("GraphQL",
    null,
    options =>
    {
        var serviceProvider = builder.Services.BuildServiceProvider();
        options.AddInterceptors(serviceProvider.GetRequiredService<AuditableEntitySaveChangesInterceptor>());
    });


var host = builder.Build();

await host.RunAsync();