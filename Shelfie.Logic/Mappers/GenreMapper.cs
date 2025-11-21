using Shelfie.Contract.DTO;
using Shelfie.Logic.Models;

namespace Shelfie.Logic.Mappers;

public class GenreMapper
{
    public Genre ToDomain(GenreDTO dto)
    {
        return new Genre(dto.GenreId, dto.GenreNaam);
    }
    
    public GenreDTO ToDto(Genre domain)
    {
        return new GenreDTO(domain.GenreID, domain.GenreNaam);
    }
}