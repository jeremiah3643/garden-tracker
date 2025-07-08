using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using GardenTrackerApi.Data;
using GardenTrackerApi.Models;
using Npgsql

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
    Console.WriteLine($"DATABASE_URL: '{connectionString ?? "null"}'");
    if (string.IsNullOrEmpty(connectionString))
    {
        connectionString = "Host=localhost;Database=gardentrackerdb;Username=postgres;Password=yourpassword"; // Local fallback
    }
    else
    {
        Console.WriteLine("Using Neon DATABASE_URL.");
    }
    try
    {
        var npgsqlBuilder = new NpgsqlConnectionStringBuilder(connectionString)
        {
            SslMode = SslMode.Require, // Neon requires SSL
            TrustServerCertificate = true // For local testing with self-signed certs
        };
        options.UseNpgsql(npgsqlBuilder.ToString());
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Npgsql configuration error: {ex.Message}");
        throw;
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