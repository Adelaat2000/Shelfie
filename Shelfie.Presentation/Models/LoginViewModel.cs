using System.ComponentModel.DataAnnotations;

namespace Shelfie.Presentation.Models
{
    public class LoginViewModel
    {
        [Display(Name = "Gebruikersnaam")]
        public string GebruikersNaam { get; set; }
        public string Wachtwoord { get; set; }
    }
}
