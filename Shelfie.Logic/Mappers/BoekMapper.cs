using Shelfie.Contract.DTO;
using Shelfie.Contract.InputDTO;
using Shelfie.Contract.OutputDTO;
using Shelfie.Logic.Models;
    
namespace Shelfie.Logic.Mappers;

public class BoekMapper
{
    public Boek ToDomain(BoekInputDTO dto)
    {
        return new Boek(0, dto.ISBN, dto.Titel );
    }

    public Boek ToDomain(BoekDTO dto)
    {
        return new Boek(dto.BoekID, dto.ISBN, dto.Titel);
    }

    public BoekOutputDTO ToOutput(Boek boek)
    {
        return new BoekOutputDTO
        {
            ISBN = boek.ISBN,
            Titel = boek.Titel,
            AuteurNaam = boek.Auteurs.Select(a => a.AuteurNaam).ToList()
        };
    }
}