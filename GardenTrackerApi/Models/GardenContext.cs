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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Host=localhost;Database=gardentrackerdb;Username=postgres;Password=091690");
            }
        }
    }
}