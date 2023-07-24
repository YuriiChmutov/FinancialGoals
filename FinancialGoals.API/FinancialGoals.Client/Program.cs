global using Microsoft.AspNetCore.Components.Authorization;

using Blazored.LocalStorage;
using Blazored.Modal;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using FinancialGoals.Client;
using FinancialGoals.Client.CategoryService;
using FinancialGoals.Client.Services;
using FinancialGoals.Client.Services.AuthService;
using FinancialGoals.Client.Services.TransactionService;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient {BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)});
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ImageService>();

builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddAntDesign();
builder.Services.AddBlazoredModal();
builder.Services.AddBlazoredLocalStorage();

await builder.Build().RunAsync();