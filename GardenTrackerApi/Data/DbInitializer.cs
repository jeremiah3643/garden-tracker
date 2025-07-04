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
            try
            {
                context.Database.Migrate();
                Console.WriteLine("Database migration completed successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred during database migration: {ex.Message}");
                throw; // Rethrow to stop application if migration fails
            }

            // Ensure the database is ready before querying
            context.Database.EnsureCreated(); // Temporary to ensure tables exist
            Console.WriteLine("Checking if database exists...");

            // Check if database has been seeded
            if (context.Crops.Any())
            {
                return; // DB has been seeded
            }

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