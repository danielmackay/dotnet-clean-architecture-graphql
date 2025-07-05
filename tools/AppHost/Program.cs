using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var sqlServer = builder
    .AddSqlServer("sql")
    .WithLifetime(ContainerLifetime.Persistent);

var db = sqlServer
    .AddDatabase("graphql-db");

var migrationService = builder.AddProject<MigrationService>("migrations")
    .WithReference(db)
    .WaitFor(sqlServer);

// TODO: Add Blazor UI

builder
    .AddProject<GraphQL>("graphql-api")
    .WithEndpoint()
    .WithReference(db)
    .WaitForCompletion(migrationService);

builder.Build().Run();