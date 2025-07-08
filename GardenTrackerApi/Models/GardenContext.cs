using Microsoft.EntityFrameworkCore;

namespace GardenTrackerApi.Models
{
    public class GardenContext : DbContext
    {
        public GardenContext(DbContextOptions<GardenContext> options)
            : base(options)
        {
        }

        public DbSet<Crop> Crops { get; set; }
        public DbSet<Harvest> Harvests { get; set; }
    }
}