namespace MentabilityAPI.Models
{
    // Klass som hanterar användare
    public class User
    {
        // Properties
        // Id
        public int Id { get; set; }

        // Användarnamn
        public string? Username { get; set; }

        // Lösenord
        public string? Password { get; set; }

        // Förnamn
        public string? FirstName { get; set; }

        // Efternamn
        public string? LastName { get; set; }

        // E-post
        public string? Email { get; set; }
    }
}
