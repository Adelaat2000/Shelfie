namespace Shelfie.Presentation.Models 
{
    public class BoekViewModel
    {
        public int BoekID { get; set; }
        public string Titel { get; set; } = string.Empty;
        public string? AuteurNaam { get; set; }
        public string ISBN { get; set; } = string.Empty;
    }
}