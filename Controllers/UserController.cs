using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MentabilityAPI.Data;
using MentabilityAPI.Models;

namespace MentabilityAPI.Controllers
{
    // Controller för användare
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        // Instansierar context-klassen och lagrar instansen
        private readonly MentabilityContext _context;

        public UserController(MentabilityContext context)
        {
            _context = context;
        }

        // Hämtar alla användare
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // Hämtar en specifik användare. Returnerar 404 om användaren inte hittas
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        /* Uppdaterar en användare. Returnerar 400 om id-parametern inte matchar användarens id.
            Returnerar 404 om användaren inte hittas */
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // Lägger till en användare och returnerar användaren
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        // Raderar en användare. Returnerar 404 om användaren inte hittas 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Hittar en specifik användare
        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
