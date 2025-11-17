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
            if (string.IsNullOrWhiteSpace(gebruikersNaam))
                throw new ArgumentNullException("Vul gebruikersnaam in",nameof(gebruikersNaam));

            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentNullException("Vul Email in", nameof(email));
                    
            if (string.IsNullOrWhiteSpace(wachtwoordHash))
                throw new ArgumentNullException("Vul wachtwoord in",nameof(wachtwoordHash));
            
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
