using System.Collections.Generic;

namespace Shelfie.Domain.Models
{
    public class Gebruiker
    {
        public int GebruikerID { get; set; }
        public string GebruikersNaam { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string WachtwoordHash { get; set; } = string.Empty;
        public string? PersoonlijkeInfo { get; set; }
        public string? BannerURL { get; set; }
        public string? IcoonURL { get; set; }
    }
}
