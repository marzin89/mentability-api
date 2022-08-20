using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MentabilityAPI.Data;
using MentabilityAPI.Models;

namespace MentabilityAPI.Controllers
{
    // Controller för aktiviteter
    [Route("api/[controller]")]
    [ApiController]
    public class ActivityController : ControllerBase
    {
        // Instansierar context-klassen och lagrar instansen
        private readonly MentabilityContext _context;

        public ActivityController(MentabilityContext context)
        {
            _context = context;
        }

        // Hämtar alla aktiviteter i omvänd datumordning
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Activity>>> GetActivities()
        {
            return await _context.Activities.OrderByDescending(activity => activity.Date).ToListAsync();
        }

        // Hämtar en specifik aktivitet. Returnerar 404 om aktiviteten inte hittas
        [HttpGet("{id}")]
        public async Task<ActionResult<Activity>> GetActivity(int id)
        {
            var activity = await _context.Activities.FindAsync(id);

            if (activity == null)
            {
                return NotFound();
            }

            return activity;
        }

        /* Uppdaterar en aktivitet. Returnerar 400 om id-parametern inte matchar aktivitetens id.
            Returnerar 404 om aktiviteten inte hittas */
        [HttpPut("{id}")]
        public async Task<IActionResult> PutActivity(int id, Activity activity)
        {
            if (id != activity.Id)
            {
                return BadRequest();
            }

            _context.Entry(activity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActivityExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // Lägger till en aktivitet och returnerar aktiviteten
        [HttpPost]
        public async Task<ActionResult<Activity>> PostActivity(Activity activity)
        {
            _context.Activities.Add(activity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetActivity", new { id = activity.Id }, activity);
        }

        // Raderar en aktivitet. Returnerar 404 om aktiviteten inte hittas
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActivity(int id)
        {
            var activity = await _context.Activities.FindAsync(id);
            if (activity == null)
            {
                return NotFound();
            }

            _context.Activities.Remove(activity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Hittar en specifik aktivitet
        private bool ActivityExists(int id)
        {
            return _context.Activities.Any(e => e.Id == id);
        }
    }
}
