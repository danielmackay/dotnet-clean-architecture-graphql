using GraphQL.Filters;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.AddInfrastructureServices();
builder.Services.AddApplicationServices();
builder.Services.AddWebServices();

builder.Services
    .AddGraphQLServer()
    .AddInMemorySubscriptions()
    .AddTypes()
    .AddProjections()
    .AddFiltering()
    .AddSorting()
    .AddErrorFilter<ValidationFilter>()
    .ModifyPagingOptions(options =>
    {
        options.IncludeTotalCount = true;
        options.MaxPageSize = 50;
        options.DefaultPageSize = 20;
        options.EnableRelativeCursors = false;
    })
    ;

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
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
app.MapDefaultEndpoints();

app.Run();