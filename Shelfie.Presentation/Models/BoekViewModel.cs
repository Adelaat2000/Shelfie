using System.ComponentModel.DataAnnotations;

namespace Shelfie.Presentation.ViewModels;
public class BoekViewModel
{
    [Required]
    public string ISBN { get; set; }

    [Required]
    public string Titel { get; set; }

    [MinLength(1, ErrorMessage = "Voeg een auteur toe" )]
    public List<string> AuteurNamen { get; set; } = new();
}