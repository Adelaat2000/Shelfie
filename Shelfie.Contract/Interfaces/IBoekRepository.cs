using Shelfie.Contract.DTO;

namespace Shelfie.Contract.Interfaces
{
    public interface IBoekRepository
    { 
        BoekDTO? GetByIsbn(string isbn);
        IEnumerable<BoekDTO> GetByTitel(string titel);
        IEnumerable<BoekDTO> GetByAuteurID(int auteurId);
        BoekDTO Insert(BoekDTO boek, List<int> auteurIds);
        BoekDTO Update(BoekDTO boek, List<int> auteurIds);
    }
}