using System.Collections.Generic;

namespace Shelfie.Logic.Models
{
    public class Gebruiker
    {
        public int GebruikerID { get; set; }
        public string GebruikersNaam { get; set; } 
        public string Email { get; set; }
        public string WachtwoordHash { get; set; }
        public string PersoonlijkeInfo { get; set; }
        public string BannerURL { get; set; }
        public string IcoonURL { get; set; }
    }
}
