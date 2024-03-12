using eShop.UI.Admin;
using eShop.UI.Http.Clients;
using eShop.UI.Admin.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using AutoMapper;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddSingleton<UIServiceAdmin>();
builder.Services.AddHttpClient<CategoryHttpClient>();
builder.Services.AddHttpClient<ProductHttpClient>();
ConfigureAutoMapper();


await builder.Build().RunAsync();


void ConfigureAutoMapper()
{
    var config = new MapperConfiguration(cfg =>
    {
        //cfg.CreateMap<CategoryGetDTO, LinkOption>().ReverseMap();
    });
    var mapper = config.CreateMapper();
    builder.Services.AddSingleton(mapper);
}