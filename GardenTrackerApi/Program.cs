using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using GardenTrackerApi.Data;
using GardenTrackerApi.Models;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });
builder.Services.AddDbContext<GardenContext>(options =>
{
    var connectionString = Environment.GetEnvironmentVariable("DATABASE_URL");
    Console.WriteLine($"DATABASE_URL: '{connectionString}'"); // Enhanced debug
    if (string.IsNullOrEmpty(connectionString))
    {
        Console.WriteLine("DATABASE_URL not found, using local fallback.");
        connectionString = "Host=localhost;Database=gardentrackerdb;Username=postgres;Password=yourpassword";
    }
    else
    {
        Console.WriteLine("Using Render DATABASE_URL.");
    }
    try
    {
        options.UseNpgsql(connectionString);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Npgsql configuration error: {ex.Message}");
        throw; // Re-throw to stop the app and log the failure
    }
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Garden Tracker API v1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();

app.Run();