using eShop.UI.Admin;
using eShop.UI.Http.Clients;
using eShop.UI.Http.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddSingleton<UIServiceAdmin>();
builder.Services.AddHttpClient<CategoryHttpClient>();
builder.Services.AddHttpClient<ProductHttpClient>();


await builder.Build().RunAsync();
