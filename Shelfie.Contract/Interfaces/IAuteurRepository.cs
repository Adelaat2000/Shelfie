using Shelfie.Contract.DTO;

namespace Shelfie.Contract.Interfaces;

public interface IAuteurRepository
{
    AuteurDTO? GetById(int id);
    AuteurDTO? GetByName(string naam);
    List<AuteurDTO> GetAuteursForBoek(int boekId);
}