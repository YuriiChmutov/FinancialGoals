using System.Reflection;
using FinancialGoals.API;
using FinancialGoals.API.Middlewares;
using FinancialGoals.Data.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;
using Azure.Storage.Blobs;
using FinancialGoals.Data.Repository.AccountService;
using FinancialGoals.Data.Repository.AuthService;
using FinancialGoals.Data.Repository.CategoryService;
using FinancialGoals.Data.Repository.TransactionService;
using FinancialGoals.Data.Resolvers;
using FinancialGoals.Services;
using Microsoft.Azure.Cosmos;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers().AddJsonOptions(opt => opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

// builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<CategoryAmountResolver>();

builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IAuthService, AuthService>();

//builder.Services.AddSwaggerGen(c => c.AddSwaggerApiKeySecurity());

builder.Services.AddSwaggerGen(c =>
{
    c.AddSwaggerJWTSecurity();
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "FinancialGoals API",
        Version = "v1",
        Description = "An API to manage operations with finance",
        Contact = new OpenApiContact
        {
            Name = "Yuri Chmutov",
            Email = "yurii.chmutov@gmail.com",
            Url = new Uri("https://github.com/YuriiChmutov")
        }
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

//builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
//    .AddCookie(o =>
//    {
//        o.Events.OnRedirectToLogin = (context) =>
//        {
//            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
//            return Task.CompletedTask;
//        };
//    });

string blobStorageConnection =
    builder.Configuration.GetConnectionString("BlobStorageConnection");
string containerName = "categoryicons";

builder.Services.AddSingleton(x => 
    new BlobServiceClient(blobStorageConnection));
builder.Services.AddScoped<BlobStorageService>(x => 
    new BlobStorageService(x.GetService<BlobServiceClient>(), containerName));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opt =>
        {
            opt.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = 
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                        builder.Configuration.GetSection("AppSettings:Token").Value)),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        }
    );

// builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//     .AddJwtBearer(o =>
//     {
//         o.TokenValidationParameters = new TokenValidationParameters
//         {
//             ValidateIssuer = true,
//             ValidateAudience = true,
//             ValidateLifetime = true,
//             ValidateIssuerSigningKey = true,
//             ValidIssuer = "your-issuer",
//             ValidAudience = "your-audience",
//             IssuerSigningKey =
//                 new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("TokenKey").Value))
//         };
//     });

builder.Services.AddDbContext<FinancialDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("FinancialGoalsConnection"))
        .EnableSensitiveDataLogging()
        .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

builder.Services.AddDbContext<CosmosDbContext>(options =>
{
    options.UseCosmos(
        "AccountEndpoint=https://localhost:8081/;AccountKey=C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==",
        "Goals"
    );
});

//builder.Services.AddDbContext<FinancialDbContext>(opt =>
//{
//    opt.UseCosmos(
//        configuration.GetSection("CosmosDB:EndpointUri").Value,
//        configuration.GetSection("CosmosDB:PrimaryKey").Value,
//        configuration.GetSection("CosmosDB:DatabaseName").Value,
//        cosmosOptionsAction: cosmosOptions =>
//        {
//            cosmosOptions.ConnectionMode(ConnectionMode.Direct);
//            cosmosOptions.MaxRequestsPerTcpConnection(20);
//            cosmosOptions.MaxTcpConnectionsPerEndpoint(32);
//        });
//});

var app = builder.Build();

app.UseStaticFiles();

app.UseCors(builder =>
{
    builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowAnyOrigin();
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app.UseApiKey(); // == UseMiddleware<ApiKeyMiddleware>(); // but i created an extension
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
