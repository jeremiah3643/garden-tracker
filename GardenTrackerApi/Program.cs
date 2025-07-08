using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using GardenTrackerApi.Data;
using GardenTrackerApi.Models;
using System.Web;
using Npgsql;
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
        Console.WriteLine("DATABASE_URL not found, using local fallback.");
        connectionString = "Host=localhost;Database=gardentrackerdb;Username=postgres;Password=yourpassword";
    }
    else
    {
        Console.WriteLine("Using Neon DATABASE_URL.");
        // Parse URI and convert to Npgsql format
        var uri = new Uri(connectionString);
        var builder = new NpgsqlConnectionStringBuilder
        {
            Host = uri.Host,
            Database = uri.AbsolutePath.TrimStart('/'),
            Username = uri.UserInfo.Split(':')[0],
            Password = uri.UserInfo.Split(':')[1]
        };
        // Handle query parameters
        var queryParams = System.Web.HttpUtility.ParseQueryString(uri.Query);
        if (queryParams["sslmode"] != null) builder.SslMode = SslMode.Require;
        if (queryParams["channel_binding"] != null) builder.ChannelBinding = ChannelBinding.Prefer; // Note: ChannelBinding might need adjustment
        connectionString = builder.ToString();
        Console.WriteLine($"Parsed connection string: {connectionString}");
    }
    try
    {
        options.UseNpgsql(connectionString);
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