using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations; // Nodig voor data validatie

namespace Shelfie.Presentation.Models 
{
    public class EditProfileViewModel
    {
        public int GebruikerID { get; set; } 

        [MaxLength(1000, ErrorMessage = "De omschrijving mag maximaal 1000 tekens lang zijn.")]
        public string? PersoonlijkeInfo { get; set; }
        public IFormFile? IcoonUpload { get; set; }
        public IFormFile? BannerUpload { get; set; }

        public string HuidigeIcoonURL { get; set; } = string.Empty;
        public string HuidigeBannerURL { get; set; } = string.Empty;
    }
}