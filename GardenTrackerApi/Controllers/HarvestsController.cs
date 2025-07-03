using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GardenTrackerApi.Models;

namespace GardenTrackerApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HarvestsController : ControllerBase
    {
        private readonly GardenContext _context;

        public HarvestsController(GardenContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<Harvest>> PostHarvest(Harvest harvest)
        {
            _context.Harvests.Add(harvest);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(PostHarvest), new { id = harvest.Id }, harvest);
        }
    }
}