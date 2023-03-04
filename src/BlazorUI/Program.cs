using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BlazorUI;
using MudBlazor.Services;
using BlazorUI.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


builder.Services
    .AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services
    .AddTodoClient(strategy: StrawberryShake.ExecutionStrategy.CacheAndNetwork)
    .ConfigureHttpClient(client => client.BaseAddress = new Uri("https://localhost:7114/graphql/"));

builder.Services.AddSingleton<StorageService>();
builder.Services.AddSingleton<TodoListState>();
builder.Services.AddSingleton<AppState>();

builder.Services.AddMudServices();

await builder.Build().RunAsync();
