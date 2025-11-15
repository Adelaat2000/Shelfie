using System.Collections.Generic;

namespace Shelfie.Logic.Models
{
    public class Gebruiker
    {
        public int GebruikerID { get; private set; }
        public string GebruikersNaam { get; set; }
        public string Email { get; set; }
        public string WachtwoordHash { get; private set; }
        public string PersoonlijkeInfo { get; set; }
        public string BannerURL { get; set; }
        public string IcoonURL { get; set; }
        
        public Gebruiker(
            int gebruikerID,
            string gebruikersNaam,
            string email,
            string wachtwoordHash,
            string persoonlijkeInfo,
            string bannerURL,
            string icoonURL)
        {
            GebruikerID = gebruikerID;
            GebruikersNaam = gebruikersNaam;
            Email = email;
            WachtwoordHash = wachtwoordHash;
            PersoonlijkeInfo = persoonlijkeInfo;
            BannerURL = bannerURL;
            IcoonURL = icoonURL;
        }
        
    }
}
