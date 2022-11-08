using BethanysPieShopHRM.App;
using BethanysPieShopHRM.App.Services;
using BethanysPieShopHRM.App.Services.Interface;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient<IEmployeeDataService, EmployeeDataService>(client =>
    client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)).AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

builder.Services.AddHttpClient<ICountryDataService, CountryDataService>(client =>
    client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)).AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

builder.Services.AddHttpClient<IJobCategoryDataService, JobCategoryDataService>(client =>
    client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)).AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

// LOCALIZATION1 - SUPPORTED CULTURES AND DEFAULT - REMEMBER TO SET ITEM PROPERTY 'BlazorWebAssemblyLoadAllGlobalizationData' TO 'TRUE' IN .CSPROJ FILE
var supportedCultuers = new string[] { "en", "it"};
builder.Services.Configure<RequestLocalizationOptions>(options => 
{
    options
        .AddSupportedCultures(supportedCultuers)
        .AddSupportedUICultures(supportedCultuers)
        .SetDefaultCulture("en");
});

// LOCALIZATION2 - REFERENCE RESOURCES FILES PATH
builder.Services.AddLocalization(
    options => options.ResourcesPath = "Resources");

builder.Services.AddScoped<ApplicationState>();

builder.Services.AddBlazoredLocalStorage();

builder.Services.AddOidcAuthentication(options =>
{
    builder.Configuration.Bind("Auth0", options.ProviderOptions);
    options.ProviderOptions.ResponseType = "code";
    options.ProviderOptions.AdditionalProviderParameters.Add("audience", builder.Configuration["Auth0:Audience"]);
});

await builder.Build().RunAsync();

