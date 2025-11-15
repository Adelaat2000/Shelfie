using Shelfie.Logic.DTOs;
using Shelfie.Logic.Models;

namespace Shelfie.Logic.Mappers
{
    public class GebruikerMapper
    {
        public Gebruiker ToDomain(GebruikerDto dto)
        {
            return new Gebruiker(
                dto.GebruikerId,
                dto.GebruikersNaam,
                dto.Email,
                dto.WachtwoordHash,
                dto.PersoonlijkeInfo,
                dto.BannerURL,
                dto.IcoonURL
            );
        }
    }
}