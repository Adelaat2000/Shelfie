using Shelfie.Contract.DTO;

namespace Shelfie.Contract.Interfaces
{
    public interface IBoekRepository
    {
        BoekDTO? GetBoekByEmail(string email);
        BoekDTO? GetBoekByTitel(string titel);
        BoekDTO? GetBoekByISBN(string isbn);
        void addBoek(BoekDTO boek);
    }
}