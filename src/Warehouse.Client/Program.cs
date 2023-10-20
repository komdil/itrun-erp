using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TabBlazor;
using Warehouse.Client;
using Warehouse.Client.Constants;
using Warehouse.Client.Pages.Auth;
using Warehouse.Client.Services.Admin;
using Warehouse.Client.Services.Auth;
using Warehouse.Client.Services.HttpClients;
using Warehouse.Client.Services.Product;
using Warehouse.Client.Services.ProductUom;
using Warehouse.Client.Services.Reports;
using Warehouse.Client.Services.Sales;
using Warehouse.Client.Services.Warehouse;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<IHttpClientService, HttpClientService>();
builder.Services.AddTabler();
builder.Services.AddOptions();
builder.Services.AddScoped<AuthenticationStateProvider, WarehouseAuthStateProvider>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IWarehouseService, WarehouseService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductUomServise, ProductUomServise>();
builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ISaleProductService, SaleProductService>();
builder.Services.AddScoped<IPurchaseService, PurchaseService>();
builder.Services.AddScoped<IReportService, ReportService>();

builder.Services.AddAuthorizationCore();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddHttpClient();
await builder.Build().RunAsync();
