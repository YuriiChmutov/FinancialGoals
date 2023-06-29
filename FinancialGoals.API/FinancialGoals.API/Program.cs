using FinancialGoals.API;
using FinancialGoals.API.Middlewares;
using FinancialGoals.Data.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => c.AddSwaggerApiKeySecurity());

builder.Services.AddDbContext<FinancialDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("FinancialGoalsConnection"))
        .EnableSensitiveDataLogging()
        .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app.UseApiKey(); // == UseMiddleware<ApiKeyMiddleware>(); // but i created an extension
app.UseAuthorization();

app.MapControllers();

app.Run();
