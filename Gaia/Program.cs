using System.Text.Json.Serialization;
using Gaia.Infrastructure.Context;
using Gaia.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Configuração do banco de dados
builder.Services.AddDbContext<DbOracle>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registro dos serviços
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<LocationService>();
builder.Services.AddScoped<AccidentService>();
builder.Services.AddScoped<RequestionService>();

// Configuração dos controllers e JSON
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "GaiaAPI", Version = "v1" });
});

var app = builder.Build();

if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "GAIA API v1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseAuthorization();
app.MapControllers();
app.Run();