using Shelfie.Contract.DTO;

namespace Shelfie.Contract.Interfaces
{
    public interface IBoekRepository
    { 
        void Create(BoekDTO boek);
        BoekDTO? GetByIsbn(string isbn);
        List<BoekDTO> GetByTitel(string titel);
        void Update(BoekDTO boek);
        void Delete(BoekDTO boek);
    }
}