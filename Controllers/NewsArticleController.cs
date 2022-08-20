using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MentabilityAPI.Data;
using MentabilityAPI.Models;

namespace MentabilityAPI.Controllers
{
    // Controller för nyheter
    [Route("api/[controller]")]
    [ApiController]
    public class NewsArticleController : ControllerBase
    {
        // Instansierar context-klassen och lagrar instansen
        private readonly MentabilityContext _context;

        public NewsArticleController(MentabilityContext context)
        {
            _context = context;
        }

        // Hämtar alla nyheter i omvänd datumordning
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NewsArticle>>> GetNews()
        {
            return await _context.News.OrderByDescending(article => article.Date).ToListAsync();
        }

        // Hämtar en specifik nyhet. Returnerar 404 om nyheten inte hittas
        [HttpGet("{id}")]
        public async Task<ActionResult<NewsArticle>> GetNewsArticle(int id)
        {
            var newsArticle = await _context.News.FindAsync(id);

            if (newsArticle == null)
            {
                return NotFound();
            }

            return newsArticle;
        }

        /* Uppdaterar en nyhet. Returnerar 400 om id-parametern inte matchar nyhetens id.
            Returnerar 404 om nyheten inte hittas */
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNewsArticle(int id, NewsArticle newsArticle)
        {
            if (id != newsArticle.Id)
            {
                return BadRequest();
            }

            _context.Entry(newsArticle).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NewsArticleExists(id))
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

        // Lägger till en nyhet och returnerar nyheten
        [HttpPost]
        public async Task<ActionResult<NewsArticle>> PostNewsArticle(NewsArticle newsArticle)
        {
            _context.News.Add(newsArticle);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNewsArticle", new { id = newsArticle.Id }, newsArticle);
        }

        // Raderar en nyhet. Returnerar 404 om nyheten inte hittas 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNewsArticle(int id)
        {
            var newsArticle = await _context.News.FindAsync(id);
            if (newsArticle == null)
            {
                return NotFound();
            }

            _context.News.Remove(newsArticle);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Hittar en specifik nyhet
        private bool NewsArticleExists(int id)
        {
            return _context.News.Any(e => e.Id == id);
        }
    }
}
