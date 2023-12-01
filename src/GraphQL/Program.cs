using CA.GraphQL.Infrastructure.Persistence;
using GraphQL.Filters;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddGraphQLServices();
builder.Services
    .AddGraphQLServer()
    .AddInMemorySubscriptions()
    .AddTypes()
    .RegisterDbContext<ApplicationDbContext>()
    .AddProjections()
    .AddFiltering()
    .AddSorting()
    .AddErrorFilter<ValidationFilter>()
//.AddMutationConventions()
//.SetPagingOptions(new PagingOptions()
//{
//    MaxPageSize = 50,
//    DefaultPageSize = 20,
//    IncludeTotalCount = true
//})
;

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();

    // Initialise and seed database
    using var scope = app.Services.CreateScope();
    var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();
    await initialiser.InitialiseAsync();
    await initialiser.SeedAsync();
}
else
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseCors(policy => policy
    .AllowAnyOrigin()
    .AllowAnyHeader()
    .AllowAnyMethod());

app.UseHealthChecks("/health");
app.UseHttpsRedirection();

app.UseRouting();

app.UseWebSockets();

app.MapGraphQL();

app.Run();
