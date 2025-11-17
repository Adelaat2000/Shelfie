using Shelfie.Contract.DTO;
using Shelfie.Logic.Models;

namespace Shelfie.Logic.Mappers;

public class AuteurMapper
{
    public Auteur ToDomain(AuteurDTO dto)
    {
        return new Auteur(dto.AuteurId, dto.AuteurNaam);
    }
    
    public AuteurDTO ToDto(Auteur domain)
    {
        return new AuteurDTO(domain.AuteurID, domain.AuteurNaam);
    }
}