namespace Shelfie.Domain.Models
{
    public class Boek
    {
        public int BoekID { get; set; }
        public string ISBN { get; set; }
        public string Titel { get; set; }
        public string? AuteurNaam { get; set; }
    }
}