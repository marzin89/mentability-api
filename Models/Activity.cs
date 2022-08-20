namespace MentabilityAPI.Models
{
    // Klass som hanterar aktiviteter
    public class Activity
    {
        // Properties
        // Id
        public int Id { get; set; }
        
        // Titel
        public string? Title { get; set; }

        // Innehåll
        public string? Content { get; set; }

        // Sökväg till bild
        public string? ImageUrl { get; set; }

        // Startdatum
        public DateTime? StartDate { get; set; }

        // Slutdatum
        public DateTime? EndDate { get; set; }

        // Datum för publicering/uppdatering
        public DateTime Date { get; set; }

        // Författare
        public string? Author { get; set; }
    }
}
