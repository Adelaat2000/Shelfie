using Shelfie.Logic.DTOs;
using Shelfie.Logic.Models;

namespace Shelfie.Logic.Mappers;

public static class GebruikerMapper
{
    public static GebruikerDto ToDto(this Gebruiker gebruiker)
    {
        return new GebruikerDto(
            gebruiker.GebruikerID,
            gebruiker.GebruikersNaam,
            gebruiker.Email);
    }
}
