using Shelfie.Contract.DTO;
using Shelfie.Logic.Models;
    
namespace Shelfie.Logic.Mappers;

public class BoekMapper
{
    private readonly AuteurMapper _auteurMapper;
    public BoekMapper()
    {
        _auteurMapper = new AuteurMapper();
    }
    
    public Boek ToDomain(BoekDTO dto)
    {
        return new Boek(
            dto.Titel,
            dto.ISBN, 
            dto.Auteurs
            .Select(a => _auteurMapper.ToDomain(a))
            .ToList(),
            dto.Genres);
    }
}