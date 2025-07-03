namespace GardenTrackerApi.Models
{
    public class Harvest
    {
        public int Id { get; set; }
        public int CropId { get; set; }
        public DateTime HarvestDate { get; set; }
        public double Quantity { get; set; }
        public string Notes { get; set; }
        public Crop Crop { get; set; }
    }
}