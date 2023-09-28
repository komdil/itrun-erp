using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TabBlazor;
using Warehouse.Client;
using Warehouse.Client.Pages.Auth;
using Warehouse.Client.Services.Auth;
using Warehouse.Client.Services.HttpClients;
using Warehouse.Client.Services.Warehouse;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient());
builder.Services.AddScoped<IHttpClientService, HttpClientService>();
builder.Services.AddTabler();
builder.Services.AddOptions();
builder.Services.AddScoped<AuthenticationStateProvider, WarehouseAuthStateProvider>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IWarehouseService, WarehouseService>();
builder.Services.AddAuthorizationCore();
builder.Services.AddBlazoredLocalStorage();

await builder.Build().RunAsync();
