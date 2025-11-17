using Shelfie.Contract.DTO;

namespace Shelfie.Contract.Interfaces
{
    public interface IGebruikerRepository
    {
        GebruikerDTO? GetByEmail(string email);
        GebruikerDTO? GetByUsername(string username);
        void AddUser(GebruikerDTO? gebruiker);
        void Delete(int gebruikerId);
    }
}