using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using GardenTrackerApi.Models;

namespace GardenTrackerApi.Data
{
    public static class DbInitializer
    {
public static void Initialize(GardenContext context)
{
    var connectionString = Environment.GetEnvironmentVariable("DATABASE_URL");
    if (string.IsNullOrEmpty(connectionString))
    {
        try
        {
            context.Database.Migrate();
            Console.WriteLine("Database migration completed successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred during database migration: {ex.Message}");
        }
    }
    else
    {
        Console.WriteLine("Skipping migration in production environment.");
    }

    if (!context.Crops.Any())
    {
        var crops = new Crop[]
        {
            new Crop { Name = "Tomato", PlantedDate = DateTime.Parse("2023-04-01"), Harvests = new List<Harvest>() },
            new Crop { Name = "Cucumber", PlantedDate = DateTime.Parse("2023-04-15"), Harvests = new List<Harvest>() },
            new Crop { Name = "Carrot", PlantedDate = DateTime.Parse("2023-03-20"), Harvests = new List<Harvest>() }
        };
        context.Crops.AddRange(crops);
        context.SaveChanges();
        Console.WriteLine("Database seeded with initial crops.");
    }
}
    }
}