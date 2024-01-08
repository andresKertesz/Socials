using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Radzen;
using Socials.Client.Client;
using Socials.Client.Client.ClientService;
using Socials.Client.Client.Controllers;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<IApiClientService, ApiClientService>();
builder.Services.AddScoped<AuthenticationController>();

builder.Services.AddScoped<EventController>();
builder.Services.AddScoped<ComponentController>(x =>
{
    ComponentController componentController = new();
    componentController.ShowNavBar = true;
    return componentController;
});

builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

builder.Services.AddScoped<DialogService>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<TooltipService>();
builder.Services.AddScoped<ContextMenuService>();

builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

builder.Services.AddBlazoredLocalStorage();



await builder.Build().RunAsync();
