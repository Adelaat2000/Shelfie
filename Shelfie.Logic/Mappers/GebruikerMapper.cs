using Shelfie.Contract.DTO;
using Shelfie.Logic.Models;

namespace Shelfie.Logic.Mappers
{
    public class GebruikerMapper
    {
        public Gebruiker ToDomain(GebruikerDTO dto)
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