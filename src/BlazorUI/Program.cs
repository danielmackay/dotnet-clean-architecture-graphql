using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BlazorUI;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


builder.Services
    .AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services
    .AddTodoClient(strategy: StrawberryShake.ExecutionStrategy.CacheAndNetwork)
    .ConfigureHttpClient(client => client.BaseAddress = new Uri("https://localhost:44339/graphql/"));

builder.Services.AddMudServices();

await builder.Build().RunAsync();
