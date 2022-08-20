using MentabilityAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MentabilityAPI.Data
{
    // Context-klass
    public class MentabilityContext : DbContext
    {
        // Konstruktor
        public MentabilityContext(DbContextOptions<MentabilityContext> options) : base(options) {}

        // Properties/tabeller för nyheter, aktiviteter, citat och användare
        public DbSet<NewsArticle> News { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
