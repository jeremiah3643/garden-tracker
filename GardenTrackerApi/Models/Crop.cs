namespace GardenTrackerApi.Models
{
    public class Crop
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public DateTime PlantedDate { get; set; }
        public string? Notes { get; set; }
        public List<Harvest>? Harvests { get; set; }
    }
}