using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MentabilityAPI.Data;
using MentabilityAPI.Models;

namespace MentabilityAPI.Controllers
{
    // Controller för aktiviteter
    [Route("api/[controller]")]
    [ApiController]
    public class QuoteController : ControllerBase
    {
        // Instansierar context-klassen och lagrar instansen
        private readonly MentabilityContext _context;

        public QuoteController(MentabilityContext context)
        {
            _context = context;
        }

        // Hämtar alla citat i omvänd datumordning
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Quote>>> GetQuotes()
        {
            return await _context.Quotes.OrderByDescending(quote => quote.Date).ToListAsync();
        }

        // Hämtar ett specifikt citat. Returnerar 404 om citatet inte hittas
        [HttpGet("{id}")]
        public async Task<ActionResult<Quote>> GetQuote(int id)
        {
            var quote = await _context.Quotes.FindAsync(id);

            if (quote == null)
            {
                return NotFound();
            }

            return quote;
        }

        /* Uppdaterar ett citat. Returnerar 400 om id-parametern inte matchar citatets id.
            Returnerar 404 om citatet inte hittas */
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuote(int id, Quote quote)
        {
            if (id != quote.Id)
            {
                return BadRequest();
            }

            _context.Entry(quote).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuoteExists(id))
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

        // Lägger till ett citat och returnerar citatet
        [HttpPost]
        public async Task<ActionResult<Quote>> PostQuote(Quote quote)
        {
            _context.Quotes.Add(quote);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetQuote", new { id = quote.Id }, quote);
        }

        // Raderar ett citat. Returnerar 404 om citatet inte hittas
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuote(int id)
        {
            var quote = await _context.Quotes.FindAsync(id);
            if (quote == null)
            {
                return NotFound();
            }

            _context.Quotes.Remove(quote);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Hittar ett specifikt citat
        private bool QuoteExists(int id)
        {
            return _context.Quotes.Any(e => e.Id == id);
        }
    }
}
