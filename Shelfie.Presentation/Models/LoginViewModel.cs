using System.ComponentModel.DataAnnotations;

namespace Shelfie.Presentation.Models;

public class LoginViewModel
{
    [Required(ErrorMessage = "Gebruikersnaam is verplicht.")]
    [Display(Name = "Gebruikersnaam")]
    public string GebruikersNaam { get; set; } = string.Empty;

    [Required(ErrorMessage = "Wachtwoord is verplicht.")]
    [DataType(DataType.Password)]
    public string Wachtwoord { get; set; } = string.Empty;
}
