namespace MentabilityAPI.Models
{
    // Klass som hanterar citat
    public class Quote
    {
        // Properties
        // Id
        public int Id { get; set; }

        // Citat
        public string? Content { get; set; }

        // Upphovsperson
        public string? Author { get; set; }

        // Användare
        public string? User { get; set; }

        // Datum för publicering/uppdatering
        public DateTime Date { get; set; }
    }
}
