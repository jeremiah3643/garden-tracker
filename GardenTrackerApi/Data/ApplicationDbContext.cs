using Microsoft.EntityFrameworkCore;
using GardenTrackerApi.Models;

namespace GardenTrackerApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Crop> Crops { get; set; }
        public DbSet<Harvest> Harvests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Crop>()
                .HasMany(c => c.Harvests)
                .WithOne(h => h.Crop)
                .HasForeignKey(h => h.CropId);
        }
    }
}