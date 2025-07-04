using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var sqlServer = builder
    .AddSqlServer("sql")
    .WithLifetime(ContainerLifetime.Persistent);

var db = sqlServer
    .AddDatabase("clean-architecture");

var migrationService = builder.AddProject<MigrationService>("migrations")
    .WithReference(db)
    .WaitFor(sqlServer);

builder
    .AddProject<GraphQL>("graphql")
    .WithEndpoint()
    .WithReference(db)
    .WaitForCompletion(migrationService);

builder.Build().Run();