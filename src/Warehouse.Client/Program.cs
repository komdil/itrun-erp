using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TabBlazor;
using Warehouse.Client;
using Warehouse.Client.Auth;
using Warehouse.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddTabler();
builder.Services.AddOptions();
builder.Services.AddScoped<AuthenticationStateProvider, WarehouseAuthStateProvider>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddAuthorizationCore();
await builder.Build().RunAsync();
