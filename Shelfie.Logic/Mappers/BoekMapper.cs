using Shelfie.Contract.DTO;
using Shelfie.Logic.Models;
    
namespace Shelfie.Logic.Mappers;

public class BoekMapper
{
    private readonly AuteurMapper _auteurMapper;

    public BoekMapper(AuteurMapper auteurMapper)
    {
        _auteurMapper = auteurMapper;
    }

    public Boek ToDomain(BoekDTO dto)
    {
        var auteurs = dto.Auteurs
            .Select(a => _auteurMapper.ToDomain(a))
            .ToList();

        return new Boek(
            dto.BoekID,
            dto.ISBN,
            dto.Titel,
            auteurs
        );
    }

    public BoekDTO ToDto(Boek domain, int boekId = 0)
    {
        var auteurDtos = domain.Auteurs
            .Select(a => _auteurMapper.ToDto(a))
            .ToList();

        return new BoekDTO(
            boekId,
            domain.ISBN,
            domain.Titel,
            auteurDtos
        );
    }
}
