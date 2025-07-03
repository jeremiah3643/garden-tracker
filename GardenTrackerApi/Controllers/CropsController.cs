using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GardenTrackerApi.Models;

namespace GardenTrackerApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CropsController : ControllerBase
    {
        private readonly GardenContext _context;

        public CropsController(GardenContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Crop>>> GetCrops()
        {
            return await _context.Crops
                .Include(c => c.Harvests)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Crop>> GetCrop(int id)
        {
            var crop = await _context.Crops
                .Include(c => c.Harvests)
                .FirstOrDefaultAsync(c => c.Id == id);
            if (crop == null)
                return NotFound();
            return crop;
        }

        [HttpPost]
        public async Task<ActionResult<Crop>> PostCrop(Crop crop)
        {
            _context.Crops.Add(crop);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCrop), new { id = crop.Id }, crop);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCrop(int id)
        {
            var crop = await _context.Crops.FindAsync(id);
            if (crop == null)
                return NotFound();
            _context.Crops.Remove(crop);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}