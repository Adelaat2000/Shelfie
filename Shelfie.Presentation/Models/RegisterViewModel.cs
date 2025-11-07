using System.ComponentModel.DataAnnotations;

namespace Shelfie.Presentation.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Gebruikersnaam is verplicht.")]
        public string Gebruikersnaam { get; set; }

        [Required(ErrorMessage = "E-mailadres is verplicht.")]
        [EmailAddress(ErrorMessage = "Voer een geldig e-mailadres in.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Wachtwoord is verplicht.")]
        [DataType(DataType.Password)]
        public string Wachtwoord { get; set; }

        [DataType(DataType.Password)]
        [Compare("Wachtwoord", ErrorMessage = "De wachtwoorden komen niet overeen.")]
        public string ConfirmWachtwoord { get; set; }
    }
}